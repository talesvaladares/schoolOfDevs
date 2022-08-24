using Microsoft.EntityFrameworkCore;
using schoolOfDevs.Entities;
using schoolOfDevs.Helpers;

namespace schoolOfDevs.Services { 

    public interface INoteService {
        public Task<List<Note>> GetAll();
        public Task<Note> Create(Note Note);
        public Task<Note> GetById(int id);
        public Task Update(Note Note);
        public Task Delete(int id);

    }

    public class NoteService : INoteService
    {

        private readonly DataContext _context;

        public NoteService(DataContext context)
        {
            _context = context;
        }

        public async Task<Note> Create(Note Note)
        {
          
            _context.Notes.Add(Note);
            await _context.SaveChangesAsync();

            return Note;
        }

        public async Task Delete(int id)
        {
            Note NoteDb = await _context.Notes.SingleOrDefaultAsync(c => c.Id == id);

            if (NoteDb is null)
            {
                throw new KeyNotFoundException($"Note {id} not found");
            }

            _context.Notes.Remove(NoteDb);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetAll() => await _context.Notes.ToListAsync();

        public async Task<Note> GetById(int id)
        {
            Note NoteDb = await _context.Notes
               .SingleOrDefaultAsync(c => c.Id == id);

            if (NoteDb is null)
            {
                throw new KeyNotFoundException($"Note {id} not found");
            }

            return NoteDb;
        }

        public async Task Update(Note Note)
        {
           
            Note NoteDb = await _context.Notes
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == Note.Id);

            if (NoteDb is null)
            {
                throw new KeyNotFoundException($"Note {Note.Id} not found");
            }

            Note.CreatedAt = NoteDb.CreatedAt;

            _context.Entry(Note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
