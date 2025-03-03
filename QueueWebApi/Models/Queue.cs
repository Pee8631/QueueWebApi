using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QueueWebApi.Models
{
    public class Queue
    {
        [Key]
        public int id { get; set; } = 0;
        [JsonPropertyName("QueueNumber")]
        public string QueueNumber { get; set; } = string.Empty;
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
    }
}
