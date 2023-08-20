using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public static CommentDto FromModel(CommentEntity entity)
        {
            CommentDto dto = new CommentDto();
            dto.CommentId = entity.Id;
            dto.Author = entity.Author;
            dto.Content = entity.Content;

            return dto;
        }
    }
}
