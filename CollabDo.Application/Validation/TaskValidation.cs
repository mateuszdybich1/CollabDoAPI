using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;

namespace CollabDo.Application.Validation
{
    public class TaskValidation
    {
        private readonly ITaskRepository _taskRepository;

        public TaskValidation(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public void ValidateTask(Guid leaderId, Guid projectId, Guid taskId)
        {
            if(!_taskRepository.TaskExists(leaderId, projectId, taskId))
            {
                throw new ValidationException("Incorrect task provided");
            }
        }
    }
}
