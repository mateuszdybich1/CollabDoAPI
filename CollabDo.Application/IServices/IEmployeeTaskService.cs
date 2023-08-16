using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IServices
{
    public interface IEmployeeTaskService
    {
        List<TaskDto> GetEmployeeTasks(Guid projectId);
        Guid SetTaskState(Guid projectId, Guid taskId, Entities.TaskStatus status);
    }
}
