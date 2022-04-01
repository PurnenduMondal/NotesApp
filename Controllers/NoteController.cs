using Microsoft.AspNetCore.Mvc;
using my_new_app.Models;
using my_new_app.Interfaces;

namespace my_new_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private INoteRepository _repository;

        public NoteController(INoteRepository repository)
        {
            _repository = repository;
        }

        // GET Note
        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return _repository.GetNotes();
        }

        // GET Note/5
        [HttpGet("{id}")]
        public Note Get(int id)
        {
            return _repository.GetNoteById(id);
        }

        // POST Note
        [HttpPost]
        public void Post(Note note)
        {
            _repository.Insert(note);
        }

        // PUT Note/5
        [HttpPut("{id}")]
        public void Put(int id, Note note)
        {
            _repository.UpdateNoteById(id, note);
        }

        // DELETE Note/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.DeleteNote(id);
        }
    }
}
