using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface IEmployeeTaskService
    {
        Guid SetTaskState(Guid projectId, Guid taskId, Entities.TaskStatus status);

        List<TaskDto> GetEmployeeTasks(Guid projectId, Entities.TaskStatus status, int pageNumber);
    }
}
