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
        List<TaskDto> GetEmployeeTasks(Guid projectId, Entities.TaskStatus status, int pageNumber);
        Guid SetTaskState(Guid projectId, Guid taskId, Entities.TaskStatus status);
    }
}
