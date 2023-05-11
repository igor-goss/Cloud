using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class File
    {
        public int Id { get; set; }
        public string? path { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
    }
}
