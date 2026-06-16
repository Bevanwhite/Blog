using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers;

[Authorize(Roles = "Admin")]
public class PanelController : Controller
{
    public IRepository _repo;
    public IFileManager _fileManager;

    public PanelController(
        IRepository repo,
        IFileManager fileManager)
    {
        _repo = repo;
        _fileManager = fileManager;
    }

    public IActionResult Index() =>
        View(_repo.GetAllPosts());


    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return View(new PostViewModel());
        else
        {
            var post = _repo.GetPost((int)id);
            return View(new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Current_Image = post.Image,
                Description = post.Description,
                Tags = post.Tags,
                Category = post.Category,
            });
        }

    }

    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel postViewModel)
    {

        var post = new Post
        {
            Id = postViewModel.Id,
            Title = postViewModel.Title,
            Body = postViewModel.Body,
            Description = postViewModel.Description,
            Tags = postViewModel.Tags,
            Category = postViewModel.Category,
        };

        if (postViewModel.Image == null)
            post.Image = postViewModel.Current_Image;
        else
            post.Image = await _fileManager.SaveImage(postViewModel.Image);

        if (post.Id > 0)
            _repo.UpdatePost(post);
        else
            _repo.AddPost(post);

        if (await _repo.SaveChangesAsync())
            return RedirectToAction("Index");
        else
            return View(post);
    }

    public async Task<IActionResult> Remove(int id)
    {
        _repo.RemovePost(id);
        await _repo.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}