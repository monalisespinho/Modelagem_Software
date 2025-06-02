using WebApi.Data;
using Microsoft.EntityFrameworkCore;

public class TutorRepository : ITutorRepository
{
    private readonly AppDbContext _context;

    public TutorRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Tutor>> GetAllAsync() => await _context.Tutores.Include(t => t.Pets).ToListAsync();
    public async Task<Tutor?> GetByIdAsync(int id) => await _context.Tutores.Include(t => t.Pets).FirstOrDefaultAsync(t => t.Id == id);
    public async Task AddAsync(Tutor tutor)
    {
        _context.Tutores.Add(tutor);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Tutor tutor)
    {
        _context.Tutores.Update(tutor);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var tutor = await _context.Tutores.FindAsync(id);
        if (tutor != null)
        {
            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();
        }
    }
}
