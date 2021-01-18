using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.API.Dtos.Books.Requests;
using BookStore.API.Dtos.Books.Resposnes;

namespace BookStore.API.Services
{
  public interface IBookService : IDisposable
  {
    Task<IList<GetAllBookResponse>> GetAllAsync(string name);
    Task<GetBookByIdResponse> GetByIdAsync(int id);
    Task<CreateNewBookResponse> CreateNewAsync(CreateNewBookRequest request);
    Task<UpdateBookResponse> UpdateAsync(int id, UpdateBookRequest request);
    Task<bool> RemoveAsync(int id);
  }
}