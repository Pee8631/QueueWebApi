using Microsoft.EntityFrameworkCore;
using QueueWebApi.Data;
using QueueWebApi.Models;
using QueueWebApi.Models.Dtos;

namespace QueueWebApi.Repositories.queueRepository
{
    public class queueRepository : IqueueRepository
    {
        private readonly DataContext dataContext;
        public queueRepository(DataContext _dataContext)
        {
            dataContext = _dataContext;            
        }
        public async Task<Dto<int>> countQueue()
        {
            try
            {
                var countQ = await dataContext.Queue.CountAsync();
                return new Dto<int>
                {
                    Data = countQ,
                    IsStatusOk = true,
                };
            } catch (Exception ex)
            {
                return new Dto<int>
                {
                    IsStatusOk = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Dto<Queue>> getQueue()
        {
            try
            {
                var queue = await dataContext.Queue.FirstOrDefaultAsync();
                return new Dto<Queue>
                {
                    Data = queue,
                    IsStatusOk = true,
                };
            } catch (Exception ex)
            {
                return new Dto<Queue>
                {
                    IsStatusOk = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Dto<Queue>> createQueue(string queueNumber)
        {
            try
            {
                Queue queue = new Queue
                {
                    QueueNumber = queueNumber,
                    createdAt = DateTimeOffset.Now,
                    updatedAt = DateTimeOffset.Now
                };

                await dataContext.Queue.AddAsync(queue);
                await dataContext.SaveChangesAsync();

                return new Dto<Queue>
                {
                    Data = queue,
                    IsStatusOk = true,
                };
            }
            catch (Exception ex)
            {
                return new Dto<Queue>
                {
                    IsStatusOk = true,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Dto<Queue>> updateQueue(string queueNumber)
        {
            try
            {
                var queue = await dataContext.Queue.FirstOrDefaultAsync();
                if (queue == null)
                {
                   var queueDto = await createQueue(queueNumber);
                    if (!queueDto.IsStatusOk)
                    {
                        return new Dto<Queue>
                        {
                            IsStatusOk = true,
                            Message = queueDto.Message,
                        };
                    }
                    else
                    {
                        queue = queueDto.Data;
                    }
                }

                dataContext.Queue.Update(queue!);
                await dataContext.SaveChangesAsync();

                return new Dto<Queue>
                {
                    Data = queue,
                    IsStatusOk = true,
                };
            }
            catch (Exception ex)
            {
                return new Dto<Queue>
                {
                    IsStatusOk = true,
                    Message = ex.Message,
                };
            }
        }
    }
}
