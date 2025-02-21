using System.ComponentModel.DataAnnotations;

namespace BookManagement.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        public DateTime PublishedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewsCount { get; set; }
    }
}
