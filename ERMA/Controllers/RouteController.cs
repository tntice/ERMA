using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERMA.App_Code;
using ERMA.Code;
using TACUtility;
using ERMA.Models;
using ERMA.Models.Route;
using ERMA.Models.RouteViewModel;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace ERMA.Controllers
{
    public class RouteController : Controller
    {
        RequisitionerRepository reqRepository = new RequisitionerRepository();
        SkipSupervisortRepository skipRepository = new SkipSupervisortRepository();
        SupervisorRepository supRepository = new SupervisorRepository();
        ManagerRepository mgrRepository = new ManagerRepository();
        ApprovalLimitRepository limitRepository = new ApprovalLimitRepository();
        ApprovalRepository approvalRepository = new ApprovalRepository();
        MasterRequisitionRepository masterRepository = new MasterRequisitionRepository();
        ReqDetailRepository reqDetRepository = new ReqDetailRepository();
        PersonRepository perRepository = new PersonRepository();
        VMMasterRequisitionRepository vmMasterRepository = new VMMasterRequisitionRepository();
        VMRequisitionForApprovalRepository vmReqRepository = new VMRequisitionForApprovalRepository();
        SecureController sc = new SecureController();


        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            try
            { 
                sc.CheckSecurity(AppRoles.All, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }


            string User = Session[SessionName.UserName.ToString()].ToString();

            if (User.ToUpper().Equals("TTILLOTSON"))
            {
                User = "RGRAVES";
            }

            ViewBag.ShowPrice = "none";
            ViewBag.ShowLineItemPrice = "none";

            List<VMMasterRequisition> vmMasters = new List<VMMasterRequisition>();
            vmMasters = vmMasterRepository.GetVMMasterRequisitionsByUserName(User);

            
            return View(vmMasters);


        }

        public ActionResult ProcessApproval(ApproveModel am)
        {
            List<Approval> vMApproval = new List<Approval>();
            Int32 iReturnValue = 0;
            Int32 iNextValue = 0;


            vMApproval = approvalRepository.GetApprovalListByStatus(am.ID, "P");

            foreach (Approval ap in vMApproval)
            {
                if (ap.Status.ToUpper().Equals("P"))
                {
                    if (am.Action.ToUpper().Equals("APPROVE"))
                    {
                        ap.Status = "A";
                    }
                    else
                    {
                        ap.Status = "D";
                        ap.RejectReason = am.Reason;
                    }
                    string User = Session[SessionName.UserName.ToString()].ToString();

                    if (User.ToUpper().Equals("TTILLOTSON"))
                    {
                        User = "RGRAVES";
                    }

                    ap.UpdatedUserName = User;
                    
                    iReturnValue = approvalRepository.UpdateApprovalRoute(ap);

                    iNextValue = approvalRepository.InsertNextLevelForApproval(ap);


                }

            }
            return View(iReturnValue);
        }

        public ActionResult SubmitReqSupervisor()
        {
            try
            { 
                sc.CheckSecurity(AppRoles.All, Session[SessionName.UserName.ToString()].ToString());
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }

            string MasterReqID = Session[SessionName.MReqID.ToString()].ToString();
            string SupID = string.Empty;

            MasterRequisition mReq = masterRepository.GetMasterRequisitionByID(MasterReqID);
            List<ReqDetail> reqDet = reqDetRepository.GetRequisitionsByMasterReqID(MasterReqID);
            Requisitioner reqr = reqRepository.GetRequisitionerByUserName(Session[SessionName.UserName.ToString()].ToString());

            foreach (ReqDetail req in reqDet)
            {
                Approval routeApproval = new Approval();

                routeApproval.RequisitionNumber = Convert.ToInt32(req.RequestID.ToString());
                routeApproval.SupervisorID = GetRequisitionerManager(reqr.ID);
                routeApproval.Level = 1;
                routeApproval.Pass = 1;
                routeApproval.Status = "P";
                routeApproval.SubmittedUserName = reqr.UserName;

                Int32 returnValue = approvalRepository.InsertApprovalRoute(routeApproval);
                SupID = routeApproval.SupervisorID;
            }
            SendEmailNotice(SupID, reqDet, MasterReqID, mReq.Description1);
                       
            return RedirectToAction("Index", "Home");
        }

        private string GetRequisitionerManager(string EmpNum)
        {
            string returnValue = string.Empty;

            string NextSup = string.Empty;

            Supervisor sup = new Supervisor();

            sup = supRepository.GetSupervisor(EmpNum);

            if ((sup.SupervisorJobClass.ToUpper().Equals("MANAGER")) || (sup.SupervisorJobClass.ToUpper().Equals("AGM")))
            {
                returnValue = sup.SupervisorEmpNum;
            }
            else
            {
                returnValue = GetRequisitionerManager(sup.SupervisorEmpNum);
            }

            return returnValue;
        }

        private void SendEmailNotice(string SupID, List<ReqDetail> reqDets, string mReqID, string masterDesc)
        {

            Person per = perRepository.GetPersonByEmpNum(SupID);

            MailMessage mail = new MailMessage();
            String[] eAddresses;
            StringBuilder sbMessage = new StringBuilder();


            //eAddresses = new string[] { per.Email.ToString(), "ttillotson@toyotomiam.com" };
            eAddresses = new string[] { "ttillotson@toyotomiam.com" };

            foreach (string eTo in eAddresses)
            {
                mail.To.Add(eTo);
            }
            mail.From = new MailAddress("ERMA@toyotomiam.com");

            mail.Subject = "ERMA Requisition needs your Approval";
            mail.Priority = MailPriority.High;

            sbMessage.AppendLine(per.FullName.ToFirstCharWordsUpper() + ": You have a requisition that requires your approval.");
            sbMessage.Append(Environment.NewLine);
            sbMessage.AppendLine("Master Requisition Number: " + mReqID + " - " + masterDesc.Trim() + ".");
            sbMessage.AppendLine(Environment.NewLine);

            foreach (ReqDetail reqDet in reqDets)
            {
                sbMessage.AppendLine("    " + reqDet.RequestID.Trim() + " - " + reqDet.VendorPartNumber.Trim() + " - " + reqDet.Qty.ToString() + " " + reqDet.UnitOfMeasure);
            }

            sbMessage.AppendLine(Environment.NewLine);
            sbMessage.AppendLine("Regards, ");
            sbMessage.AppendLine("ERMA Administrator");

            mail.Body = sbMessage.ToString();


            //Setup server
            SmtpClient smtp = new SmtpClient("172.16.1.72");

            try
            {
                smtp.Send(mail);
            }
            catch
            {

            }
        }

    }
}