using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.API.Dtos.Categories.Requests;
using BookStore.API.Dtos.Categories.Resposnes;

namespace BookStore.API.Services
{
  public interface ICategoryService : IDisposable
  {
    Task<IList<GetAllCategoryResponse>> GetAllAsync(string name);
    Task<GetCategoryByIdResponse> GetByIdAsync(int id);
    Task<CreateNewCategoryResponse> CreateNewAsync(CreateNewCategoryRequest request);
    Task<UpdateCategoryResponse> UpdateAsync(int id, UpdateCategoryRequest request);
    Task<bool> RemoveAsync(int id);
  }
}