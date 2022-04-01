using my_new_app.Models;

namespace my_new_app.Interfaces
{
  public interface INoteRepository
  {
        int Insert(Note note);
        IEnumerable<Note> GetNotes();
        Note GetNoteById(int? id);
        void UpdateNoteById(int? id, Note note);
        void DeleteNote(int? id);
  }
}