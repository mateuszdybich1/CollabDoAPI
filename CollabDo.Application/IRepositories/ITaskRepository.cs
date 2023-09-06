using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;

namespace CollabDo.Application.IRepositories
{
    public interface ITaskRepository
    {
        void AddTask(TaskEntity task);

        void UpdateTask(TaskEntity task);

        void DeleteTask(TaskEntity task);

        TaskEntity GetTask(Guid projectId, Guid taskId);

        List<TaskDto> GetUserTasks(Guid projectId, Guid assignedId, DateTime requestDate, Entities.TaskStatus status, int pageNumber);

        List<TaskDto> GetAllTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber);

        bool TaskExists(Guid leaderId,Guid projectId, Guid taskId);
    }
}
