using BloggingAPI.Business.Interfaces;
using BloggingAPI.Data;
using BloggingAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BloggingAPI.Business.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly Context _context;

        public BlogPostService(Context context)
        {
            _context = context;
        }

        public async Task<BlogPost> Create(BlogPost model)
        {
            ArgumentNullException.ThrowIfNull(model);

            if (model.IsValid())
            {
                _context.BlogPosts.Add(model.ToEntity());
                await _context.SaveChangesAsync();

                return model;
            }
            else
                throw new ArgumentException("Invalid blog post");
        }

        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            return await _context.BlogPosts.Include(bp => bp.Comments).Select(bp => bp.ToViewModel()).ToListAsync();
        }

        public async Task<BlogPost?> GetById(int id)
        {
            var blogPost = await _context.BlogPosts.Include(bp => bp.Comments).FirstOrDefaultAsync(bp => bp.Id == id);
            
            return blogPost?.ToViewModel();
        }

        public async Task PostComments(int id, IEnumerable<Comment> comments)
        {
            var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id) ?? throw new ArgumentNullException($"Blog post {id} not found");

            if (comments.Any())
            {
                blogPost.Comments ??= [];

                blogPost.Comments.AddRange(comments.Select(c => c.ToEntity(id)));

                await _context.SaveChangesAsync();
            }
            else 
                throw new ArgumentException("There has to be at least one (1) comment to be added");
        }

        #region NotImplemented

        public Task<BlogPost> Update(BlogPost entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
