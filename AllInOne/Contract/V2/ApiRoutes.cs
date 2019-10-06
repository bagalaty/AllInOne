using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllInOne.Contract.V2
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Root = "api";
        /// <summary>
        /// 
        /// </summary>
        public const string Version = "v2";
        /// <summary>
        /// 
        /// </summary>
        public const string VersionNumber = "2.0";

        /// <summary>
        /// 
        /// </summary>
        public const string Base = Root + "/" + Version;
        /// <summary>
        /// 
        /// </summary>
        public static class Bookmark
        {
            /// <summary>
            /// 
            /// </summary>
            public const string GetAll = Base + "/Bookmark";
        }
    }
}
