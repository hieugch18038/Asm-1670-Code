using DocumentFormat.OpenXml.Bibliography;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asm1670.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [Url]
        public string Image { get; set; }
        [Required]
        public float Price { get; set; }

        [Required]
        [Range(0, 99)]
        public int Quantity { get; set; }

        public ICollection<Cart> Cart { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category name: ")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Author name:")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
