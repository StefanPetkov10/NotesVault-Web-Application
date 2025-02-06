using NotesVaultApp.Data.Models.Enums;

namespace NotesVaultApp.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid();
            Notes = new List<Note>();
        }

        public Guid Id { get; set; }
        public Categories Name { get; set; }
        public List<Note> Notes { get; set; }
    }
}
