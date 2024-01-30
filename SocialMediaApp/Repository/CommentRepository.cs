using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Data;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Models;

namespace SocialMediaApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            this._context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            try
            {
                var res = await _context.Comments.ToListAsync();

                if (res.Count <= 0) return null;
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<Comment> CreateAsync(Comment comment)
        {
            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return comment;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<Comment?> GetByIdAsync(int Id)
        {
            try
            {
                return  await _context.Comments.FindAsync(Id);         
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}