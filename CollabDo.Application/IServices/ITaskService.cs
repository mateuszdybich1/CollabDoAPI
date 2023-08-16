using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IServices
{
    public interface ITaskService
    {
        Guid CreateTask(TaskDto taskDto);
        Task<Guid> AssignToEmployee(Guid projectId,Guid taskId, string employeeEmail);
        Guid SetTaskStatus(Guid projectId,Guid taskId, Entities.TaskStatus status);
        List<TaskDto> GetAllTasks(Guid projectId);

    }
}
