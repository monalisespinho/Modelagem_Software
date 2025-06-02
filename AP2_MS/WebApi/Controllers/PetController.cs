using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PetController : ControllerBase
{
    private readonly IPetRepository _repository;

    public PetController(IPetRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var pet = await _repository.GetByIdAsync(id);
        return pet == null ? NotFound() : Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pet pet)
    {
        if (string.IsNullOrWhiteSpace(pet.Nome))
            return BadRequest("Nome do pet é obrigatório.");

        await _repository.AddAsync(pet);
        return CreatedAtAction(nameof(Get), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pet pet)
    {
        if (id != pet.Id) return BadRequest();
        await _repository.UpdateAsync(pet);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}