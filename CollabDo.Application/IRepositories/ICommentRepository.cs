using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface ICommentRepository
    {
        void AddComment(CommentEntity commentEntity);

        void UpdateComment(CommentEntity commentEntity);

        void DeleteComment(CommentEntity commentEntity);

        CommentEntity GetComment(Guid taskId, Guid commentId);

        CommentDto GetLatestComment(Guid taskId, Guid latestCommentId);

        List<CommentDto> GetTaskComments(Guid taskId, int pageNumber, DateTime requestDate);
    }
}
