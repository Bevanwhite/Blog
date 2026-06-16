
using Blog.Models;

namespace Blog.Data.Repository;

public interface IRepository
{
    Post GetPost(int id);
    List<Post> GetAllPosts();
    List<Post> GetAllPosts(string category);
    void AddPost(Post post);
    void RemovePost(int id);
    void UpdatePost(Post post);

    Task<bool> SaveChangesAsync();
}