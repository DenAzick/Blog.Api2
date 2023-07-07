using Blog.Api.Managers.BlogManagers;
using Blog.Api.Models.BlogModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly PostManager _postManager;

    public PostsController(PostManager postManager)
    {
        _postManager = postManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        try
        {
            return Ok(await _postManager.GetPosts());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the posts.");
        }
    }

    [HttpGet("blogId")]
    public async Task<IActionResult> GetPosts(Guid blogId)
    {
        try
        {
            return Ok(await _postManager.GetPosts(blogId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the posts.");
        }
    }

    [HttpGet("postId")]
    public async Task<IActionResult> GetPostById(Guid postId)
    {
        try
        {
            return Ok(await _postManager.GetPostById(postId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the post.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostModel model)
    {
        try
        {
            return Ok(await _postManager.CreatePost(model));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the post.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost(Guid postId, CreatePostModel model)
    {
        try
        {
            return Ok(await _postManager.UpdatePost(postId, model));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the post.");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePost(Guid postId)
    {
        try
        {
            return Ok(await _postManager.DeletePost(postId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the post.");
        }
    }

    [HttpGet("GetLikes")]
    public async Task<IActionResult> GetLikes(Guid postId, Guid userId)
    {
        try
        {
            return Ok(await _postManager.GetLikes(postId, userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the likes.");
        }
    }

    [HttpPost("Like")]
    public async Task<IActionResult> Like(Guid postId, Guid userId)
    {
        try
        {
            return Ok(await _postManager.Like(postId, userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while performing the like operation.");
        }
    }

    [HttpGet("isSaved")]
    public async Task<IActionResult> IsSaved(Guid postId, Guid userId)
    {
        try
        {
            return Ok(await _postManager.IsSaved(postId, userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while checking if the post is saved.");
        }
    }


    [HttpGet("GetSavedPosts")]
    public async Task<IActionResult> GetSavedPosts(Guid userId)
    {
        try
        {
            return Ok(await _postManager.GetSavedPosts(userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the saved posts.");
        }
    }

    [HttpPost("savePost")]
    public async Task<IActionResult> SavePost(Guid postId, Guid userId)
    {
        try
        {
            return Ok(await _postManager.SavePost(postId, userId));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the post.");
        }
    }

}