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


namespace ERMA.Controllers
{
    public class HomeController : Controller
    {
        SecureController sc = new SecureController();

        public ActionResult Index()
        {
            try
            {                
                sc.CheckSecurity(AppRoles.All, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {
                if(Session[SessionName.MReqID.ToString()].ToString().Length > 0)
                {
                    ViewBag.MasterReqMsg = String.Format("Master Requisition {0} successfully added", Session[SessionName.MReqID.ToString()].ToString());
                }
                else
                {
                    if(Session[SessionName.MReqError.ToString()].ToString().Length > 0)
                    {
                        ViewBag.MasterReqMsg = String.Format("{0}", Session[SessionName.MReqError.ToString()].ToString());
                    }
                    else
                    {
                        ViewBag.MasterReqMsg = "";
                    }
                }
            }
            catch
            {
                ViewBag.MasterReqMsg = "";
            }
            string strVal = string.Empty;
            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";
            return View();
        }

        //public JsonResult JText()
        //{

        //}

        public ActionResult Unauthorized()
        {
            ViewBag.Message = "You are unauthorized to use this application";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Login to the Application";
            ViewBag.ErrorMessage = "";
            //Session[SessionName.UserName.ToString()] = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();

            return View();

        }
                
        public ActionResult Login(string msg)
        {

            string u = Request["txtUserName"].ToString().ToUpper();
            string p = Request["txtPassword"].ToString();
            string strCon = string.Empty;

            strCon = "Driver={Client Access ODBC Driver (32-bit)};system=" + Properties.Settings.Default.Server + ";dbq=,CMSDATEDI TOYOTOMI WOW OIPAYFILES;dftpkglib=QGPL;languageid=ENU;pkg=QGPL/DEFAULT(IBM),2,0,1,0,512;qrystglmt=-1;cmt=0;nam=1;translate=1;ssl=0;signon=1;uid=" + u + ";pwd=" + p + "";


            Session[SessionName.Conn.ToString()] = strCon;
            //Session[SessionName.UserName.ToString()] = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            //Session[SessionName.UserName.ToString()] = u;

            DataTable dt = new DataTable();

            string sql = "SELECT * FROM WOW.EMPLOYEE WHERE EMDELT = 'A'";

            try
            {
                dt = TACData.Data.GetDataRows(sql, strCon);
                if(!(dt is null))
                {
                    if(dt.Rows.Count > 0)
                    {
                        Session[SessionName.UserName.ToString()] = u;
                        Session[SessionName.Password.ToString()] = p;

                        bool isCookiePersistent = true;
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, u.ToUpper(), DateTime.Now, DateTime.Now.AddMinutes(120), isCookiePersistent, "Admin" + Session[SessionName.Conn.ToString()]);
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                        if(isCookiePersistent)
                        {
                            authCookie.Expires = authTicket.Expiration;
                        }
                        Response.Cookies.Add(authCookie);

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        Session[SessionName.UserName.ToString()] = string.Empty;
                        Session[SessionName.Password.ToString()] = string.Empty;
                        ViewBag.ErrorMessage = "Invalid User Name or Password";
                        return View();

                    }
                }
                else
                {
                    Session[SessionName.UserName.ToString()] = string.Empty;
                    Session[SessionName.Password.ToString()] = string.Empty;
                    ViewBag.ErrorMessage = "Invalid User Name or Password";
                    return View();
                }
            }
            catch(Exception ex)
            {
                string strMsg = ex.Message;

                ViewBag.ErrorMessage = "Invalid User Name or Password";
                return View();
            }
        }
        
    }
}