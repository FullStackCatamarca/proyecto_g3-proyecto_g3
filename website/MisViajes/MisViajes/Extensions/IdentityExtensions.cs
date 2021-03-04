using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MisViajes.Extensions
{
    public static  class IdentityExtensions
    {
        public static string GetAvatarUrl(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("AvatarUrl");
            return (claim != null && claim.Value != "") ? claim.Value : string.Empty;
        }

        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return (claim != null && claim.Value != " ") ? claim.Value : "Sin Nombre";
        }

        public static string GetImgUrl(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ImgUrl");
            return (claim != null && claim.Value !="") ? claim.Value : string.Empty;
        }

        
    }
}