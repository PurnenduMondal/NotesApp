import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Note } from './note';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable()
export class NotesService {
  notesUrl = 'https://localhost:7254/note';  // URL to web api

  constructor(private http: HttpClient) {}

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.notesUrl)
  }

  searchNotes(term: string): Observable<Note> {
    term = term.trim();

    const url = `${this.notesUrl}/${term}`;
    console.log(url)
    return this.http.get<Note>(url)
  }


  addNote(note: Note): Observable<number> {
    return this.http.post<number>(this.notesUrl, note, httpOptions)
  }

  deleteNote(id: number): Observable<unknown> {
    const url = `${this.notesUrl}/${id}`; // DELETE api/notes/42
    return this.http.delete(url, httpOptions)
  }

  updateNote(id:number, note: Note): Observable<unknown> {
    httpOptions.headers =
      httpOptions.headers.set('Authorization', 'my-new-auth-token');

    return this.http.put(`${this.notesUrl}/${id}`, note, httpOptions)
  }
}