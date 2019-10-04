using Core.Proxy.Http;
using Services.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
    public class BaseModel
    {
        public BaseModel()
        { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }



        public BaseModel(AllInOneResponse response)
        {
            if (response != null)
            {
                Message = response.message;
                MessageType = response.HasError ? MessageType.error : MessageType.success;
            }
            else
            {
                Message = "Invalid Response";
                MessageType = MessageType.error;
            }
            if (Message == "No message available")
                Message = "Internal Server Error";
        }

        public string Message { get; set; }

        public MessageType MessageType { get; set; }

        public void FillServerError(BaseModel response, bool splitErrorMessage = false)
        {
            if (splitErrorMessage)
            {
                var tokens = response.Message.Split(new char[] { ':' });
                response.Message = tokens.Length == 2 ? tokens[1] : response.Message;
            }

            MessageType = MessageType.error;
            Message = response.Message;
            if (Message == "No message available")
                Message = "Internal Server Error";

        }

        public void FillServerError(AllInOneResponse response)
        {
            var tokens = response.message.Split(':');
            response.message = tokens.Length == 1 ? tokens[0] : tokens[1];

            MessageType = MessageType.error;
            Message = response.message;
        }

    }
}
