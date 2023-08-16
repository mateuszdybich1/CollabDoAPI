﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        

        public TaskService(
            IUserContext userContext, 
            IUserRepository userRepository, 
            ILeaderRepository leaderRepository,
            IEmployeeRepository employeeRepository, 
            IProjectRepository projectRepository, 
            ITaskRepository taskRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _leaderRepository = leaderRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }
        public Guid CreateTask(TaskDto taskDto)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            ProjectValidation validation = new ProjectValidation(_projectRepository);
            validation.ValidateProjectId(taskDto.ProjectId);

            TaskEntity taskEntity = new TaskEntity(taskDto.ProjectId,taskDto.Name,taskDto.Priority,taskDto.Deadline, leaderId);

            _taskRepository.AddTask(taskEntity);

            return taskEntity.Id;
        }

        public async Task<Guid> AssignToEmployee(Guid projectId, Guid taskId, string employeeEmail)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            TaskValidation taskValidation = new TaskValidation(_taskRepository);
            taskValidation.ValidateTask(leaderId,projectId,taskId);

            TaskEntity task = _taskRepository.GetTask(projectId, taskId);

            task.ModifiedBy = leaderId;

            Guid employeeUserId = await _userRepository.GetUserIdByEmail(employeeEmail);

            Guid employeeId = _employeeRepository.GetEmployeeId(leaderId, employeeUserId);

            task.AssignToEmployee(employeeId);



            _taskRepository.UpdateTask(task);

            return taskId;
            
        }

        

        public List<TaskDto> GetAllTasks(Guid projectId)
        {
            

            ProjectValidation validation = new ProjectValidation(_projectRepository);
            validation.ValidateProjectId(projectId);

            return _taskRepository.GetAllTasks(projectId);
        }


        public Guid SetTaskStatus(Guid projectId, Guid taskId, Entities.TaskStatus status)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid leaderId = _leaderRepository.GetLeaderId(userId);


            ProjectValidation validation = new ProjectValidation(_projectRepository);
            validation.ValidateProjectId(projectId);

            TaskEntity task = _taskRepository.GetTask(projectId, taskId);

            task.ModifiedBy = leaderId;

            task.SetStatus(status);


            _taskRepository.UpdateTask(task);

            return taskId;
        }
    }
}