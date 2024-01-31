using SocialMediaApp.Models;

namespace SocialMediaApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int Id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> UpdateAsync(int Id, Comment comment);
        Task<Comment?> DeleteAsync(int Id);

    }
}
