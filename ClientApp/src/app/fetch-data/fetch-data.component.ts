import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public notes: Note[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Note[]>('https://localhost:7254/note').subscribe(result => {
      this.notes = result;
    }, error => console.error(error));
  }


}

interface Note {
  noteId: string;
  content: string;
}
