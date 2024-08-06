


using Microsoft.EntityFrameworkCore;
using Notes.API.Models.Entities;
using System.Diagnostics;

namespace Notes.API.Data
{
    // this will inherite from DbContext
    // DbContext Configuration: Configures database connection settings.
    // Tracking: Tracks changes made to the entity objects.
    // Querying: Allows querying of the database using LINQ.
    // Saving: Saves changes back to the database.


    public class NotesDbContext : DbContext
    {
        // contrcution which we can pass options
        public NotesDbContext(DbContextOptions options) : base(options)
        {

        }

        // type of properties for DbContext so we can talk to the database
        // DbSet Represents a collection of entities that you can query from the database and is analogous to a table in the database.
        public DbSet<Note> Notes { get; set; }
        // Notes will act as interface to use the notes table to create update and delte individual notes 
    }

}
