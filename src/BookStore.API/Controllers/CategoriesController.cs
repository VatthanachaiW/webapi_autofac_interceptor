using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BookStore.API.Dtos.Categories.Requests;
using BookStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string name)
    {
      try
      {
        var result = await _categoryService.GetAllAsync(name);
        return Ok(result);
      }
      catch (Exception exception)
      {
        return BadRequest(exception);
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      try
      {
        var result = await _categoryService.GetByIdAsync(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreatAsync(CreateNewCategoryRequest request)
    {
      try
      {
        var result = await _categoryService.CreateNewAsync(request);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
      try
      {
        var result = await _categoryService.UpdateAsync(id, request);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
      try
      {
        var result = await _categoryService.RemoveAsync(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}