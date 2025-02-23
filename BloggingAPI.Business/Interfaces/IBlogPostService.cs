using BloggingAPI.Models.ViewModels;

namespace BloggingAPI.Business.Interfaces
{
    public interface IBlogPostService : IBaseService<BlogPost> 
    {
        public Task PostComments(int id, IEnumerable<Comment> comments);
    }
}
