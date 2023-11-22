using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? ReleaseYear { get; set; }
        public bool Active { get; set; }
        public string? ExternalId { get; set; }


        public Movie()
        {
            Active = true;
        }
    }
}
