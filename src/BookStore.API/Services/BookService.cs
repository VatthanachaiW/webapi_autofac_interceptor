using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Dtos.Books.Requests;
using BookStore.API.Dtos.Books.Resposnes;
using BookStore.API.Models;
using BookStore.API.UnitOfWorks;

namespace BookStore.API.Services
{
  public class BookService : IBookService
  {
    private bool _disposed;
    private IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      _disposed = true;

      _unitOfWork.Dispose();
    }

    public async Task<IList<GetAllBookResponse>> GetAllAsync(string name)
    {
      var result = await _unitOfWork.BookRepository.ToListAsync();
      if (!string.IsNullOrWhiteSpace(name))
      {
        result = result.Where(s => s.Name.Contains(name)).ToList();
      }

      return result.Select(s => new GetAllBookResponse
      {
        BookId = s.Id,
        BookName = s.Name,
        CategoryId = s.CategoryId,
        CategoryName = s.BookCategory.Name
      }).ToList();
    }

    public async Task<GetBookByIdResponse> GetByIdAsync(int id)
    {
      var result = await _unitOfWork.BookRepository.FirstOfDefaultAsync(s => s.Id == id);
      if (result == null) throw new Exception("Data not found");
      return new GetBookByIdResponse
      {
        BookId = result.Id,
        BookName = result.Name,
        CategoryId = result.CategoryId,
        CategoryName = result.BookCategory.Name
      };
    }

    public async Task<CreateNewBookResponse> CreateNewAsync(CreateNewBookRequest request)
    {
      try
      {
        if (request == null) throw new Exception("Invalid Argument");

        var existCategory = await _unitOfWork.CategoryRepository.FirstOfDefaultAsync(s => s.Id == request.CategoryId);
        if (existCategory == null) throw new Exception("BookCategory does not exist");
        var newRequest = new Book
        {
          Name = request.BookName,
          BookCategory = existCategory
        };

        await _unitOfWork.BookRepository.Add(newRequest);

        if (await _unitOfWork.CommitAsync())
        {
          return new CreateNewBookResponse
          {
            BookId = newRequest.Id,
            BookName = newRequest.Name,
            CategoryId = newRequest.CategoryId,
            CategoryName = newRequest.BookCategory.Name
          };
        }

        throw new Exception("Data invalid");
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<UpdateBookResponse> UpdateAsync(int id, UpdateBookRequest request)
    {
      try
      {
        var exist = await _unitOfWork.BookRepository.FirstOfDefaultAsync(s => s.Id == id);
        var existCategory = await _unitOfWork.CategoryRepository.FirstOfDefaultAsync(s => s.Id == request.CategoryId);
        if (existCategory == null) throw new Exception("BookCategory does not exist");

        if (exist == null) throw new Exception("Data not found");

        exist.Name = request.BookName;
        exist.BookCategory = existCategory;

        _unitOfWork.BookRepository.Update(exist);
        if (await _unitOfWork.CommitAsync())
        {
          return new UpdateBookResponse()
          {
            CategoryId = exist.Id,
            CategoryName = exist.Name
          };
        }

        throw new Exception("Data invalid");
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<bool> RemoveAsync(int id)
    {
      try
      {
        var result = await _unitOfWork.BookRepository.FirstOfDefaultAsync(s => s.Id == id);
        if (result == null) throw new Exception("Data not found");

        _unitOfWork.BookRepository.Remove(result);
        if (await _unitOfWork.CommitAsync())
        {
          return true;
        }

        throw new Exception("Data invalid");
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}