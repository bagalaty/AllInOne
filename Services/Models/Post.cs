using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class Post: BaseModel
    {
        [Key, MaxLength(38)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ThumbImage { get; set; }
        public string Body { get; set; }
        public DateTime AddDate { get; set; }

    }
}