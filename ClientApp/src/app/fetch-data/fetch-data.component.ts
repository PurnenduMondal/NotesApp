import { Component, OnInit, } from '@angular/core';
import { Note } from './note';
import { NotesService } from './notes.service';

@Component({
  selector: 'app-fetch-data',
  providers: [NotesService],
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public notes: Note[] = [];
  editNote: Note | undefined;
  noteName = '';

  constructor(private notesService: NotesService) {}

  ngOnInit() {
    this.getNotes();
  }

  getNotes(): void {
    this.notesService.getNotes()
      .subscribe(notes => (this.notes = notes));
  }

  add(content: string): void {
    this.editNote = undefined;
    content = content.trim();
    if (!content) {
      return;
    }
    const newNote: Note = { content } as Note;
    this.notesService
      .addNote(newNote)
      .subscribe(noteId => {this.notes.push({ noteId, content } as Note);console.log(noteId)});
  }

  delete(note: Note): void {
    this.notes = this.notes.filter(h => h !== note);
    this.notesService
      .deleteNote(note.noteId)
      .subscribe();
  }

  edit(noteName: string) {
    this.update(noteName);
    this.editNote = undefined;
  }

  search(searchTerm: string) {
    this.editNote = undefined;
    if (searchTerm) {
      this.notesService
        .searchNotes(searchTerm)
        .subscribe(note => this.notes = [note]);
    } else {
      this.getNotes();
    }
  }

  update(content: string) {
    if (content && this.editNote) {
      var noteId = this.editNote.noteId;
      const indexOfNote = this.notes.findIndex(h => h.noteId === noteId);
      if (indexOfNote > -1) {
        this.notes[indexOfNote] = { noteId, content } as Note;
      }
      this.notesService
        .updateNote(this.editNote.noteId, { noteId, content } as Note)
        .subscribe();
      this.editNote = undefined;
    }
  }
}


