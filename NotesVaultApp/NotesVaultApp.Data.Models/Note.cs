﻿namespace NotesVaultApp.Data.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
