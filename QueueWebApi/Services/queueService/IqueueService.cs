using QueueWebApi.Models;
using QueueWebApi.Models.Dtos;
namespace QueueWebApi.Services.queueService
{
    public interface IqueueService
    {
        public Task<Dto<Queue>> getQueue();
        public Task<Dto<Queue>> getNewQueue();
        public Task<Dto<Queue>> clearQueue();
    }
}
