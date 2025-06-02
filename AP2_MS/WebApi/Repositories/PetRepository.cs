using WebApi.Data;
using Microsoft.EntityFrameworkCore;
public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Pet>> GetAllAsync() => await _context.Pets.Include(p => p.Tutor).ToListAsync();
    public async Task<Pet?> GetByIdAsync(int id) => await _context.Pets.Include(p => p.Tutor).FirstOrDefaultAsync(p => p.Id == id);
    public async Task AddAsync(Pet pet)
    {
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Pet pet)
    {
        _context.Pets.Update(pet);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet != null)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
        }
    }
}