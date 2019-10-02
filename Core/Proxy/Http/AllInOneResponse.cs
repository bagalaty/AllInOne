namespace Core.Proxy.Http
{
    public class AllInOneResponse
    {
        public string ResponseString { get; set; }

        public int code { get; set; }

        public string message { get; set; }

        public int numberOfPages { get; set; }

        public bool HasError => code != 10;
    }

    public class AllInOneResponse<T> : AllInOneResponse
    {
        public AllInOneResponse() { }

        public AllInOneResponse(AllInOneResponse response)
        {
            ResponseString = response.ResponseString;
            code = response.code;
            message = response.message;
        }

        public T data { get; set; }

        public int totalPages { get; set; }

        public int page { get; set; }

        public int size { get; set; }

    }
}
