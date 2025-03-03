using Microsoft.AspNetCore.Mvc;
using QueueWebApi.Models;
using QueueWebApi.Repositories.queueRepository;
using QueueWebApi.Services.queueService;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class queueController : ControllerBase
    {
        public readonly IqueueService queueService;
        public queueController(IqueueService _queueService)
        {
            queueService = _queueService;
        }
        // GET: api/<queueController>
        [HttpGet]
        public async Task<ActionResult<Queue>> Get()
        {
             var queueDto = await queueService.getQueue();
            if (queueDto.IsStatusOk && queueDto.Data != null)
                return Ok(queueDto.Data);
            else
                return StatusCode(500, queueDto.Message);
        }
        // GET: api/<queueController>
        [HttpGet("new")]
        public async Task<ActionResult<Queue>> GetNew()
        {
            var queueDto = await queueService.getNewQueue();
            if (queueDto.IsStatusOk && queueDto.Data != null)
                return Ok(queueDto.Data);
            else
                return StatusCode(500, queueDto.Message);
        }

        // GET: api/<queueController>
        [HttpGet("clear")]
        public async Task<IActionResult> Clear()
        {
            var queueDto = await queueService.clearQueue();
            if (queueDto.IsStatusOk && queueDto.Data != null)
                return Ok(new { message = "ล้างคิวสำเร็จ" });
            else
                return StatusCode(500, queueDto.Message);
        }
    }
}
