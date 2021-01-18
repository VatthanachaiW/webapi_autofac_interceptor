using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Dtos.Categories.Requests;
using BookStore.API.Dtos.Categories.Resposnes;
using BookStore.API.Models;
using BookStore.API.UnitOfWorks;

namespace BookStore.API.Services
{
  public class CategoryService : ICategoryService
  {
    private bool _disposed;
    private IUnitOfWork _unitOfWork;

    ~CategoryService()
    {
      Dispose();
    }

    public CategoryService(IUnitOfWork unitOfWork)
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


    public async Task<IList<GetAllCategoryResponse>> GetAllAsync(string name)
    {
      var response = await _unitOfWork.CategoryRepository.ToListAsync();
      if (!string.IsNullOrWhiteSpace(name))
      {
        response = response.Where(s => s.Name.Contains(name)).ToList();
      }

      return response.Select(s => new GetAllCategoryResponse
      {
        CategoryId = s.Id,
        CategoryName = s.Name
      }).ToList();
    }

    public async Task<GetCategoryByIdResponse> GetByIdAsync(int id)
    {
      var response = await _unitOfWork.CategoryRepository.FirstOfDefaultAsync(s => s.Id == id);
      if (response == null) throw new Exception("Data not founc");
      return new GetCategoryByIdResponse
      {
        CategoryId = response.Id,
        CategoryName = response.Name
      };
    }

    public async Task<CreateNewCategoryResponse> CreateNewAsync(CreateNewCategoryRequest request)
    {
      try
      {
        if (request == null) throw new Exception("Invalid Argument");
        var newRequest = new BookCategory
        {
          Name = request.CategoryName
        };

        await _unitOfWork.CategoryRepository.Add(newRequest);
        if (await _unitOfWork.CommitAsync())
        {
          return new CreateNewCategoryResponse
          {
            CategoryId = newRequest.Id,
            CategoryName = newRequest.Name
          };
        }

        throw new Exception("Data invalid");
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<UpdateCategoryResponse> UpdateAsync(int id, UpdateCategoryRequest request)
    {
      try
      {
        var exist = await _unitOfWork.CategoryRepository.FirstOfDefaultAsync(s => s.Id == id);
        if (exist == null) throw new Exception("Data not found");
        exist.Name = request.CategoryName;
        _unitOfWork.CategoryRepository.Update(exist);
        if (await _unitOfWork.CommitAsync())
        {
          return new UpdateCategoryResponse
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
        var result = await _unitOfWork.CategoryRepository.FirstOfDefaultAsync(s => s.Id == id);
        if (result == null) throw new Exception("Data not found");

        _unitOfWork.CategoryRepository.Remove(result);
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