using Newtonsoft.Json;

namespace BookStore.API.Dtos.Categories.Resposnes
{
  [JsonObject]
  public class GetCategoryByIdResponse
  {
    [JsonProperty] public int CategoryId { get; set; }
    [JsonProperty] public string CategoryName { get; set; }
  }
}