namespace NotesVaultApp.Data.Models
{
    public class Note
    {
        public Note()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
