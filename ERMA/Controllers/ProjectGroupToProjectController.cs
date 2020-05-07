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
    public class ProjectGroupToProjectController : Controller
    {
        ProjectGroupRepository pjRepository = new ProjectGroupRepository();
        ProjectRepository prjRepository = new ProjectRepository();
        MNGroupProjectRepository prjGroupRepository = new MNGroupProjectRepository();
        VMGroupProjectRepository vmGrpProjRepository = new VMGroupProjectRepository();
        CMSMethods cmsMethods = new CMSMethods();
        SecureController sc = new SecureController();

        [HttpGet]
        // GET: ProjectGroupToProject
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
            Int32 iPGID = Convert.ToInt32(PGID);
            VMGroupProject vmGrpProject = new VMGroupProject();
            vmGrpProject = vmGrpProjRepository.GetByPGID(iPGID);
            vmGrpProject.ProjectStatusText = "ALL";

            return View(nameof(Index), vmGrpProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VMGroupProject vmGrpProject)
        {
            vmGrpProject = vmGrpProjRepository.GetByPGIDAndStatus(vmGrpProject.PGID, vmGrpProject.ProjectStatusText);
            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            return View(nameof(Index), vmGrpProject);
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

            MNGroupProject mnGrpPrj = new MNGroupProject();
            VMGroupProject mvGrpPrj = new VMGroupProject();

            Int32 PGID = Convert.ToInt32(Request.QueryString["PGID"].ToString());
            string ProjectNumber = Request.QueryString["SelectedProject"].ToString();
            string Status = Request.QueryString["ProjectStatusText"].ToString().ToUpper();
            Int32 result = 0;


            if (!(ProjectNumber.Equals("ALL")))
            {            
                mnGrpPrj.PGID = PGID;
                mnGrpPrj.ProjectID = ProjectNumber;

                
                result = prjGroupRepository.Insert(mnGrpPrj);
                cmsMethods.AddSinglePermissionByProject(PGID.ToString(), ProjectNumber);


                
                if (Status.ToUpper().Equals("ALL"))
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGID(PGID);

                }
                else
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGIDAndStatus(PGID, Status);
                }
            }
            else
            {
                if(Status.ToUpper().Equals("ALL"))
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGID(PGID);
                }
                else
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGIDAndStatus(PGID, Status);
                }

                foreach(Project prj in mvGrpPrj.ProjectNotInGroupList)
                {
                    MNGroupProject mnWorkingObject = new MNGroupProject();
                    mnWorkingObject.PGID = PGID;
                    mnWorkingObject.ProjectID = prj.Code;
                    result = prjGroupRepository.Insert(mnWorkingObject);

                }
                cmsMethods.AddAllPermissions(PGID.ToString());
                
                
                if (Status.ToUpper().Equals("ALL"))
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGID(PGID);

                }
                else
                {
                    mvGrpPrj = vmGrpProjRepository.GetByPGIDAndStatus(PGID, Status);
                }
            }


            return View(nameof(Index), mvGrpPrj);

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

            MNGroupProject mnGrpPrj = new MNGroupProject();

            Int32 PGID = Convert.ToInt32(Request.QueryString["PGID"].ToString());
            string ProjectNumber = Request.QueryString["item.Code"].ToString();
            string Status = Request.QueryString["item.StatusDesc"].ToString().ToUpper();

            string MNPGID = Request.QueryString["rec.MNPGID"].ToString();

            Int32 result = 0;
            mnGrpPrj.PGID = PGID;
            mnGrpPrj.ProjectID = ProjectNumber;
            
            cmsMethods.RemoveSinglePermissionByProject(PGID.ToString(), ProjectNumber);
            result = prjGroupRepository.Delete(MNPGID);
            
            VMGroupProject mvGrpPrj = new VMGroupProject();
            Int32 iPGID = Convert.ToInt32(PGID);


            mvGrpPrj = vmGrpProjRepository.GetByPGIDAndStatus(iPGID, Status);

            return View(nameof(Index), mvGrpPrj);
        }


        public ActionResult DeleteAll()
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

            MNGroupProject mnGrpPrj = new MNGroupProject();

            Int32 PGID = Convert.ToInt32(Request.QueryString["PGID"].ToString());
            
            Int32 result = 0;
            
            VMGroupProject mvGrpPrj = new VMGroupProject();
            try
            {
                cmsMethods.RemoveAllPermissions(PGID.ToString());
            }
            catch { }
            result = prjGroupRepository.Delete(PGID);

            mvGrpPrj = vmGrpProjRepository.GetByPGID(PGID);

            return View(nameof(Index), mvGrpPrj);
        }


        public ActionResult GetActiveProjects(string PGID)
        {
            Int32 iPGID = Convert.ToInt32(PGID);
            VMGroupProject vmGrpProject = new VMGroupProject();
            vmGrpProject = vmGrpProjRepository.GetByPGIDAndStatus(iPGID, "ACTIVE");
            vmGrpProject.ProjectStatusText = "ACTIVE";
            List<SelectListItem> items = new List<SelectListItem>();

            items = vmGrpProject.CboProjectsNotInGroup;

            return Json(items, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetInActiveProjects(string PGID)
        {
            Int32 iPGID = Convert.ToInt32(PGID);
            VMGroupProject vmGrpProject = new VMGroupProject();
            vmGrpProject = vmGrpProjRepository.GetByPGIDAndStatus(iPGID, "INACTIVE");
            vmGrpProject.ProjectStatusText = "INACTIVE";
            List<SelectListItem> items = new List<SelectListItem>();

            items = vmGrpProject.CboProjectsNotInGroup;

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllProjects(string PGID)
        {
            Int32 iPGID = Convert.ToInt32(PGID);
            VMGroupProject vmGrpProject = new VMGroupProject();
            vmGrpProject = vmGrpProjRepository.GetByPGID(iPGID);
            vmGrpProject.ProjectStatusText = "ALL";
            List<SelectListItem> items = new List<SelectListItem>();

            items = vmGrpProject.CboProjectsNotInGroup;

            return Json(items, JsonRequestBehavior.AllowGet);

        }
    }
}