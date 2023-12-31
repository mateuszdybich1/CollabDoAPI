﻿namespace CollabDo.Application.Entities
{
    public class EmployeeRequestEntity : Entity<Guid>
    {
        public string Username {get; private set;}

        public string Email { get; private set;}

        public Guid LeaderId { get; private set;}

        public LeaderEntity Leader { get; private set; }

        public EmployeeRequestEntity()
        {

        }

        public EmployeeRequestEntity(Guid leaderId, string username, string email, Guid userId) :base(userId)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            Username = username;
            Email = email;
            LeaderId = leaderId;
        }

    }
}
