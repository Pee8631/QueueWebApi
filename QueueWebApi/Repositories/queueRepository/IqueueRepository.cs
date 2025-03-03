using QueueWebApi.Models;
using QueueWebApi.Models.Dtos;

namespace QueueWebApi.Repositories.queueRepository
{
    public interface IqueueRepository
    {
        public Task<Dto<int>> countQueue();
        public Task<Dto<Queue>> getQueue();
        public Task<Dto<Queue>> createQueue(string queueNumber);
        public Task<Dto<Queue>> updateQueue(string queueNumber);
    }
}
