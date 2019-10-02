
using Core.Model.OAuth;
using Microsoft.AspNetCore.Http;

namespace Services.Helpers
{
    public static class AppConstants
    {
        public static string LanguageCookieName { get; } = "ASP.NET_LANG";

        public static string RefreshTokenCookieName { get; } = "ASP.NET_TKN";

        public static string MintAdminAuthCookieName { get; } = "ASP.NET_MINTADMIN";
        public static string DefaultLanguage { get; } = "ar";

        public static string ClientSideDateFormat { get; } = "dd-mm-yyyy";

        public static string ClientSideMappedDateFormat { get; } = "dd-MM-yyyy";

        //public static MintOAuth2Response CurrentOAuthUser
        //{
        //    get { SessionExtensions.SetString()}
        //    set { }
        //}//=> HttpContext.Current.Session["CurrentOAuthUser"] as MintOAuth2Response;

    }
}