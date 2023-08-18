using System.ComponentModel.DataAnnotations;

namespace CollabDo.Application.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsLeader {get; set; }
    }
}
