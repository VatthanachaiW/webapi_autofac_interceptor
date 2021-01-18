using Newtonsoft.Json;

namespace BookStore.API.Dtos.Books.Requests
{
  [JsonObject]
  public class CreateNewBookRequest
  {
    [JsonProperty] public string BookName { get; set; }
    [JsonProperty] public int CategoryId { get; set; }
  }
}