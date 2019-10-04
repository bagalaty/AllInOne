using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Models
{
    public class Bookmark: BaseModel
    {
        [Key]
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
