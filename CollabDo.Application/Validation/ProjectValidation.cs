using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Validation
{
    public class ProjectValidation
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectValidation(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public void ValidateProjectId(string projectId)
        {
            if (!_projectRepository.ProjectExists(Guid.Parse(projectId)))
            {
                throw new ValidationException("Incorrect project ID");
            }
        }
    }
}
