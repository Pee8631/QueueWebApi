using System.ComponentModel.DataAnnotations;

namespace QueueWebApi.Models
{
    public class Queue
    {
        [Key]
        public int id { get; set; } = 0;
        public string QueueNumber { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } = DateTime.MinValue;
        public DateTime updatedAt { get; set; } = DateTime.MinValue;
    }
}
