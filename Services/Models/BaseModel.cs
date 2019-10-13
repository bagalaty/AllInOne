using Services.Models.Enums;
using Services.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Services.Models
{
    public class BaseModel: ITrackable
    {
        public BaseModel()
        { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, MaxLength(38)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public string CreatedBy { get ; set ; }
        public DateTime LastUpdatedAt { get ; set ; }
        public string LastUpdatedBy { get ; set ; }

        //[IgnoreDataMember]
        //public DateTime Created { get; set; }
        //[IgnoreDataMember]
        //public DateTime Modified { get; set; }
        [IgnoreDataMember]
        public string Message { get; set; }
        [IgnoreDataMember]
        public MessageType MessageType { get; set; }


        //public BaseModel(AllInOneResponse response)
        //{
        //    if (response != null)
        //    {
        //        Message = response.message;
        //        MessageType = response.HasError ? MessageType.error : MessageType.success;
        //    }
        //    else
        //    {
        //        Message = "Invalid Response";
        //        MessageType = MessageType.error;
        //    }
        //    if (Message == "No message available")
        //        Message = "Internal Server Error";
        //}




        //public void FillServerError(BaseModel response, bool splitErrorMessage = false)
        //{
        //    if (splitErrorMessage)
        //    {
        //        var tokens = response.Message.Split(new char[] { ':' });
        //        response.Message = tokens.Length == 2 ? tokens[1] : response.Message;
        //    }

        //    MessageType = MessageType.error;
        //    Message = response.Message;
        //    if (Message == "No message available")
        //        Message = "Internal Server Error";

        //}

        //public void FillServerError(AllInOneResponse response)
        //{
        //    var tokens = response.message.Split(':');
        //    response.message = tokens.Length == 1 ? tokens[0] : tokens[1];

        //    MessageType = MessageType.error;
        //    Message = response.message;
        //}

    }
}
