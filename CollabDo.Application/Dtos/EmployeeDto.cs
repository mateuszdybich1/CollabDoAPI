using CollabDo.Application.Entities;

namespace CollabDo.Application.Dtos
{
    public class EmployeeDto
    {
        public Guid EmployeeId { get; set; }

        public string LeaderRequestEmail { get; set; }

        public Guid LeaderId { get; set; }

        public static EmployeeDto FromModel(EmployeeEntity entity)
        {
            EmployeeDto dto = new EmployeeDto();
            dto.EmployeeId = entity.Id;
            dto.LeaderRequestEmail = entity.LeaderRequestEmail;
            dto.LeaderId = entity.LeaderId;

            return dto;
        }
    }
}
