using DocumentFormat.OpenXml.Bibliography;
using System.Collections.Generic;

namespace Asm1670.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}
