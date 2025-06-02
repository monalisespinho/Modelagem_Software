using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class TutorController : ControllerBase
{
    private readonly ITutorRepository _repository;

    public TutorController(ITutorRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var tutor = await _repository.GetByIdAsync(id);
        return tutor == null ? NotFound() : Ok(tutor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tutor tutor)
    {
        if (string.IsNullOrWhiteSpace(tutor.Nome) || string.IsNullOrWhiteSpace(tutor.Telefone))
            return BadRequest("Nome e Telefone são obrigatórios.");

        await _repository.AddAsync(tutor);
        return CreatedAtAction(nameof(Get), new { id = tutor.Id }, tutor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tutor tutor)
    {
        if (id != tutor.Id) return BadRequest();
        await _repository.UpdateAsync(tutor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}