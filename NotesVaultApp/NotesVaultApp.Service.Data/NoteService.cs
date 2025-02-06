using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NotesVaultApp.Data.Models;
using NotesVaultApp.Data.Models.Enums;
using NotesVaultApp.Data.Repository.Interface;
using NotesVaultApp.DTOs;
using NotesVaultApp.DTOs.Note_DTOs;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Service.Data
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(IRepository<Note> noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllAttached()
                .Include(n => n.Category)
                .ToArrayAsync();

            var notesToMap = _mapper.Map<IEnumerable<NoteDto>>(notes);
            foreach (var note in notesToMap)
            {
                var originalNote = notes.First(n => n.Id == note.Id);
                note.CreatedAt = originalNote.CreatedAt.ToString("yyyy-MM-dd");
                note.UpdatedAt = originalNote.UpdatedAt?.ToString("yyyy-MM-dd") ?? "No update";
            }

            return notesToMap;
        }

        public async Task<NoteDto?> GetNoteByIdAsync(int id)
        {
            //return await _noteRepository.GetByIdAsync(id);
            var note = await _noteRepository.GetAllAttached()
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id);

            var noteToMap = _mapper.Map<NoteDto>(note);
            noteToMap.CreatedAt = note.CreatedAt.ToString("yyyy-MM-dd");
            noteToMap.UpdatedAt = note.UpdatedAt?.ToString("yyyy-MM-dd") ?? "No update";

            return noteToMap;
        }

        public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            var newNote = _mapper.Map<Note>(createNoteDto);
            newNote.CreatedAt = DateTime.UtcNow;
            newNote.Category = new Category
            {
                Name = Enum.Parse<Categories>(createNoteDto.Category)
            };

            await _noteRepository.AddAsync(newNote);
            return _mapper.Map<NoteDto>(newNote);
        }

        public async Task<bool> UpdateNoteAsync(int id, UpdateNoteDto updateNoteDto)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null) return false;


            _mapper.Map(updateNoteDto, existingNote);
            existingNote.Category = new Category { Name = Enum.Parse<Categories>(updateNoteDto.Category) };
            existingNote.UpdatedAt = DateTime.UtcNow;

            await _noteRepository.UpdateAsync(existingNote);
            return true;
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null) return false;

            await _noteRepository.DeleteAsync(id);
            return true;
        }
    }
}
