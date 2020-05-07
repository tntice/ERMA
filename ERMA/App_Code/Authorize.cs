using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TACUtility;
using SecurityTools;

namespace ERMA.App_Code
{
    public static class Authorize
    {
        
        public static bool IsAuthorized(AppRoles role, String UserName)
        {
            bool returnValue = true;
            try
            {
                //string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
                string user = UserName;


                if (TACAuthorization.HasPersmission("ERMA", user, role.ToString(), false))
                {
                    returnValue = true;
                }
                else
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


    public enum AppRoles
    {
        Admin,
        Power,
        Manager,
        Limited,
        Reports,
        All
    }

    public enum SessionName
    {
        UserName,
        Password,
        GLAccount,
        DateRequired,
        Dept,
        VendorCode,
        ProjectCode,
        MReqID,
        ReqID,
        ReqError,
        MReqError,
        Conn
    }
}