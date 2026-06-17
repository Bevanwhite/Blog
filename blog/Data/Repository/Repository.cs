using System.Linq.Expressions;
using Blog.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Repository;

public class Repository : IRepository
{
    private readonly ApplicationDbContext _repo;

    public Repository(ApplicationDbContext repo)
    {
        _repo = repo;
    }
    public void AddPost(Post post)
    {
        _repo.Posts.Add(post);
    }

    public List<Post> GetAllPosts()
    {
        return _repo.Posts.ToList();
    }

    public List<Post> GetAllPosts(string category)
    {
        Expression<Func<Post, bool>> InCategory =
        (post) => post.Category.ToLower().Equals(category.ToLower());

        return _repo.Posts
            .Where(InCategory)
            .ToList();
    }

    public Post GetPost(int id)
    {
        return _repo.Posts.FirstOrDefault(post => post.Id == id);
    }

    public void RemovePost(int id)
    {
        _repo.Posts.Remove(GetPost(id));
    }

    public void UpdatePost(Post post)
    {
        _repo.Posts.Update(post);
    }

    public async Task<bool> SaveChangesAsync()
    {
        if (await _repo.SaveChangesAsync() > 0)
        {
            return true;
        }

        return false;
    }
}