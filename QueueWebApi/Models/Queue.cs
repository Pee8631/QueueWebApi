using System.ComponentModel.DataAnnotations;

namespace QueueWebApi.Models
{
    public class Queue
    {
        [Key]
        public int id { get; set; } = 0;
        public string QueueNumber { get; set; } = string.Empty;
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updatedAt { get; set; }
    }
}
