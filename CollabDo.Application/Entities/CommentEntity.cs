﻿namespace CollabDo.Application.Entities
{
    public class CommentEntity : Entity<Guid>
    { 
        public string Author { get; private set; }
        public string Content { get; set; }
        public Guid TaskId { get; private set; }
        public TaskEntity Task { get; private set; }

        public CommentEntity()
        {

        }

        public CommentEntity(Guid taskId, string author, string content, Guid userId) : base(userId)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException($"'{nameof(author)}' cannot be null or whitespace.", nameof(author));
            }

            Author = author;
            Content = content;
            TaskId = taskId;
        }
    }
}
