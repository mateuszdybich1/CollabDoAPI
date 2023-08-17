using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
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
    public class EmployeeTaskService : IEmployeeTaskService
    {
        private readonly IUserContext _userContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public EmployeeTaskService(IUserContext userContext, IEmployeeRepository employeeRepository, IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _userContext = userContext;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public List<TaskDto> GetEmployeeTasks(Guid projectId, Entities.TaskStatus status, int pageNumber)
        {
            Guid userId = _userContext.CurrentUserId;

            
            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(employeeId);

            ProjectValidation projectValidation = new ProjectValidation(_projectRepository);
            projectValidation.ValidateProjectId(projectId);

            return _taskRepository.GetEmplyeesTasks(projectId, employeeId,status,pageNumber);

        }

        public Guid SetTaskState(Guid projectId, Guid taskId, Entities.TaskStatus status)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(employeeId);

            ProjectValidation projectIdValidation = new ProjectValidation(_projectRepository);
            projectIdValidation.ValidateProjectId(projectId);

            TaskEntity task = _taskRepository.GetTask(projectId, taskId);

            task.ModifiedBy = employeeId;
            task.ModifiedOn = DateTime.UtcNow;
            task.SetStatus(status);
            
            _taskRepository.UpdateTask(task);
            return taskId;
        }
    }
}
