using NotesVaultApp.DTOs;
using NotesVaultApp.DTOs.Note_DTOs;

namespace NotesVaultApp.Service.Data.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllNotesAsync();
        Task<NoteDto> GetNoteByIdAsync(int id);
        Task<NoteDto> CreateNoteAsync(CreateNoteDto createNoteDto);
        Task<bool> UpdateNoteAsync(int id, UpdateNoteDto updateNoteDto);
        Task<bool> DeleteNoteAsync(int id);
    }
}
