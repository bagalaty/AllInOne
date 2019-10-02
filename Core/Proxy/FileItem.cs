using Microsoft.AspNetCore.Http;

namespace Core.Proxy
{
    public class FileItem
    {
        public IFormFile File { get; set; }

        public string ParamterName { get; set; }
    }
}