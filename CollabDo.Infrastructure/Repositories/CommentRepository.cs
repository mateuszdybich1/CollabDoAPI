using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.IRepositories;

namespace CollabDo.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddComment(CommentEntity commentEntity)
        {
            _appDbContext.Comments.Add(commentEntity);
            _appDbContext.SaveChanges();
        }

        public void DeleteComment(CommentEntity commentEntity)
        {
            _appDbContext.Comments.Remove(commentEntity);
            _appDbContext.SaveChanges();
        }

        public void UpdateComment(CommentEntity commentEntity)
        {
            _appDbContext.Comments.Update(commentEntity);
            _appDbContext.SaveChanges();
        }

        public CommentEntity GetComment(Guid taskId, Guid commentId)
        {
            return _appDbContext.Comments.SingleOrDefault(e => e.TaskId == taskId && e.Id == commentId);
        }

        public List<CommentDto> GetTaskComments(Guid taskId, int pageNumber)
        {
            List<CommentEntity> entities = _appDbContext.Comments
                .Where(e=>e.TaskId == taskId)
                .Skip((pageNumber - 1) * 25)
                .Take(25)
                .ToList();

            return entities.Select(CommentDto.FromModel).ToList();
        }
    }
}
