using CollabDo.Application.Dtos;

namespace CollabDo.Application.IServices
{
    public interface ICommentService
    {
        Task<Guid> AddComment(Guid taskId, string content);

        Task<Guid> EditComment(Guid taskId, Guid commentId, string content);

        Task<Guid> DeleteComment(Guid taskId, Guid commentId);

        List<CommentDto> GetTaskComments(Guid taskId, int pageNumber, DateTime requestDate);

        CommentDto GetLatestComment(Guid taskId, Guid latestCommentId);
    }
}
