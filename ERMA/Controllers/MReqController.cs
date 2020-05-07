using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ERMA.App_Code;
using ERMA.Models;
using TACUtility;
using ERMA.Code;

namespace ERMA.Controllers
{
    public class MReqController : Controller
    {
        RequisitionerRepository repository = new RequisitionerRepository();

        VendorRepository vnRepository = new VendorRepository();
        SecureController sc = new SecureController();

        // GET: MReq
        public ActionResult Index()
        {
            try
            {
                if (Session[SessionName.MReqID.ToString()].ToString().Length > 0)
                {
                    ViewBag.MasterReqMsg = String.Format("Master Requisition {0} successfully added", Session[SessionName.MReqID.ToString()].ToString());
                }
                else
                {
                    if (Session[SessionName.MReqError.ToString()].ToString().Length > 0)
                    {
                        ViewBag.MasterReqMsg = String.Format("Error creating a Master Requisition: {0}", Session[SessionName.MReqError.ToString()].ToString());
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
            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";
            return View();
        }

        [HttpGet]
        public ActionResult ReqDefaults()
        {


            return View();
        }

        [HttpPost]
        public ActionResult ReqDefaults(RequisitionLineItemDefaults rDef)
        {
            try
            {
                Session[SessionName.GLAccount.ToString()] = rDef.GlAccount;
                Session[SessionName.Dept.ToString()] = rDef.DeptCode;
                Session[SessionName.DateRequired.ToString()] = rDef.RequiredDate;
                Session[SessionName.VendorCode.ToString()] = rDef.VendorCode;
                
            }
            catch { }

            return RedirectToAction("Create", "ReqDetail");
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            { 
                sc.CheckSecurity(AppRoles.All, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            MasterRequisition mr = new MasterRequisition();

            mr.MasterReqID = 0;
            mr.MasterReqNbrSpecified = true;
            mr.RequestID = "MReq".ToUniqueID();
            mr.RequestMode = "MReqMode".ToUniqueID();
            mr.Description3 = "Status, Inwork";
            mr.CMSDatabase = Properties.Settings.Default.DB2Database;
            //mr.Requisitioner = "1295";
            mr.Buyer = "SYS";


            mr.VendorList.Clear();
            mr.VendorList = vnRepository.GetVendorList(string.Empty);


            Requisitioner rq = new Requisitioner();
            string u = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper(); //In case we ever want to use Windows Login ID
            u = Session[SessionName.UserName.ToString()].ToString().ToUpper();  //Use the CMS User name
            rq = repository.GetRequisitionerByID(u);

            mr.Requisitioner = rq.ID;

            try
            {
                Session[SessionName.ProjectCode.ToString()] = mr.ProjectCodeSummary;
            }
            catch
            {
                Session[SessionName.ProjectCode.ToString()] = "";
            }
            return View(mr);
        }
                
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(MasterRequisition mr)
        {
            Int32 MReqID = 0;
            string Result = string.Empty;
            string u = Session[SessionName.UserName.ToString()].ToString();
            string p = Session[SessionName.Password.ToString()].ToString();

            Session[SessionName.MReqID.ToString()] = "";
            Session[SessionName.MReqError.ToString()] = "";
            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";


            if (ModelState.IsValid)
            {
                CMSAPI api = new CMSAPI();

                Result = api.AddMasterRequisition(mr, u, p);

                if(Result.IsNumeric())
                {
                    MReqID = Convert.ToInt32(Result);
                    Session[SessionName.MReqID.ToString()] = MReqID.ToString();
                }
                else
                {
                    Session[SessionName.MReqError.ToString()] = Result;
                }
                try
                {
                    Session[SessionName.ProjectCode.ToString()] = mr.ProjectCodeSummary;
                    Session[SessionName.VendorCode.ToString()] = mr.VendorCode;
                }
                catch
                {
                    Session[SessionName.ProjectCode.ToString()] = "";
                }
                return RedirectToAction("Create", "ReqDetail");
            }
            else
            {

                mr.VendorList.Clear();
                mr.VendorList = vnRepository.GetVendorList(string.Empty);
                return View(mr);
            }

        }


    }
}