using System;
using System.Threading.Tasks;
using BookStore.API.Dtos.Books.Requests;
using BookStore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class BooksController : ControllerBase
  {
    private IBookService _bookService;

    public BooksController(IBookService bookService)
    {
      _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string name)
    {
      try
      {
        var result = await _bookService.GetAllAsync(name);
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
        var result = await _bookService.GetByIdAsync(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreatAsync(CreateNewBookRequest request)
    {
      try
      {
        var result = await _bookService.CreateNewAsync(request);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateBookRequest request)
    {
      try
      {
        var result = await _bookService.UpdateAsync(id, request);
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
        var result = await _bookService.RemoveAsync(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}