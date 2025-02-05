using System.ComponentModel.DataAnnotations;

namespace NotesVaultApp.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters long.")]
        public string Content { get; set; } = null!;

        public string CreatedAt { get; set; } = null!;
    }
}
