const API_BASE_URL = "https://localhost:7092/api";

document.addEventListener('DOMContentLoaded', () => {
    const notesList = document.getElementById('notes');
    const createNoteForm = document.getElementById('createNoteForm');
    const showAllNotesButton = document.getElementById('showAllNotes');
    const hideNotesButton = document.getElementById('hideNotes');

    notesList.style.display = "none";
    hideNotesButton.style.display = "none";

    // Fetch and display all notes
    async function fetchNotes() {
        const response = await fetch(`${API_BASE_URL}/Note`);
        const notes = await response.json();
        notesList.innerHTML = '';
        notes.forEach(note => {
            const li = document.createElement('li');
            li.innerHTML = `
                <strong>${note.title}</strong>
                <p>${note.content}</p>
                <small>Category: ${note.category}</small>
                <p><small>Created on: ${note.createdAt}</small></p>
                <p><small>Updated on: ${note.updatedAt}</small></p>
                <div class="note-actions">
                    <button onclick="editNote('${note.id}')">Edit</button>
                    <button class="delete" onclick="deleteNote('${note.id}')">Delete</button>

                </div>
            `;
            notesList.appendChild(li);

        });
        notesList.style.display = "block";
        hideNotesButton.style.display = "inline-block";
        showAllNotesButton.style.display = "none";
    }

    createNoteForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        const title = document.getElementById('title').value;
        const content = document.getElementById('content').value;
        const category = document.getElementById('category').value;

        const newNote = {
            title,
            content,
            category
        };

        const response = await fetch(`${API_BASE_URL}/Note`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newNote)
        });

        if (response.ok) {
            fetchNotes();
            createNoteForm.reset();
        }
    });

    if (showAllNotesButton) {
        showAllNotesButton.addEventListener('click', () => {
            fetchNotes();
        });
    }

    hideNotesButton.addEventListener('click', () => {
        notesList.style.display = "none";
        hideNotesButton.style.display = "none";
        showAllNotesButton.style.display = "inline-block";
    });

    window.editNote = async (id) => {
        const title = prompt('Enter new title:');
        const content = prompt('Enter new content:');
        const category = prompt('Enter new category (Personal, Work, General):');

        if (title && content && category) {
            const updatedNote = {
                title,
                content,
                category
            };

            const response = await fetch(`${API_BASE_URL}/Note/${encodeURIComponent(id)}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedNote)
            });

            if (response.ok) {
                fetchNotes();
            }
        }
    };

    window.deleteNote = async (id) => {
        if (confirm('Are you sure you want to delete this note?')) {
            const response = await fetch(`${API_BASE_URL}/Note/${encodeURIComponent(id)}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                fetchNotes();
            }
        }
    };
});
