using Newtonsoft.Json;

namespace BookStore.API.Dtos.Categories.Requests
{
  [JsonObject]
  public class CreateNewCategoryRequest
  {
    [JsonProperty] [JsonRequired] public string CategoryName { get; set; }
  }
}