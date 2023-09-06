using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;

namespace CollabDo.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        

        public TaskService(
            IUserContext userContext, 
            IUserRepository userRepository, 
            ILeaderRepository leaderRepository,
            IEmployeeRepository employeeRepository, 
            IProjectRepository projectRepository, 
            ITaskRepository taskRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _leaderRepository = leaderRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }
        public async Task<Guid> CreateTask(TaskDto taskDto)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(leaderId);

            
            ProjectValidation validation = new ProjectValidation(_projectRepository);
            validation.ValidateProjectId(taskDto.ProjectId);

            Guid assignUserId = await _userRepository.GetUserIdByEmail(taskDto.UserEmail);

           
            TaskEntity taskEntity = new TaskEntity(taskDto.ProjectId,taskDto.Name,taskDto.Description,taskDto.Priority,taskDto.Deadline, userId);

            taskEntity.AssignToEmployee(assignUserId);

            _taskRepository.AddTask(taskEntity);

            return taskEntity.Id;
        }


        public Guid SetTaskStatus(Guid projectId, Guid taskId, bool isLeader, Entities.TaskStatus status)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid memberId;
            if(isLeader)
            {
                memberId = _leaderRepository.GetLeaderId(userId);
                LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
                leaderValidation.ValidateLeader(memberId);
            }
            else
            {
                memberId = _employeeRepository.GetEmployeeId(userId);
                EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
                employeeValidation.ValidateEmployeeId(memberId);
            }
            


            ProjectValidation projectIdValidation = new ProjectValidation(_projectRepository);
            projectIdValidation.ValidateProjectId(projectId);

            TaskEntity task = _taskRepository.GetTask(projectId, taskId);

            task.ModifiedBy = memberId;
            task.ModifiedOn = DateTime.UtcNow;
            task.SetStatus(status);

            _taskRepository.UpdateTask(task);

            return taskId;
        }


        public Guid DeleteTask(Guid projectId, Guid taskId)
        {
            Guid userId = _userContext.CurrentUserId;

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(userId);

            TaskEntity taskEntity = _taskRepository.GetTask(projectId,taskId);
            
            _taskRepository.DeleteTask(taskEntity);

            return taskEntity.Id;
        }

        public List<TaskDto> GetEmployeeTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid employeeId = _employeeRepository.GetEmployeeId(userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(employeeId);

            ProjectValidation projectValidation = new ProjectValidation(_projectRepository);
            projectValidation.ValidateProjectId(projectId);

            return _taskRepository.GetUserTasks(projectId, userId, requestDate, status, pageNumber);

        }

        public List<TaskDto> GetLeaderTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation leaderValidation = new LeaderValidation(_leaderRepository);
            leaderValidation.ValidateLeader(leaderId);

            return _taskRepository.GetUserTasks(projectId, userId, requestDate, status, pageNumber);

        }

        public async Task<List<TaskDto>> GetAllTasks(Guid projectId, DateTime requestDate, Entities.TaskStatus status, int pageNumber)
        {
            ProjectValidation validation = new ProjectValidation(_projectRepository);
            validation.ValidateProjectId(projectId);

            List<TaskDto> tasks = _taskRepository.GetAllTasks(projectId, requestDate, status,pageNumber);

            for(int i = 0; i < tasks.Count; i++)
            {
                KeycloakUserRequestModel model = await _userRepository.GetUser((Guid)tasks[i].AssignedId);
                tasks[i].UserEmail = model.Email;
            }

            return tasks;
        }


        
    }
}
