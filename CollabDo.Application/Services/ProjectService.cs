﻿using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using CollabDo.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILeaderRepository _leaderRepository;
        private readonly IUserContext _userContext;

        public ProjectService(IProjectRepository projectRepository, ILeaderRepository leaderRepository, IUserContext userContext)
        {
            _projectRepository = projectRepository;
            _leaderRepository = leaderRepository;
            _userContext = userContext;
        }

        public Guid SaveProject(ProjectDto projectDto)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid leaderId = _leaderRepository.GetLeaderId(userId);

            LeaderValidation validation = new LeaderValidation(_leaderRepository);
            validation.ValidateLeader(leaderId);

            ProjectEntity projectEntity = new ProjectEntity(leaderId,projectDto.Name,projectDto.Priority);

            _projectRepository.AddProject(projectEntity);

            return projectEntity.Id;
        }

        public Guid UpdateProjectState(string projectId)
        {
            Guid userId = _userContext.CurrentUserId;

            Guid leaderId = _leaderRepository.GetLeaderId(userId);


            ProjectValidation projectValidation = new ProjectValidation(_projectRepository);

            projectValidation.ValidateProjectId(leaderId,projectId);


            ProjectEntity projectEntity = _projectRepository.GetProject(Guid.Parse(projectId), leaderId);

            projectEntity.SetProjectStatus(ProjectStatus.Finished);


            _projectRepository.UpdateProject(projectEntity);
            
            return projectEntity.Id;
        }
    }
}