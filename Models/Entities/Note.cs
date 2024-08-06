namespace Notes.API.Models.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        // random number generator 
        public string Title  { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

        // we will invoke this data from the JS client side
    }
}
