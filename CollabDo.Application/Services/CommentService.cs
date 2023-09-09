using CollabDo.Application.Dtos;
using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IUserContext userContext, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public async Task<Guid> AddComment(Guid taskId, string content)
        {
            Guid userId = _userContext.CurrentUserId;

            KeycloakUserRequestModel model = await _userRepository.GetUser(userId);

            CommentEntity commentEntity = new CommentEntity(taskId,model.Email,content,userId); 

            _commentRepository.AddComment(commentEntity);

            return commentEntity.Id;
        }

        public async Task<Guid> DeleteComment(Guid taskId, Guid commentId)
        {
            Guid userId = _userContext.CurrentUserId;

            KeycloakUserRequestModel model = await _userRepository.GetUser(userId);

            CommentEntity commentEntity = _commentRepository.GetComment(taskId,commentId);

            if(commentEntity.Author != model.Email)
            {
                throw new ValidationException("Incorrect comment author");
            }

            _commentRepository.DeleteComment(commentEntity);

            return commentEntity.Id;

        }

        public async Task<Guid> EditComment(Guid taskId, Guid commentId, string content)
        {
            Guid userId = _userContext.CurrentUserId;

            KeycloakUserRequestModel model = await _userRepository.GetUser(userId);

            CommentEntity commentEntity = _commentRepository.GetComment(taskId, commentId);

            if (commentEntity.Author != model.Email)
            {
                throw new ValidationException("Incorrect comment author");
            }


            commentEntity.Content = content;
            commentEntity.ModifiedOn = DateTime.UtcNow;
            commentEntity.ModifiedBy = userId;

            _commentRepository.UpdateComment(commentEntity);

            return commentEntity.Id;
        }

        public CommentDto GetLatestComment(Guid taskId, Guid? latestCommentId)
        {

            CommentDto dto = _commentRepository.GetLatestComment(taskId, latestCommentId);

            return dto;
        }

        public List<CommentDto> GetTaskComments(Guid taskId, int pageNumber, DateTime requestDate)
        {
            return _commentRepository.GetTaskComments(taskId, pageNumber,requestDate);
        }
    }
}
