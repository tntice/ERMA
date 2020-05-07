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
    public class MNGroupProjectController : Controller
    {

        ProjectGroupRepository pjRepository = new ProjectGroupRepository();
        ProjectRepository prjRepository = new ProjectRepository();
        MNGroupProjectRepository prjGroupRepository = new MNGroupProjectRepository();
        VMGroupProjectRepository vmGrpProjRepository = new VMGroupProjectRepository();
        public int PageSize = 10;
        SecureController sc = new SecureController();

        // GET: MNGroupProject
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

            VMGroupProject mvGrpPrj = new VMGroupProject();

            Int32 iPGID = Convert.ToInt32(PGID);

            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);
                       

            return View(nameof(Index), mvGrpPrj);

            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(VMGroupProject mvGrpPrj)
        {

            return View(nameof(Index), mvGrpPrj);

        }

        [HttpGet]
        public ActionResult Index2(string PGID)
        {
            try { 
            sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            VMGroupProject mvGrpPrj = new VMGroupProject();

            Int32 iPGID = Convert.ToInt32(PGID);

            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);


            return View(nameof(Index2), mvGrpPrj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index2(VMGroupProject mvGrpPrj)
        {

            return View(nameof(Index2), mvGrpPrj);

        }

        [HttpGet]
        public ActionResult List(string PGID, int page = 1)
        {
            try { 
            sc.CheckSecurity(AppRoles.Admin, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            VMGroupProject mvGrpPrj = new VMGroupProject();

            Int32 iPGID = Convert.ToInt32(PGID);

            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);

            mvGrpPrj.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = 10, TotalItems = mvGrpPrj.ProjectList.Count };

            mvGrpPrj.ProjectList = mvGrpPrj.ProjectList.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            
            return View(nameof(List), mvGrpPrj );

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(VMGroupProject mvGrpPrj)
        {
            return View(nameof(Index), mvGrpPrj);
        }


        public ActionResult CreateList(int page)
        {
            try { 
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

            mnGrpPrj.PGID = PGID;
            mnGrpPrj.ProjectID = ProjectNumber;

            ViewBag.GetFocus = "chk" + PGID.ToString() + ProjectNumber;

            Int32 result = 0;

            result = prjGroupRepository.Insert(mnGrpPrj);

            VMGroupProject mvGrpPrj = new VMGroupProject();
            Int32 iPGID = Convert.ToInt32(PGID);
            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);
            mvGrpPrj.ProjectList = mvGrpPrj.ProjectList.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            mvGrpPrj.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = 10, TotalItems = mvGrpPrj.ProjectList.Count };


            return View(nameof(List), mvGrpPrj);

        }

        public ActionResult Create()
        {
            try { 
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

            mnGrpPrj.PGID = PGID;
            mnGrpPrj.ProjectID = ProjectNumber;

            ViewBag.GetFocus = "chk" + PGID.ToString() + ProjectNumber;

            Int32 result = 0;

            result = prjGroupRepository.Insert(mnGrpPrj);

            VMGroupProject mvGrpPrj = new VMGroupProject();
            Int32 iPGID = Convert.ToInt32(PGID);
            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);
            
            return View(nameof(Index), mvGrpPrj);

        }

        public ActionResult Delete()
        {
            try { 
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
            string MNPGID = Request.QueryString["rec.MNPGID"].ToString();

            mnGrpPrj.PGID = PGID;
            mnGrpPrj.ProjectID = ProjectNumber;
            ViewBag.GetFocus = "chk" + PGID.ToString() + ProjectNumber;

            Int32 result = 0;

            result = prjGroupRepository.Delete(MNPGID);

            VMGroupProject mvGrpPrj = new VMGroupProject();
            Int32 iPGID = Convert.ToInt32(PGID);
            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);

            return View(nameof(Index), mvGrpPrj);
        }

        public ActionResult DeleteList(int page)
        {
            try { 
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
            string MNPGID = Request.QueryString["rec.MNPGID"].ToString();

            mnGrpPrj.PGID = PGID;
            mnGrpPrj.ProjectID = ProjectNumber;
            ViewBag.GetFocus = "chk" + PGID.ToString() + ProjectNumber;

            Int32 result = 0;

            result = prjGroupRepository.Delete(MNPGID);

            VMGroupProject mvGrpPrj = new VMGroupProject();
            Int32 iPGID = Convert.ToInt32(PGID);
            mvGrpPrj = vmGrpProjRepository.GetByPGID(iPGID);
            mvGrpPrj.ProjectList = mvGrpPrj.ProjectList.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            mvGrpPrj.PagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = 10, TotalItems = mvGrpPrj.ProjectList.Count };

            return View(nameof(List), mvGrpPrj);
        }
    }
}