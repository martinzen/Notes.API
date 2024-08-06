using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.API.Data;
using Notes.API.Models.Entities;

namespace Notes.API.Controllers
{
    // assigning the controller to API meaning that it wont have any news just pass data to our clients
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        // this is so we can use it inside the actions read only property 
        private readonly NotesDbContext notesDbContext;

        public NotesController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }


        // becasue its fetching results 
        [HttpGet]
        // get all the notes from the database action method  
        public async Task<IActionResult> GetAllNotes()
        {
            // Get the notes from the database => we have to get use of the Notes DbContext which is injected in the services and we cah use that injection
            // Notes is property becasue we defind it in  public DbSet<Note> Notes { get; set; }
            return Ok(await notesDbContext.Notes.ToListAsync());
            // rest api its a 200 return which is sucessful we need to wrap it in OK method becasue its is IActionResult
        }

        // single mehtod that gets only single entity for the database 

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNoteById")]
        // this is assigning the action name 
        // this is an action name GetNoteById
        public async Task<IActionResult> GetNoteById([FromRoute] Guid id)
        {
            //  return await notesDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);

            //or

            var note = await notesDbContext.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            // Ok responce
            return Ok(note);
        }

        [HttpPost]
        // adding new node to the database
        // this will return 201 http responce
        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await notesDbContext.Notes.AddAsync(note);
            await notesDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // update method
        [HttpPut]
        [Route("{id:Guid}")]
        // Guid name should match id what ever we are passing in the Route
        // updated notes property From Body
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note updatedNote) 
        {

            var existingNote = await notesDbContext.Notes.FindAsync(id);

            if (existingNote == null) 
            {
                return NotFound();
            }

            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.IsVisible = updatedNote.IsVisible;

            await notesDbContext.SaveChangesAsync();

            return Ok(existingNote);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var existingNote = await notesDbContext.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            // remove its not a aysnc methods 
            notesDbContext.Notes.Remove(existingNote);
            // thats why we do this so we can save changes 
            await notesDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
