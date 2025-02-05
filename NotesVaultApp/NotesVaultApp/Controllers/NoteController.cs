using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotesVaultApp.Data.Models;
using NotesVaultApp.Data.Models.Enums;
using NotesVaultApp.DTOs;
using NotesVaultApp.DTOs.Note_DTOs;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NoteController(INoteService noteService, IMapper mapper, ILogger<NoteController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _noteService = noteService;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();

            foreach (var note in notes)
            {
                Console.WriteLine($"Note ID: {note.Id}, Category: {note.Category?.Name}");
            }
            return Ok(_mapper.Map<IEnumerable<NoteDto>>(notes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Note with ID {id} not found.");
                return NotFound();
            }

            return Ok(_mapper.Map<NoteDto>(note));
        }


        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = Enum.Parse<Categories>(noteDto.Category); // Change Category to Categories

            var note = _mapper.Map<Note>(noteDto);
            note.CreatedAt = DateTime.UtcNow.ToString();

            note.Category = new Category { Name = category }; // Create a new Category object

            var createdNote = await _noteService.CreateNoteAsync(note);

            return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, _mapper.Map<NoteDto>(createdNote));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = _mapper.Map<Note>(noteDto);
            note.UpdatedAt = DateTime.UtcNow.ToString();
            note.Category = new Category
            {
                Name = Enum.Parse<Categories>(noteDto.Category)
            };
            note.Id = id;

            var result = await _noteService.UpdateNoteAsync(id, note);
            if (!result)
            {
                _logger.LogWarning($"Failed to update Note with ID {id}. Not found.");
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Attempted to delete Note with ID {id}, but it was not found.");
                return NotFound();
            }

            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}