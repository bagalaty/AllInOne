namespace Core
{
    public static class Constants
    {
        public const int PollingIntervalInMs = 5000;

        public static class Headers
        {
            public const string Bearer = "Bearer";

            public const string BusinessSdkVersionHeaderName = "X-ClientService-ClientTag";

            public const string ConsumerSdkVersionHeaderName = "X-RequestStats";

            public const string JsonContentType = "application/json";

            public const string FormUrlEncodedContentType = "application/x-www-form-urlencoded";

            public const string SdkVersionHeaderValue = "SDK-Version=CSharp-v{0}";

            public const string ThrowSiteHeaderName = "X-ThrowSite";

            public const string MultiPartContentType = "multipart/form-data";
        }
    }
}