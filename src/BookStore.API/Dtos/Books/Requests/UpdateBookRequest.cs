using Newtonsoft.Json;

namespace BookStore.API.Dtos.Books.Requests
{
  [JsonObject]
  public class UpdateBookRequest
  {
    [JsonProperty] public string BookName { get; set; }
    [JsonProperty] public int CategoryId { get; set; }
  }
}