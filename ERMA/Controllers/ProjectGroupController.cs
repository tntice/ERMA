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
    public class ProjectGroupController : Controller
    {

        ProjectGroupRepository pjRepository = new ProjectGroupRepository();
        MNGroupProjectRepository mnGrpPrjRepository = new MNGroupProjectRepository();
        MNGroupRequisitionerRepository mnGrpReqRepository = new MNGroupRequisitionerRepository();
        CMSMethods cmsMethods = new CMSMethods();
        SecureController sc = new SecureController();

        // GET: ProjectGroup
        public ActionResult Index()
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

            return RedirectToAction(nameof(ListByStatus), new { status = "A" });
        }
    
        public ActionResult ListByStatus(string status)
        {
            List<ProjectGroup> pgs = pjRepository.GetProjectGroupsByStatus(status);

            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            return View(pgs);
        }

        [HttpGet]        
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

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectGroup prjGroup)
        {
            try
            { 
                sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            Int32 result = 0;

            if(ModelState.IsValid)
            {
                result = pjRepository.Insert(prjGroup);
            }
            else
            {
                return View();
            }

            if(result.Equals(1))
            {
                return RedirectToAction(nameof(ListByStatus), new { status = "A" });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string PGID)
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

            ProjectGroup prjGroup = pjRepository.GetProjectGroupByID(PGID);

            //workingFilter.CboDivisions.Add(new SelectListItem() { Text = "Select a Division", Value = "0" });
            prjGroup.StatusList.Clear();
            prjGroup.StatusList.Add(new Status("A", "Active"));
            prjGroup.StatusList.Add(new Status("I", "Inactive"));



            return View(prjGroup);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectGroup prjGroup)
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

            prjGroup.StatusList.Clear();
            prjGroup.StatusList.Add(new Status("A", "Active"));
            prjGroup.StatusList.Add(new Status("I", "Inactive"));

            if (ModelState.IsValid)
            {
                pjRepository.Update(prjGroup);
                return RedirectToAction(nameof(ListByStatus), new { status = "A" });

            }
            else
            {
                return View(prjGroup);
            }
        }

        [HttpGet]
        public ActionResult Delete(string PGID)
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

            
            ProjectGroup prjGroup = pjRepository.GetProjectGroupByID(PGID);
            //Int32 result = pjRepository.Delete(prjGroup);

            return View(prjGroup);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProjectGroup prjGroup)
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

            if (ModelState.IsValid)
            {

                Int32 iMNGrpProj = 0;
                Int32 iMNGrpReq = 0;
                Int32 iPGID = Convert.ToInt32(prjGroup.PGID.ToString());

                cmsMethods.RemoveAllPermissions(prjGroup.PGID.ToString());
                iMNGrpProj = mnGrpPrjRepository.Delete(iPGID);
                iMNGrpReq = mnGrpReqRepository.Delete(iPGID);

                pjRepository.Delete(prjGroup);
                return RedirectToAction(nameof(ListByStatus), new { status = "A" });

            }
            else
            {
                return View(prjGroup);
            }
        }

    }

}