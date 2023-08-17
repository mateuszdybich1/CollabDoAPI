using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class ProjectDto
    {
        public Guid ProjectId { get; private set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,3)]
        public Priority Priority { get; set; }

        


        public static ProjectDto FromModel(ProjectEntity entity)
        {
            ProjectDto dto = new ProjectDto();
            dto.ProjectId = entity.Id;
            dto.Name = entity.Name;
            dto.Priority = entity.Priority;
            return dto;
        }
    }
}
