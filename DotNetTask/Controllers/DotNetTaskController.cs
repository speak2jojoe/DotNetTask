using DotNetTask.DTOs;
using DotNetTask.Interfaces;
using DotNetTask.Models;
using DotNetTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DotNetTaskController : ControllerBase
    {
        private readonly IDotNetTaskService _dotNetTaskService;

        public DotNetTaskController(IDotNetTaskService dotNetTaskService)
        {
            _dotNetTaskService = dotNetTaskService;
        }


        // POST api/<DotNetTaskController>
        [HttpPost]
        public IActionResult Add([FromBody] List<string> QuestionTypes)
        {
            try
            {
                var result = _dotNetTaskService.CreateQuestionTypeAsync(QuestionTypes);

                return Ok("Question type(s) added successfully");                    
            }
            catch (Exception ex)
            {
                return BadRequest($"Sorry an error has occured : {ex.Message}");
            }
        }

        // GET: api/<DotNetTaskController>
        [HttpGet]
        public async Task<IEnumerable<QuestionTypeDto>> GetAll()
        {
            try
            {
                var result = await _dotNetTaskService.GetQuestionTypesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<DotNetTaskController>/5
        [HttpGet("{id}")]
        public async Task<QuestionTypeDto> Get(Guid id)
        {
            try
            {
                var result = await _dotNetTaskService.GetQuestionTypeAsync(id);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<DotNetTaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] string Type)
        {
            try
            {
                var result = await _dotNetTaskService.UpdateQuestionTypeAsync(id, Type);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Sorry an error has occured : {ex.Message}");
            }
        }

        // DELETE api/<DotNetTaskController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _dotNetTaskService.DeleteQuestionTypeAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Sorry an error has occured : {ex.Message}");
            }
        }
    }
}
