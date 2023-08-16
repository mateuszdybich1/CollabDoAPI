﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface ITaskRepository
    {
        void AddTask(TaskEntity task);
        TaskEntity GetTask(Guid projectId,Guid taskId);
        void UpdateTask(TaskEntity task);
        void DeleteTask(TaskEntity task);
        List<TaskDto> GetEmplyeesTasks(Guid projectId, Guid EmployeeId);
        List<TaskDto> GetAllTasks(Guid projectId);
        bool TaskExists(Guid leaderId,Guid projectId, Guid taskId);
    }
}