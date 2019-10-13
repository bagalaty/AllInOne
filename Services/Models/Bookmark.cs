using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Services.Models
{
    [DebuggerDisplay("{Id} - {Title} - {IsDeleted} - {Description}")]
    public class Bookmark: BaseModel
    {
        [Key, MaxLength(38)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(100), StringLength(110)]
        public string Title { get; set; }
        [Required, MaxLength(400), StringLength(400)]
        public string Description { get; set; }
        [Required, DataType(DataType.Url)]
        public string ImageURL { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
