using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERMA.App_Code;
using ERMA.Code;
using TACUtility;
using ERMA.Models;
using System.Data;


namespace ERMA.Controllers
{
    public class ReqDetailController : Controller
    {
        RequisitionerRepository reqRepository = new RequisitionerRepository();
        VendorRepository vnRepository = new VendorRepository();
        GLAccountRepository glRepository = new GLAccountRepository();
        UoMRepository uomRepository = new UoMRepository();
        TaxGroupRepository tgRepository = new TaxGroupRepository();
        TaxRateRepository trRepository = new TaxRateRepository();
        ProjectRepository prjRepository = new ProjectRepository();
        VendorPartRepository vpRepository = new VendorPartRepository();
        TaxRateDetailRepository trdRepository = new TaxRateDetailRepository();
        SecureController sc = new SecureController();

        // GET: ReqDetail
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
                if (Session[SessionName.ReqID.ToString()].ToString().Length > 0)
                {
                    ViewBag.ReqMsg = String.Format("Requisition Line Item {0} successfully added", Session[SessionName.ReqID.ToString()].ToString());
                }
                else
                {
                    if (Session[SessionName.ReqError.ToString()].ToString().Length > 0)
                    {
                        ViewBag.ReqMsg = String.Format("{0}", Session[SessionName.ReqError.ToString()].ToString());
                    }
                    else
                    {
                        ViewBag.ReqMsg = "";
                    }
                }
            }

            catch
            {
                ViewBag.ReqMsg = "";
            }

            ViewBag.ShowPrice = "block";
            ViewBag.ShowLineItemPrice = "none";
            return View();
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


            ReqDetail rd = new ReqDetail();

            rd.RequisitionNumber = 0;

            rd.RequestID = "ReqID".ToUniqueID();
            rd.RequestMode = "ReqModeID".ToUniqueID();
            rd.CMSDatabase = Properties.Settings.Default.DB2Database.ToString();
            rd.Qty = 1;
            rd.UnitOfMeasure = "EA";
            rd.OrderUnit = "EA";
            rd.RequiredDate = DateTime.Now.AddDays(30);
            rd.InventoryStockroom = "DFTSTK";
            rd.Buyer = "SYS";

            ViewBag.ShowPrice = "block";
            ViewBag.ShowLineItemPrice = "none";
            //rd.GlAccount = 86833430;

            Requisitioner rq = new Requisitioner();
            string u = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper(); //In case we ever want to use Windows Login ID
            u = Session[SessionName.UserName.ToString()].ToString().ToUpper();  //Use the CMS User name
            rq = reqRepository.GetRequisitionerByID(u);
            rd.Requisitioner = rq.ID;

            rd.GLAccountList.Clear();
            rd.GLAccountList = glRepository.GetGLAccountList(u);

            rd.UoMList.Clear();
            rd.UoMList = uomRepository.GetUoMList();

            rd.TaxGroupList.Clear();
            rd.TaxGroupList = tgRepository.GetTaxGroupList();

            rd.ProjectList.Clear();
            rd.ProjectList = prjRepository.GetProjectList(u);


            try
            {
                if (!(Session[SessionName.GLAccount.ToString()] is null))
                {
                    rd.GlAccount = Convert.ToDecimal(Session[SessionName.GLAccount.ToString()].ToString());
                    rd.DeptCode = GetDeptCode(rd.GlAccount);

                }
                else
                {
                    var item = rd.GLAccountList.FirstOrDefault();
                    rd.GlAccount = item.Number;
                    rd.DeptCode = GetDeptCode(rd.GlAccount);
                    
                }

                if (!(Session[SessionName.DateRequired.ToString()] is null))
                {
                    rd.RequiredDate = Convert.ToDateTime(Session[SessionName.DateRequired.ToString()].ToString());
                }

                if (!(Session[SessionName.Dept.ToString()] is null))
                {
                    rd.DeptCode = Session[SessionName.Dept.ToString()].ToString();
                }

                if (!(Session[SessionName.VendorCode.ToString()] is null))
                {
                    rd.VendorCode = Session[SessionName.VendorCode.ToString()].ToString();

                    Vendor vndr = vnRepository.GetVendorByID(rd.VendorCode);
                    rd.VendorName = vndr.VendorName;
                    rd.TaxGroupCode = vndr.TaxGroupCode;
                    
                    if (rd.TaxGroupCode.Length > 0)
                    {
                        rd.TaxRateList.Clear();
                        rd.TaxRateList = trRepository.GetTaxRateList(rd.TaxGroupCode);
                    }
                    else
                    {
                        rd.TaxRateList.Clear();
                    }

                    rd.VendorPartList.Clear();
                    rd.VendorPartList = vpRepository.GetVendorPartList(string.Empty, rd.VendorCode);


                }
                if (!(Session[SessionName.ProjectCode.ToString()] is null))
                {
                    rd.ProjectCode = Session[SessionName.ProjectCode.ToString()].ToString();
                }
            }
            catch { }
            try
            {
                string strMasterReq = Session[SessionName.MReqID.ToString()].ToString();      
                if(strMasterReq.IsNumeric())
                {
                    rd.MasterRequisitionNumber = Convert.ToDecimal(strMasterReq);

                }
            }
            catch
            {
                rd.MasterRequisitionNumber = 0;
            }

