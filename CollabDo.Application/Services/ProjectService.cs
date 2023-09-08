using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;

namespace CollabDo.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserContext _userContext;

        public ProjectService(IProjectRepository projectRepository, ILeaderRepository leaderRepository, IEmployeeRepository employeeRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _leaderRepository = leaderRepository;
            _employeeRepository = employeeRepository;
            _userContext = userContext;
        }

        
        public Guid SaveProject(ProjectDto projectDto)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation validation = new LeaderValidation(_leaderRepository);
            validation.ValidateLeader(userId);

            ProjectEntity projectEntity = new ProjectEntity(leaderId, projectDto.Name,projectDto.Priority, userId);

            _projectRepository.AddProject(projectEntity);

            return projectEntity.Id;
        }

        public Guid UpdateProjectState(Guid projectId)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation validation = new LeaderValidation(_leaderRepository);
            validation.ValidateLeader(userId);

            ProjectValidation projectValidation = new ProjectValidation(_projectRepository);
            projectValidation.ValidateProjectId(projectId);

            ProjectEntity projectEntity = _projectRepository.GetProject(projectId, leaderId);
            projectEntity.ModifiedBy = userId;
            projectEntity.ModifiedOn = DateTime.UtcNow;

            projectEntity.SetProjectStatus(ProjectStatus.Finished);

            _projectRepository.UpdateProject(projectEntity);
            
            return projectEntity.Id;
        }

        public List<ProjectDto> GetProjects(ProjectStatus status, int pageNumber, DateTime requestDate)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation validation = new LeaderValidation(_leaderRepository);
            validation.ValidateLeader(userId);

            return _projectRepository.GetLeaderProjects(leaderId, status, pageNumber, requestDate);
        }

        public List<ProjectDto> GetProjects(Guid? leaderId, ProjectStatus status, int pageNumber, DateTime requestDate)
        {
            Guid userId = _userContext.CurrentUserId;
            
            Guid employeeId = _employeeRepository.GetEmployeeId((Guid)leaderId,userId);

            EmployeeValidation employeeValidation = new EmployeeValidation(_employeeRepository);
            employeeValidation.ValidateEmployeeId(userId);

            return _projectRepository.GetLeaderProjects((Guid)leaderId, status, pageNumber,requestDate);
        }

        public Guid DeleteProject(Guid projectId)
        {
            Guid userId = _userContext.CurrentUserId;
            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation validation = new LeaderValidation(_leaderRepository);
            validation.ValidateLeader(userId);

            ProjectValidation projectValidation = new ProjectValidation(_projectRepository);
            projectValidation.ValidateProjectId(projectId);

            ProjectEntity projectEntity = _projectRepository.GetProject(projectId, leaderId);

            _projectRepository.DeleteProject(projectEntity);

            return projectEntity.Id;
        }
    }
}
