using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ITaskService
    {
        Guid CreateTask(TaskDto taskDto);

        Task<Guid> AssignToEmployee(Guid projectId,Guid taskId, string employeeEmail);

        Guid SetTaskStatus(Guid projectId,Guid taskId, Entities.TaskStatus status);

        List<TaskDto> GetAllTasks(Guid projectId, Entities.TaskStatus status, int pageNumber);
    }
}
