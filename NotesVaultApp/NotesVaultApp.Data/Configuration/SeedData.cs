using NotesVaultApp.Data.Models;
using NotesVaultApp.Data.Models.Enums;

namespace NotesVaultApp.Data.Configuration
{
    public class SeedData
    {
        public Category Category1 = new();
        public Category Category2 = new();

        public Note Note1 = new();
        public Note Note2 = new();

        public SeedData()
        {
            SeedCategories();
            SeedNotes();
        }

        private void SeedCategories()
        {
            Category1.Id = Guid.NewGuid();
            Category1.Name = Categories.Personal;
            Category2.Id = Guid.NewGuid();
            Category2.Name = Categories.Work;
        }

        private void SeedNotes()
        {
            Note1.Id = Guid.NewGuid();
            Note1.Title = "First Note";
            Note1.Content = "This is the content of the first note";
            Note1.CreatedAt = DateTime.Today.AddDays(-1);
            Note1.UpdatedAt = DateTime.Today.AddDays(1);
            Note1.CategoryId = Category1.Id;

            Note2.Id = Guid.NewGuid();
            Note2.Title = "Second Note";
            Note2.Content = "This is the content of the second note";
            Note2.CreatedAt = DateTime.Today;
            Note2.CategoryId = Category2.Id;
        }

    }
}
