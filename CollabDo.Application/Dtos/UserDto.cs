using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public UserDto(string username, string email) 
        {
            Username = username;
            Email = email;
        }
    }
}
