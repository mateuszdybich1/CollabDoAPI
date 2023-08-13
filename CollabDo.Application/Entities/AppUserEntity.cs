using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public enum UserRole
    {
        User,
        GoupLeader
    }
    public class AppUserEntity : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public string Username { get; private set; }

        public UserRole Role { get; private set; }

        public List<ProjectEntity> Projects { get; set; }

        public AppUserEntity() 
        { 
        }

        public AppUserEntity(Guid userId, string username, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            UserId = userId;
            Username = username;
            Role = role;
        }

    }
}
