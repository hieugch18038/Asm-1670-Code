using System.Collections.Generic;

namespace Asm1670.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }

        public ICollection<Book> Book { get; set; }    
    }
}
