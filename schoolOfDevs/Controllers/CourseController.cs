using Microsoft.AspNetCore.Mvc;
using schoolOfDevs.Entities;
using schoolOfDevs.Services;

namespace schoolOfDevs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        // injeção de dependencias
        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course) => Ok(await _service.Create(course));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        //recebendo o id pela rota
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Course course)
        {
            await _service.Update(course);

            // confirmação que foi atualizado
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }

    }
}