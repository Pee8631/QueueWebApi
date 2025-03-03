using QueueWebApi.Models;
using QueueWebApi.Models.Dtos;
using QueueWebApi.Repositories.queueRepository;

namespace QueueWebApi.Services.queueService
{
    public class queueService : IqueueService
    {
        public readonly IqueueRepository queueRepository;
        public queueService(IqueueRepository _queueRepository)
        {
            queueRepository = _queueRepository;
        }
        public async Task<Dto<Queue>> getQueue()
        {
            try
            {
                var countDto = await queueRepository.countQueue();
                if (!countDto.IsStatusOk)
                {
                    return new Dto<Queue>
                    {
                        IsStatusOk = false,
                        Message = countDto.Message,
                    };
                }
                if (countDto.Data == 0)
                {
                    var queueDto = await queueRepository.createQueue("00");
                    if (!countDto.IsStatusOk)
                        return queueDto;

                }
                var queueDto1 = await queueRepository.getQueue();
                if (!countDto.IsStatusOk)
                    return queueDto1;

                return queueDto1;

            }
            catch (Exception ex)
            {
                return new Dto<Queue>
                {
                    IsStatusOk = false,
                    Message = ex.Message,
                };
            }

        }

        public Task<Dto<Queue>> getNewQueue()
        {
            throw new NotImplementedException();
        }

        public Task<Dto<Queue>> clearQueue()
        {
            throw new NotImplementedException();
        }
    }
}
