using Microsoft.AspNetCore.Mvc;
using schoolOfDevs.Entities;
using schoolOfDevs.Services;

namespace schoolOfDevs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;

        // injeção de dependencias
        public NoteController(INoteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Note Note) => Ok(await _service.Create(Note));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        //recebendo o id pela rota
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Note Note)
        {
            await _service.Update(Note);

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