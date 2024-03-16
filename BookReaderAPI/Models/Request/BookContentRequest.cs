using System.Text.Json.Serialization;

namespace BookReaderAPI.Models.Request
{
    public class BookContentRequest
    {
        [JsonPropertyName("content")]
        public string Base64Content { get; set; }
    }
}
