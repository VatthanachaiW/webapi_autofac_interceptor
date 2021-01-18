using Newtonsoft.Json;

namespace BookStore.API.Dtos.Books.Resposnes
{
  [JsonObject]
  public class CreateNewBookResponse
  {
    [JsonProperty] public int BookId { get; set; }
    [JsonProperty] public string BookName { get; set; }
    [JsonProperty] public int CategoryId { get; set; }
    [JsonProperty] public string CategoryName { get; set; }
  }
}