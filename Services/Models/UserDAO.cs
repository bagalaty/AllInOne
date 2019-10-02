using System;
using System.Collections.Generic;
using System.Linq;
using Services.Helpers;
using Services.Models.Enums;
using Newtonsoft.Json;
using Core.Model.OAuth;
using Services;
using Microsoft.Extensions.Primitives;

namespace Services.Models
{
    public class UserDAO : BaseModel
    {
        public string foundationId;
        public string organizationId;

        public UserDAO()
        { }
        public string id { get; set; }

        public string login { get; set; }

        public string fullName { get; set; }
        public string name { get; set; }

        public UserTypes UserType { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public bool Activated { get; set; }

        public string lang { get; set; }

        public bool firstLogin { get; set; }

        public string access_token { get; set; }

        public long? role_id { get; set; }

        public string country { get; set; }

        public string profession { get; set; }

        public string birthDate { get; set; }

        public string image { get; set; }

        public string userStatus { get; set; }

        public string interests { get; set; }

        public int spacesCount { get; set; }

        //public string roleName
        //{
        //    get
        //    {
        //        if (roles != null && roles.Any())
        //        {
        //            return Utilities.RemoveFirstUnderscore(roles.FirstOrDefault().name);
        //        }
        //        return "N/A";
        //    }
        //}

        //public Dictionary<string, PermissionAction> permissions { get; set; }
        //public Dictionary<string, Dictionary<string, PermissionAction>> spaceRolePermission { get; set; }








        #region Calculated

        public DateTime? ParsedBirthDate => birthDate.GetDateTime();

        #endregion



        public bool IsB2C { get; set; }
        public bool IsOwner { get; set; }
        public bool emailNotification { get; set; }
        public string school { get; set; }
    }
}
