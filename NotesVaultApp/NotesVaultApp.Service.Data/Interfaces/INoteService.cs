using NotesVaultApp.DTOs;
using NotesVaultApp.DTOs.Note_DTOs;

namespace NotesVaultApp.Service.Data.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllAsync();
        Task<NoteDto> GetByIdAsync(Guid id);
        Task<NoteDto> CreateAsync(CreateNoteDto createNoteDto);
        Task<bool> UpdateAsync(Guid id, UpdateNoteDto updateNoteDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
