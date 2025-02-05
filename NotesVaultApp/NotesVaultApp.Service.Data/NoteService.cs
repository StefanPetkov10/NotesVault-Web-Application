﻿using NotesVaultApp.Data.Models;
using NotesVaultApp.Data.Repository.Interface;
using NotesVaultApp.Service.Data.Interfaces;

namespace NotesVaultApp.Service.Data
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;

        public NoteService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _noteRepository.GetAllAsync();
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _noteRepository.GetByIdAsync(id);
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _noteRepository.AddAsync(note);
            return note;
        }

        public async Task<bool> UpdateNoteAsync(int id, Note note)
        {
            var existingNote = await _noteRepository.GetByIdAsync(id);
            if (existingNote == null) return false;

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;

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
