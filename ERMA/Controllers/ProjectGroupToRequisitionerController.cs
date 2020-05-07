using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERMA.Models;
using ERMA.Models.ProjectViewModel;
using ERMA.Code;
using ERMA.App_Code;
using TACUtility;

namespace ERMA.Controllers
{
    public class ProjectGroupToRequisitionerController : Controller
    {
        ProjectGroupRepository pjRepository = new ProjectGroupRepository();
        ProjectRepository prjRepository = new ProjectRepository();
        MNGroupRequisitionerRepository grpReqRepository = new MNGroupRequisitionerRepository();
        VMGroupRequisitionerRepository vmGrpReqRepository = new VMGroupRequisitionerRepository();
        CMSMethods cmsMethods = new CMSMethods();
        SecureController sc = new SecureController();

        // GET: ProjectGroupToRequisitioner
        [HttpGet]
        public ActionResult Index(string PGID)
        {
            try
            { 
                sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            VMGroupRequisitioner grpReq = new VMGroupRequisitioner();

            Int32 iPGID = Convert.ToInt32(PGID);

            grpReq = vmGrpReqRepository.GetByPGID(iPGID);

            return View(nameof(Index), grpReq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VMGroupRequisitioner mvGrpReq)
        {
            return View(nameof(Index), mvGrpReq);
        }

        public ActionResult Create()
        {
            try
            { 
                sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            MNGroupRequisitioner mnGrpReq = new MNGroupRequisitioner();

            Int32 PGID = Convert.ToInt32(Request.QueryString["PGID"].ToString());
            string ReqUserID = Request.QueryString["item.UserName"].ToString().ToUpper().Trim();

            mnGrpReq.PGID = PGID;
            mnGrpReq.UserID = ReqUserID;

            Int32 result = 0;

            result = grpReqRepository.Insert(mnGrpReq);
            cmsMethods.AddSinglePermissionByRequisitioner(PGID.ToString(), ReqUserID);

            VMGroupRequisitioner vmGrpReq = new VMGroupRequisitioner();

            vmGrpReq = vmGrpReqRepository.GetByPGID(PGID);

            return View(nameof(Index), vmGrpReq);         

        }
        public ActionResult Delete()
        {
            try
            { 
                sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            MNGroupRequisitioner mnGrpReq = new MNGroupRequisitioner();

            Int32 PGID = Convert.ToInt32(Request.QueryString["PGID"].ToString());
            string ReqUserID = Request.QueryString["item.UserName"].ToString().ToUpper().Trim();
            string MNPRGID = Request.QueryString["rec.MNPRGID"].ToString();

            mnGrpReq.PGID = PGID;
            mnGrpReq.UserID = ReqUserID;

            Int32 result = 0;

            cmsMethods.RemoveSinglePermissionByRequisitioner(PGID.ToString(), ReqUserID);
            result = grpReqRepository.Delete(MNPRGID);

            VMGroupRequisitioner vmGrpReq = new VMGroupRequisitioner();

            vmGrpReq = vmGrpReqRepository.GetByPGID(PGID);

            return View(nameof(Index), vmGrpReq);

        }

    }
}