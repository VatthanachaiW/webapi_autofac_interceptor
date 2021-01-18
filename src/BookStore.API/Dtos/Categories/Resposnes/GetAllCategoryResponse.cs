using Newtonsoft.Json;

namespace BookStore.API.Dtos.Categories.Resposnes
{
  public class GetAllCategoryResponse
  {
    [JsonProperty] public int CategoryId { get; set; }
    [JsonProperty] public string CategoryName { get; set; }
  }
}