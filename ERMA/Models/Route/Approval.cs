using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TACData;
using TACUtility;
using ERMA.Code;
using ERMA.Models;
using ERMA.Models.RouteViewModel;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERMA.Models.Route
{
    public class Approval
    {
        private Int32 _PKID = 0;
        private Int32 _requisitionNumber = 0;
        private string _SupervisorID = string.Empty;
        private string _status = "P";
        private Int32 _level = 1;
        private Int32 _pass = 1;
        private string _rejectToNumber = string.Empty;
        private string _rejectReason = string.Empty;
        private DateTime _submittedTimeStamp = DateTime.Now;
        private string _submittedUserName = string.Empty;
        private DateTime _updatedTimeStamp = DateTime.Now;
        private string _updatedUserName = string.Empty;
        private string _ApprovedChecked = string.Empty;
        private string _DeniedChecked = string.Empty;

        public int PKID { get => _PKID; set => _PKID = value; }
        public int RequisitionNumber { get => _requisitionNumber; set => _requisitionNumber = value; }
        public string SupervisorID { get => _SupervisorID; set => _SupervisorID = value; }
        public string Status { get => _status; set => _status = value; }
        public int Level { get => _level; set => _level = value; }
        public int Pass { get => _pass; set => _pass = value; }
        public string RejectToNumber { get => _rejectToNumber; set => _rejectToNumber = value; }
        public string RejectReason { get => _rejectReason; set => _rejectReason = value; }
        public DateTime SubmittedTimeStamp { get => _submittedTimeStamp; set => _submittedTimeStamp = value; }
        public string SubmittedUserName { get => _submittedUserName; set => _submittedUserName = value; }
        public DateTime UpdatedTimeStamp { get => _updatedTimeStamp; set => _updatedTimeStamp = value; }
        public string UpdatedUserName { get => _updatedUserName; set => _updatedUserName = value; }
        public string ApprovedChecked { get => _ApprovedChecked; set => _ApprovedChecked = value; }
        public string DeniedChecked { get => _DeniedChecked; set => _DeniedChecked = value; }

        public Approval() { }

    }

    public class ApproveModel
    {
        private string _ID = string.Empty;
        private string _Action = string.Empty;
        private string _Reason = string.Empty;

        public string ID { get => _ID; set => _ID = value; }
        public string Action { get => _Action; set => _Action = value; }
        public string Reason { get => _Reason; set => _Reason = value; }
    }
    public class ApprovalRepository
    {

        private ReqDetailRepository reqDetRepository = new ReqDetailRepository();
        private ManagerRepository mgrRepository = new ManagerRepository();
        private SupervisorRepository supRepository = new SupervisorRepository();
        private SkipSupervisortRepository skipRepository = new SkipSupervisortRepository();
        private MasterRequisitionRepository mReqRepository = new MasterRequisitionRepository();
        private VMMasterRequisitionRepository vmMReqRepository = new VMMasterRequisitionRepository();
        private ApprovalLimitRepository limitRepository = new ApprovalLimitRepository();


        public List<Approval> GetApprovalList(string RequisitionNumber)
        {
            List<Approval> lstReturn = new List<Approval>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetRouteByRequisitionNumber(RequisitionNumber);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Approval apr = new Approval();

                        apr.PKID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        apr.RequisitionNumber = Convert.ToInt32(dr.ItemArray[1].ToString());
                        apr.SupervisorID = dr.ItemArray[2].ToString().Trim().ToUpper();
                        apr.Status = dr.ItemArray[3].ToString().Trim().ToUpper();
                        apr.Level = Convert.ToInt32(dr.ItemArray[4].ToString());
                        apr.Pass = Convert.ToInt32(dr.ItemArray[5].ToString());
                        apr.RejectToNumber = dr.ItemArray[6].ToString().Trim().ToUpper();
                        apr.RejectReason = dr.ItemArray[7].ToString().Trim();
                        apr.SubmittedTimeStamp = Convert.ToDateTime(dr.ItemArray[8].ToString());
                        apr.SubmittedUserName = dr.ItemArray[9].ToString();
                        if(dr.ItemArray[10].ToString().Length > 0)
                        {
                            apr.UpdatedTimeStamp = Convert.ToDateTime(dr.ItemArray[10].ToString());                  
                        }
                        apr.UpdatedUserName = dr.ItemArray[11].ToString();

                        lstReturn.Add(apr);
                    }
                }
            }
            catch
            {

            }
            return lstReturn;

        }

        public List<Approval> GetApprovalListByStatus(string RequisitionNumber, string Status)
        {
            List<Approval> lstReturn = new List<Approval>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetRouteByRequisitionNumberAndStatus(RequisitionNumber, Status);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Approval apr = new Approval();

                        apr.PKID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        apr.RequisitionNumber = Convert.ToInt32(dr.ItemArray[1].ToString());
                        apr.SupervisorID = dr.ItemArray[2].ToString().Trim().ToUpper();
                        apr.Status = dr.ItemArray[3].ToString().Trim().ToUpper();
                        apr.Level = Convert.ToInt32(dr.ItemArray[4].ToString());
                        apr.Pass = Convert.ToInt32(dr.ItemArray[5].ToString());
                        apr.RejectToNumber = dr.ItemArray[6].ToString().Trim().ToUpper();
                        apr.RejectReason = dr.ItemArray[7].ToString().Trim();
                        apr.SubmittedTimeStamp = Convert.ToDateTime(dr.ItemArray[8].ToString());
                        apr.SubmittedUserName = dr.ItemArray[9].ToString();
                        if (dr.ItemArray[10].ToString().Length > 0)
                        {
                            apr.UpdatedTimeStamp = Convert.ToDateTime(dr.ItemArray[10].ToString());
                        }
                        apr.UpdatedUserName = dr.ItemArray[11].ToString();

                        lstReturn.Add(apr);
                    }
                }
            }
            catch
            {

            }
            return lstReturn;

        }

        public Int32 InsertNextLevelForApproval(Approval apr)
        {
            Int32 returnValue = 0;
            decimal totalPriceWithoutTaxes = 0;
           
            Int32 nextLimit = 0;
            decimal decNextLimit = 0;
            Int32 currLimit = 0;
            decimal decCurrLimit = 0;


            try
            {
                ReqDetail rd = new ReqDetail();
                rd = reqDetRepository.GetRequisitionDetailByID(apr.RequisitionNumber.ToString());
                //Get the Department
                Supervisor sup = new Supervisor();
                            
                List<ReqDetail> rdList = reqDetRepository.GetRequisitionsByMasterReqID(rd.MasterRequisitionNumber.ToString());

                foreach (ReqDetail r in rdList)
                {
                    totalPriceWithoutTaxes += r.Qty * r.PerUnitPrice;
                }


                if (apr.Status.Equals("A"))
                {
                    if(apr.Level.Equals(1))
                    {
                                     
                        
                        //Get the Department Manager
                        Manager mgr = new Manager();
                        mgr = mgrRepository.GetManagerByDept(rd.DeptCode);
                        

                        //Make sure they are not the same person
                        if(apr.SupervisorID.Equals(mgr.EmpNum))
                        {
                            //Person already processed this record.  Find the next Supervisor
                            sup = supRepository.GetSupervisor(mgr.EmpNum);
                            currLimit = limitRepository.GetApprovalLimit(sup.UnderlingJobClass, sup.UnderlingTitle);
                            decCurrLimit = Convert.ToDecimal(currLimit);

                            nextLimit = limitRepository.GetApprovalLimit(sup.SupervisorJobClass, sup.SupervisorTitle);
                            decNextLimit = Convert.ToDecimal(nextLimit);

                            //Do we skip this supervisor?
                            sup = getNextSupervisor(sup, rd);

                            
                            if(decCurrLimit.Equals(decNextLimit))
                            {
                                //Add the next approval route
                                Approval routeApproval = new Approval();

                                routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                                routeApproval.SupervisorID = sup.SupervisorEmpNum;
                                routeApproval.Level = apr.Level + 1;
                                routeApproval.Pass = apr.Pass;
                                routeApproval.Status = "P";
                                routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                                returnValue = InsertApprovalRoute(routeApproval);

                                //SendEmailNotice(SupID, reqDet, MasterReqID, mReq.Description1);

                            }
                            else if(totalPriceWithoutTaxes >= decCurrLimit)
                            {
                                Approval routeApproval = new Approval();
                            
                                routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                                routeApproval.SupervisorID = sup.SupervisorEmpNum;
                                routeApproval.Level = apr.Level + 1;
                                routeApproval.Pass = apr.Pass;
                                routeApproval.Status = "P";
                                routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                                returnValue = InsertApprovalRoute(routeApproval);
                            }
                            else if(sup.UnderlingTitle.ToUpper().Equals("PRESIDENT"))
                            {
                                //Final Approval on Requisition
                                rd.Status = "Y";
                                returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);
                            }
                            else
                            {
                                //Final Approval on Requisition
                                rd.Status = "Y";
                                returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);

                            }
                           
                            //returnValue = 2;
                        }
                        else
                        {
                            //Person also needs to approve this record.
                            sup = supRepository.GetSupervisor(apr.SupervisorID);
                            currLimit = limitRepository.GetApprovalLimit(sup.UnderlingJobClass, sup.UnderlingTitle);
                            decCurrLimit = Convert.ToDecimal(currLimit);

                            nextLimit = limitRepository.GetApprovalLimit(sup.SupervisorJobClass, sup.SupervisorTitle);
                            decNextLimit = Convert.ToDecimal(nextLimit);

                            //Do we skip this supervisor?
                            sup = getNextSupervisor(sup, rd);


                            if (decCurrLimit.Equals(decNextLimit))
                            {
                                //Add the next approval route
                                Approval routeApproval = new Approval();

                                routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                                routeApproval.SupervisorID = sup.SupervisorEmpNum;
                                routeApproval.Level = apr.Level + 1;
                                routeApproval.Pass = apr.Pass;
                                routeApproval.Status = "P";
                                routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                                returnValue = InsertApprovalRoute(routeApproval);

                            }
                            else if (totalPriceWithoutTaxes >= decCurrLimit)
                            {
                                Approval routeApproval = new Approval();

                                routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                                routeApproval.SupervisorID = sup.SupervisorEmpNum;
                                routeApproval.Level = apr.Level + 1;
                                routeApproval.Pass = apr.Pass;
                                routeApproval.Status = "P";
                                routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                                returnValue = InsertApprovalRoute(routeApproval);
                            }
                            else if (sup.UnderlingTitle.ToUpper().Equals("PRESIDENT"))
                            {
                                //Final Approval on Requisition
                                rd.Status = "Y";
                                returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);
                            }
                            else
                            {
                                //Final Approval on Requisition
                                rd.Status = "Y";
                                returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);

                            }
                            //returnValue = 3;
                        }

                    }
                    else if(apr.Level.Equals(2))
                    {
                        //Person also needs to approve this record.
                        sup = supRepository.GetSupervisor(apr.SupervisorID);
                        currLimit = limitRepository.GetApprovalLimit(sup.UnderlingJobClass, sup.UnderlingTitle);
                        decCurrLimit = Convert.ToDecimal(currLimit);

                        nextLimit = limitRepository.GetApprovalLimit(sup.SupervisorJobClass, sup.SupervisorTitle);
                        decNextLimit = Convert.ToDecimal(nextLimit);

                        //Do we skip this supervisor?
                        sup = getNextSupervisor(sup, rd);


                        if (decCurrLimit.Equals(decNextLimit))
                        {
                            //Add the next approval route
                            Approval routeApproval = new Approval();

                            routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                            routeApproval.SupervisorID = sup.SupervisorEmpNum;
                            routeApproval.Level = apr.Level + 1;
                            routeApproval.Pass = apr.Pass;
                            routeApproval.Status = "P";
                            routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                            returnValue = InsertApprovalRoute(routeApproval);

                        }
                        else if (totalPriceWithoutTaxes >= decCurrLimit)
                        {
                            Approval routeApproval = new Approval();

                            routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                            routeApproval.SupervisorID = sup.SupervisorEmpNum;
                            routeApproval.Level = apr.Level + 1;
                            routeApproval.Pass = apr.Pass;
                            routeApproval.Status = "P";
                            routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                            returnValue = InsertApprovalRoute(routeApproval);
                        }
                        else if (sup.UnderlingTitle.ToUpper().Equals("PRESIDENT"))
                        {
                            //Final Approval on Requisition
                            rd.Status = "Y";
                            returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);
                        }
                        else
                        {
                            //Final Approval on Requisition
                            rd.Status = "Y";
                            returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);

                        }

                    }
                    else
                    {
                        //All other approval Levels
                        //Person also needs to approve this record.
                        sup = supRepository.GetSupervisor(apr.SupervisorID);
                        currLimit = limitRepository.GetApprovalLimit(sup.UnderlingJobClass, sup.UnderlingTitle);
                        decCurrLimit = Convert.ToDecimal(currLimit);

                        nextLimit = limitRepository.GetApprovalLimit(sup.SupervisorJobClass, sup.SupervisorTitle);
                        decNextLimit = Convert.ToDecimal(nextLimit);

                        //Do we skip this supervisor?
                        sup = getNextSupervisor(sup, rd);


                        if (decCurrLimit.Equals(decNextLimit))
                        {
                            //Add the next approval route
                            Approval routeApproval = new Approval();

                            routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                            routeApproval.SupervisorID = sup.SupervisorEmpNum;
                            routeApproval.Level = apr.Level + 1;
                            routeApproval.Pass = apr.Pass;
                            routeApproval.Status = "P";
                            routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                            returnValue = InsertApprovalRoute(routeApproval);

                        }
                        else if (totalPriceWithoutTaxes >= decCurrLimit)
                        {
                            Approval routeApproval = new Approval();

                            routeApproval.RequisitionNumber = Convert.ToInt32(rd.RequestID.ToString());
                            routeApproval.SupervisorID = sup.SupervisorEmpNum;
                            routeApproval.Level = apr.Level + 1;
                            routeApproval.Pass = apr.Pass;
                            routeApproval.Status = "P";
                            routeApproval.SubmittedUserName = sup.UnderlingEmpNum;

                            returnValue = InsertApprovalRoute(routeApproval);
                        }
                        else if (sup.UnderlingTitle.ToUpper().Equals("PRESIDENT"))
                        {
                            //Final Approval on Requisition
                            rd.Status = "Y";
                            returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);
                        }
                        else
                        {
                            //Final Approval on Requisition
                            rd.Status = "Y";
                            returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);

                        }

                    }
                }
                else if (apr.Status.Equals("D"))
                {
                    rd.Status = "R";
                    returnValue = reqDetRepository.UpdateRequisitionApproval(rd, apr.UpdatedUserName);
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        private Supervisor getNextSupervisor(Supervisor sup, ReqDetail rdt)
        {
            Supervisor nextSupervisor = new Supervisor();
            if(skipRepository.IsSupervisorSkipped(rdt.DeptCode, sup.SupervisorJobClass))
            {
                nextSupervisor = supRepository.GetSupervisor(sup.SupervisorEmpNum);
                return getNextSupervisor(nextSupervisor, rdt);
            }
            else
            {
                return sup;
            }
        }

        public Int32 GetRouteMaxLevel(string RequisitionNumber)
        {
            Int32 returnValue = 1;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();

            string strSQL = SQL.GetRouteMaxLevelByRequisitionNumber(RequisitionNumber);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if(drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch
            {
                
            }
            return returnValue;
        }

        public Int32 GetRouteMaxLevelByStatus(string RequisitionNumber, Int32 Pass)
        {
            Int32 returnValue = 1;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();

            string strSQL = SQL.GetRouteMaxLevelByRequisitionNumberAndPass(RequisitionNumber, Pass);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch
            {

            }
            return returnValue;
        }

        public Int32 GetRouteMaxPass(string RequisitionNumber)
        {
            Int32 returnValue = 1;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();

            string strSQL = SQL.GetRouteMaxPassByRequisitionNumber(RequisitionNumber);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch
            {

            }
            return returnValue;
        }

        public Int32 InsertApprovalRoute(Approval apr)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACNonDataType ndt = new TACNonDataType();

            string strSQL = SQL.InsertRoute(apr);
            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                returnValue = ndt.CountAffected;
            }
            catch
            {
                returnValue = -1;
            }

            return returnValue;
        }

        public Int32 UpdateApprovalRoute(Approval apr)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACNonDataType ndt = new TACNonDataType();

            string strSQL = SQL.UpdateRoute(apr);

            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                returnValue = ndt.CountAffected;
            }
            catch
            {
                returnValue = -1;
            }

            return returnValue;
        }

    }
}