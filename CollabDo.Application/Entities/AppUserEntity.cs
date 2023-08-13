using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Entities
{
    public class AppUserEntity : Entity
    {
        public Guid UserId { get; private set; }

        public string Username { get; private set; }

        public AppUserEntity() { }

        public AppUserEntity(Guid userId, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            UserId = userId;
            Username = username;
        }

    }
}
