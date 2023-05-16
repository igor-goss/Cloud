using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class File
    {
        public int Id { get; set; }
        public string? path { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        public byte[]? Content { get; set; }

        [ForeignKey(nameof(User.Id))]
        public int UserId { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime? DateModified { get; set; }
    }
}
