using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotesVaultApp.DTOs.Note_DTOs;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteService noteService, IMapper mapper, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllAsync();

            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await _noteService.GetByIdAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Note with ID {id} not found.");
                return NotFound();
            }

            return Ok(note);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdNote = await _noteService.CreateAsync(noteDto);

            return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, [FromBody] UpdateNoteDto updateNoteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _noteService.UpdateAsync(id, updateNoteDto);
            if (!result)
            {
                _logger.LogWarning($"Failed to update Note with ID {id}. Not found.");
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var note = await _noteService.GetByIdAsync(id);
            if (note == null)
            {
                _logger.LogWarning($"Attempted to delete Note with ID {id}, but it was not found.");
                return NotFound();
            }

            await _noteService.DeleteAsync(id);
            return NoContent();
        }
    }
}