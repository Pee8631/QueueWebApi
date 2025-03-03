namespace QueueWebApi.Models.Dtos
{
    public class Dto<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool IsStatusOk { get; set; }
    }
}
