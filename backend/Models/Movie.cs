using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? ReleaseYear { get; set; }
        public bool Active { get; set; }
        public int? ExternalId { get; set; }

        public Movie()
        {
            Active = true;
            ExternalId = null;
        }
    }
}
