using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class AppUser : IdentityUser
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        //{
        //    var userIndentity=await manager.CreateItentityAsync(this,DefaultAuthentication)
        //        return userIndentity;
        //}

        // Extended Properties
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? FacebookId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsNewsLetter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid UserGUID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DatOfBirth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid BlockedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

    }
}
