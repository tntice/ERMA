using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERMA.Code;
using System.Data.Odbc;
using System.Data;
using System.Web.Security;
using ERMA.App_Code;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TACUtility;

namespace ERMA.Code
{
    public static class SecurityUtility
    {

        public static bool isAuthenticated(string UserName)
        {
            bool returnValue = true;
            try
            {
                string user = UserName.ToUpper().Trim();

                if (user.Length == 0)
                {
                    returnValue = false;
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }
    }

    public class SecureController : Controller
    { 
        public  ActionResult CheckSecurity( AppRoles r, string UserName = null)
        {
            try
            {            
                if(UserName is null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (!(SecurityUtility.isAuthenticated(UserName)))
                {
                    return RedirectToAction("Login", "Home");
                }

                if (!(Authorize.IsAuthorized(r, UserName)))
                {
                    return RedirectToAction("Unauthorized", "Home");
                }
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
            return null;


        }
    }
}