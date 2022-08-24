using Microsoft.AspNetCore.Mvc;
using schoolOfDevs.Entities;
using schoolOfDevs.Services;

namespace schoolOfDevs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        // injeção de dependencias
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user) => Ok(await _service.Create(user));

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        //recebendo o id pela rota
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetById(id));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            await _service.Update(user);

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