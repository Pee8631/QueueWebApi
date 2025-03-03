using QueueWebApi.Models;
using QueueWebApi.Models.Dtos;
using QueueWebApi.Repositories.queueRepository;

namespace QueueWebApi.Services.queueService
{
    public class queueService : IqueueService
    {
        public char[] Alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
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
                    var queueNewDto = await queueRepository.createQueue("00");
                    if (!queueNewDto.IsStatusOk)
                        return queueNewDto;
                }
                var queueDto = await queueRepository.getQueue();
                if (!queueDto.IsStatusOk)
                    return queueDto;

                return queueDto;
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

        public async Task<Dto<Queue>> getNewQueue()
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
                    var queueNewDto = await queueRepository.createQueue("A0");
                    return queueNewDto;
                }

                var queueDto = await queueRepository.getQueue();
                if (!queueDto.IsStatusOk && queueDto.Data == null)
                    return queueDto;
                var qnDto = generateQueueNumber(queueDto.Data!.QueueNumber);
                if (!qnDto.IsStatusOk && qnDto.Data == null)
                {
                    return new Dto<Queue>
                    {
                        IsStatusOk = false,
                        Message = qnDto.Message,
                    };
                }

                queueDto.Data.QueueNumber = qnDto.Data!;

                var queueUpdateDto = await queueRepository.updateQueue(queueDto.Data.QueueNumber);
                if (!queueUpdateDto.IsStatusOk)
                {
                    return queueUpdateDto;
                }
                return queueDto;
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

        public async Task<Dto<Queue>> clearQueue()
        {
            try
            {
                var queueUpdateDto = await queueRepository.updateQueue("00");
                return queueUpdateDto;
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
        private Dto<string> generateQueueNumber(string QueueNumber)
        {
            try
            {
                int letterIndex = -1;
                for (int i = 0; i < Alphabet.Length; i++)
                {
                    if (Alphabet[i] == QueueNumber[0])
                    {
                        letterIndex = i;
                        break;
                    }
                }
                char queueNumberChar = QueueNumber[1];

                if (!int.TryParse(queueNumberChar.ToString(), out int queueNumber))
                {
                    return new Dto<string>
                    {
                        Data = QueueNumber,
                        IsStatusOk = false,
                        Message = "Invalid queue number format: Second character must be a digit."
                    };
                }

                if (letterIndex == -1)
                {
                    letterIndex = 0;
                    queueNumber = 0;
                }
                else if (queueNumber == 9)
                {
                    letterIndex++;
                    if (letterIndex >= Alphabet.Length)
                    {
                        letterIndex = 0;
                    }
                    queueNumber = 0;
                }
                else
                {
                    queueNumber++;
                }

                QueueNumber = Alphabet[letterIndex].ToString() + queueNumber;
                return new Dto<string>
                {
                    Data = QueueNumber,
                    IsStatusOk = true,
                };
            }
            catch (Exception ex)
            {
                return new Dto<string>
                {
                    Data = QueueNumber,
                    IsStatusOk = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
