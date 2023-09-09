using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
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

        public List<CommentDto> GetTaskComments(Guid taskId, int pageNumber, DateTime requestDate)
        {
            List<CommentEntity> entities = _appDbContext.Comments
                .Where(e=>e.TaskId == taskId && e.CreatedOn < requestDate)
                .OrderByDescending(e=>e.CreatedOn)
                .Skip((pageNumber - 1) * 5)
                .Take(5)
                .ToList();

            return entities.Select(CommentDto.FromModel).ToList();
        }

        public CommentDto GetLatestComment(Guid taskId, Guid latestCommentId)
        {
            CommentEntity latestComment = _appDbContext.Comments.Where(e => e.TaskId == taskId && e.Id == latestCommentId).SingleOrDefault();
            if(latestComment == null)
            {
                throw new ValidationException("Incorrect last comment Id");
            }
            DateTime latestCommentDate = latestComment.CreatedOn;

            CommentEntity entity = _appDbContext.Comments.Where(e => e.TaskId == taskId && e.CreatedOn > latestCommentDate).OrderBy(e => e.CreatedOn).FirstOrDefault();

            if (entity == null)
            {
                throw new EntityNotFoundException("No new comment");
            }

            return CommentDto.FromModel(entity);
        }
    }
}
