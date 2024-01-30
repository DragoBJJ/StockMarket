using SocialMediaApp.DTO.Comment;
using SocialMediaApp.Models;

namespace SocialMediaApp.Mappers
{
    public static class CommentMappers
    {

        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,

            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto commentModel, int stockId)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = stockId,
            };

        }
    }
}
