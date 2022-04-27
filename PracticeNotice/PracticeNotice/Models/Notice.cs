using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeNotice.Models
{
    public class Notice
    {
        [Key]
        public int Idx { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegDate { get; set; }
    }
}
