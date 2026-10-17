using System.ComponentModel.DataAnnotations;

namespace NetflixClone.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        public int ReleaseYear { get; set; }

        public int Duration { get; set; }

        public string? PosterUrl { get; set; }

        public string? VideoUrl { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<MyList> MyLists { get; set; } = new();
    }
}