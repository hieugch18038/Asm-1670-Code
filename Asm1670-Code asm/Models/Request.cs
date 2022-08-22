using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm1670.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string RequestName { get; set; }
    }
}
