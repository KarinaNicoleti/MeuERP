using Microsoft.AspNetCore.Mvc;
using ERPWeb.Models;
using ERPWeb.Services;

namespace ERPWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _service;

        // Use injeção de dependência em vez de instanciar diretamente
        public ClientesController(ClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetAll()
        {
            var clientes = _service.Listar();
            return Ok(clientes);
        }

        [HttpPost]
        public ActionResult<Cliente> Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados inválidos");

            var novo = _service.Criar(cliente);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _service.BuscarPorId(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> Update(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || id != cliente.Id)
                return BadRequest("Dados inválidos");

            var atualizado = _service.Atualizar(id, cliente);
            if (atualizado == null)
                return NotFound("Cliente não encontrado");

            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sucesso = _service.Deletar(id);
            if (!sucesso)
                return NotFound("Cliente não encontrado");

            return NoContent(); // Melhor prática para DELETE
        }
    }
}