using CollabDo.Application.Entities;
using System.ComponentModel.DataAnnotations;

namespace CollabDo.Application.Dtos
{
    public class EmployeeRequestDto
    {
        public Guid EmployeeRequestId { get; set; }
        
        public string? Username { get; set; }

        
        public string? Email { get; set; }


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