            return View(rd);
        }

        [HttpPost]
        public ActionResult Create(ReqDetail rd)
        {
            decimal ReqID = 0;
            string Result = string.Empty;
            string u = Session[SessionName.UserName.ToString()].ToString();
            string p = Session[SessionName.Password.ToString()].ToString();

            Session[SessionName.ReqID.ToString()] = "";
            Session[SessionName.ReqError.ToString()] = "";
            ViewBag.ShowPrice = "block";
            ViewBag.ShowLineItemPrice = "none";

            if (ModelState.IsValid)
            {
                try
                {
                    Session[SessionName.GLAccount.ToString()] = rd.GlAccount;
                    Session[SessionName.Dept.ToString()] = rd.DeptCode;
                    Session[SessionName.DateRequired.ToString()] = rd.RequiredDate.ToShortDateString();
                    Session[SessionName.VendorCode.ToString()] = rd.VendorCode;
                    Session[SessionName.ProjectCode.ToString()] = rd.ProjectCode;
                }
                catch { }

                rd.GLAccountList.Clear();
                rd.GLAccountList = glRepository.GetGLAccountList(u);

                rd.UoMList.Clear();
                rd.UoMList = uomRepository.GetUoMList();

                rd.TaxGroupList.Clear();
                rd.TaxGroupList = tgRepository.GetTaxGroupList();

                rd.ProjectList.Clear();
                rd.ProjectList = prjRepository.GetProjectList(u);

                rd.VendorPartList.Clear();
                rd.VendorPartList = vpRepository.GetVendorPartList(string.Empty, rd.VendorCode);

                if (rd.TaxGroupCode.Length > 0)
                {
                    rd.TaxRateList.Clear();
                    rd.TaxRateList = trRepository.GetTaxRateList(rd.TaxGroupCode);
                }
                

                CMSAPI api = new CMSAPI();

                Result = api.AddRequisition(rd, u, p);

                if(Result.IsNumeric())
                {
                    ReqID = Convert.ToDecimal(Result);

                    Session[SessionName.ReqID.ToString()] = Result;
                }
                else
                {
                    Session[SessionName.ReqError.ToString()] = "Error creating Requisition Line Item: " + Result;
                    return View(rd);
                }
                                               
                return RedirectToAction("Index", "ReqDetail");

            }
            else
            {

                rd.GLAccountList.Clear();
                rd.GLAccountList = glRepository.GetGLAccountList(u);

                rd.UoMList.Clear();
                rd.UoMList = uomRepository.GetUoMList();

                rd.TaxGroupList.Clear();
                rd.TaxGroupList = tgRepository.GetTaxGroupList();

                rd.VendorPartList.Clear();
                rd.VendorPartList = vpRepository.GetVendorPartList(string.Empty, rd.VendorCode);

                if (rd.TaxGroupCode.Length > 0)
                {
                    rd.TaxRateList.Clear();
                    rd.TaxRateList = trRepository.GetTaxRateList(rd.TaxGroupCode);
                }
                else
                {
                    rd.TaxRateList.Clear();
                }

                rd.ProjectList.Clear();
                rd.ProjectList = prjRepository.GetProjectList(u);
                
                return View(rd);
            }

        }
        
        public ActionResult GetVendorList(string VendorFilter)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Vendor> vendors = new List<Vendor>();
            
            vendors = vnRepository.GetVendorList(VendorFilter);


            foreach(Vendor vn in vendors)
            {
                items.Add(new SelectListItem() { Text = vn.VendorCode + " - " + vn.VendorName, Value = vn.VendorCode });
            }

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetVendorPartList(string PartFilter)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<VendorPart> vendorParts = new List<VendorPart>();
            string VendorCode = Session[SessionName.VendorCode.ToString()].ToString();

            vendorParts = vpRepository.GetVendorPartList(PartFilter, VendorCode);

            foreach(VendorPart vp in vendorParts)
            {
                items.Add(new SelectListItem() { Text = vp.PartNumber + " - " + vp.Description, Value = vp.PartNumber });
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVendorPart(string PartNumber)
        {
            string VendorCode = Session[SessionName.VendorCode.ToString()].ToString();
            VendorPart vp = new VendorPart();

            vp = vpRepository.GetVendorPart(PartNumber, VendorCode);

            return Json(vp.Description, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxRateList(string TaxGroup)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<TaxRate> taxRates = new List<TaxRate>();

            taxRates = trRepository.GetTaxRateList(TaxGroup);

            foreach(TaxRate tr in taxRates)
            {
                items.Add(new SelectListItem() { Text = tr.ListDisplay, Value = tr.Code });
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public string GetDeptCode(decimal GLAccnt)
        {
            string returnValue = string.Empty;
            try
            {
                if (GLAccnt > 0)
                {
                    string strGL = GLAccnt.ToString();
                    strGL = strGL.Substring(0, 2);
                    returnValue = strGL;
                }
            }
            catch
            {
                returnValue = string.Empty;
            }

            return returnValue;
        }
    }
}