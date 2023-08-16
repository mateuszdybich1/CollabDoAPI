﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _appDbContext;

        public TaskRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddTask(TaskEntity task)
        {
            _appDbContext.Tasks.Add(task);
            _appDbContext.SaveChanges();
        }

        public void DeleteTask(TaskEntity task)
        {
            _appDbContext.Tasks.Remove(task);
            _appDbContext.SaveChanges();
        }

        public List<TaskDto> GetAllTasks(Guid projectId)
        {
            List<TaskEntity> entities = _appDbContext.Tasks.Where(e => e.ProjectID == projectId).ToList();
            return entities.Select(TaskDto.FromModel).ToList();
        }

        public List<TaskDto> GetEmplyeesTasks(Guid projectId, Guid EmployeeId)
        {
            List<TaskEntity> entities = _appDbContext.Tasks.Where(e => e.ProjectID == projectId && e.AssignedToEmployeeId == EmployeeId).ToList();
            return entities.Select(TaskDto.FromModel).ToList();
        }

        public TaskEntity GetTask(Guid projectId,Guid taskId)
        {
            return _appDbContext.Tasks.SingleOrDefault(e => e.Id == taskId && e.ProjectID == projectId);
        }

        public bool TaskExists(Guid leaderId,Guid projectId, Guid taskId)
        {
            return _appDbContext.Tasks.Include(e=>e.Project)
                .Any(e=>e.Id == taskId && e.ProjectID == projectId && e.Project.LeaderId == leaderId);
        }

        public void UpdateTask(TaskEntity task)
        {
            _appDbContext.Tasks.Update(task);
            _appDbContext.SaveChanges();
        }
    }
}