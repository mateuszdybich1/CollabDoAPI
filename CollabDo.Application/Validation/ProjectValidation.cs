using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;

namespace CollabDo.Application.Validation
{
    public class ProjectValidation
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectValidation(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        public void ValidateProjectId(Guid projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
            {
                throw new ValidationException("Incorrect project ID");
            }
        }
    }
}
