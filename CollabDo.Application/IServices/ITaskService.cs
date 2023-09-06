using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ITaskService
    {
        Task<Guid> CreateTask(TaskDto taskDto);

        Guid SetTaskStatus(Guid projectId, Guid taskId, bool isLeader, Entities.TaskStatus status);

        Guid DeleteTask(Guid projectId,Guid taskId);

        List<TaskDto> GetEmployeeTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber);

        List<TaskDto> GetLeaderTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber);

        Task<List<TaskDto>> GetAllTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber);
    }
}
