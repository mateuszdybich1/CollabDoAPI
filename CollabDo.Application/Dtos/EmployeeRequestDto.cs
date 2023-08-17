using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class EmployeeRequestDto
    {
        public Guid EmployeeRequestId { get; private set; }
        [Required]
        public string Username { get; private set; }

        [Required]
        public string Email { get; private set; }


        public static EmployeeRequestDto FromModel(EmployeeRequestEntity entity)
        {
            EmployeeRequestDto dto = new EmployeeRequestDto();
            dto.EmployeeRequestId = entity.Id;
            dto.Username = entity.Username;
            dto.Email = entity.Email;

            return dto;
        }
    }
}
