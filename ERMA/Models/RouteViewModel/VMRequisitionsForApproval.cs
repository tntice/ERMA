using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TACData;
using TACUtility;
using ERMA.Code;
using ERMA.Models;
using ERMA.Models.Route;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERMA.Models.RouteViewModel
{
    public class VMRequisitionsForApproval
    {
        private ReqDetail _requisitionLineItem = new ReqDetail();
        private Approval _PendingApproval = new Approval();
        private string _approverEmployeeNumber = string.Empty;
        private string _approverUserName = string.Empty;
        private string _approverFullName = string.Empty;


        public VMRequisitionsForApproval() { }

        public ReqDetail RequisitionLineItem { get => _requisitionLineItem; set => _requisitionLineItem = value; }
        public Approval PendingApproval { get => _PendingApproval; set => _PendingApproval = value; }
        public string ApproverEmployeeNumber { get => _approverEmployeeNumber; set => _approverEmployeeNumber = value; }
        public string ApproverUserName { get => _approverUserName; set => _approverUserName = value; }
        public string ApproverFullName { get => _approverFullName; set => _approverFullName = value; }
    }

    public class VMRequisitionForApprovalRepository
    {
        GLAccountRepository glRepository = new GLAccountRepository();
        ProjectRepository prjRepository = new ProjectRepository();


        public List<VMRequisitionsForApproval> GetRequisitionsforApprovalByUserNameAndMasterRequisitionID(string UserName, string MasterReqID)
        {
            List<VMRequisitionsForApproval> lstReturn = new List<VMRequisitionsForApproval>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string strSQL = SQL.GetRequisitionForApprovalByUserNameAndMasterID(UserName, MasterReqID, strSchema);

            try
            {
                dt = Data.GetDataRows(strSQL, cCon);

                if(dt != null)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        VMRequisitionsForApproval appr = new VMRequisitionsForApproval();
                        
                        appr.PendingApproval.PKID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        appr.PendingApproval.RequisitionNumber = Convert.ToInt32(dr.ItemArray[1].ToString());
                        appr.PendingApproval.SupervisorID = dr.ItemArray[2].ToString();
                        appr.PendingApproval.Status = dr.ItemArray[3].ToString();
                        appr.PendingApproval.Level = Convert.ToInt32(dr.ItemArray[4].ToString());
                        appr.PendingApproval.Pass = Convert.ToInt32(dr.ItemArray[5].ToString());
                        appr.PendingApproval.RejectToNumber = dr.ItemArray[6].ToString();
                        appr.PendingApproval.RejectReason = dr.ItemArray[7].ToString();
                        if (dr.ItemArray[8].ToString().Length > 0)
                        {
                            appr.PendingApproval.SubmittedTimeStamp = Convert.ToDateTime(dr.ItemArray[8].ToString());
                        }
                        appr.PendingApproval.SubmittedUserName = dr.ItemArray[9].ToString();
                        appr.ApproverEmployeeNumber = dr.ItemArray[12].ToString();
                        appr.ApproverUserName = dr.ItemArray[13].ToString().ToUpper().Trim();
                        appr.ApproverFullName = dr.ItemArray[14].ToString().ToFirstCharWordsUpper();

                        ReqDetailRepository reqRepository = new ReqDetailRepository();

                        appr.RequisitionLineItem = reqRepository.GetRequisitionDetailByID(dr.ItemArray[15].ToString());

                        appr.RequisitionLineItem.GLAccountList = glRepository.GetGLAccountList(UserName);
                        appr.RequisitionLineItem.ProjectList = prjRepository.GetProjectList(UserName);
                        if(appr.PendingApproval.Status != "P")
                        {
                            appr.RequisitionLineItem.Disabled = "disabled";
                        }
                        else
                        {
                            appr.RequisitionLineItem.Disabled = string.Empty;
                        }

                        if(appr.PendingApproval.Status.Equals("A"))
                        {
                            appr.PendingApproval.ApprovedChecked = "checked";
                            appr.PendingApproval.DeniedChecked = string.Empty;
                        }
                        else if(appr.PendingApproval.Status.Equals("D"))
                        {
                            appr.PendingApproval.ApprovedChecked = string.Empty;
                            appr.PendingApproval.DeniedChecked = "checked";
                        }

                        


                        lstReturn.Add(appr);                        
                            
                    }
                }
            }
            catch { }

            return lstReturn;
        }

    }


}

