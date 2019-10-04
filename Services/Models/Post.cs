using System;

namespace Services.Models
{
    public class Post: BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ThumbImage { get; set; }
        public string Body { get; set; }
        public DateTime AddDate { get; set; }

    }
}