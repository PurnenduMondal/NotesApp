using my_new_app.Models;
using System.Data.SqlClient;
using System.Data;
using my_new_app.Interfaces;

namespace my_new_app.Repositories
{
  public class NoteRepository : INoteRepository
  {
    public string connectionString { get; set; }
    public IConfiguration _configuration;

    public NoteRepository(IConfiguration configuration)
    {
      _configuration = configuration;
      connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public void Insert(Note note)
    {
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertNote", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Content", note.Content); 
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }

    public IEnumerable<Note> GetNotes()
    {
      List<Note> notes = new();
      
      using (SqlConnection cnn = new SqlConnection(connectionString))
      {
        using (SqlCommand cmd = new SqlCommand("GetAllNotes", cnn))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          if (cnn.State == ConnectionState.Closed)
          {
            cnn.Open();
          }
          using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
          {
            while (reader.Read())
            {
              notes.Add(new Note()
              {
                  NoteId = Convert.ToInt32(reader["NoteId"]),
                  Content = reader["Content"].ToString()
              });
            }
            return notes;
          }
        }
      }
    }
    public Note GetNoteById(int? id)
    {
        Note note = new();

        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetNoteById", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NoteId", id);
                
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        note.NoteId = Convert.ToInt32(reader["NoteId"]);
                        note.Content = reader["Content"].ToString();
                    }
                    return note;
                }
            }
        }
    }

    public void UpdateNoteById(int? id, Note note)
    {
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateNoteById", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NoteId", id);
                cmd.Parameters.AddWithValue("@Content", note.Content);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
    public void DeleteNote(int? id)
    {
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteNoteById", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NoteId", id);
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
    }
}
