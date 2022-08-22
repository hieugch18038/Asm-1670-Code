using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm1670.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Email { get; set; }
        public int OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
