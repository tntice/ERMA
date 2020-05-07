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
    public class VMMasterRequisition
    {
        private MasterRequisition _masterRequisition = new MasterRequisition();
        private List<VMRequisitionsForApproval> _forApprovalLineItem = new List<VMRequisitionsForApproval>();
        private Decimal _TotalWithoutTaxes = 0;

        public MasterRequisition VmMasterRequisition { get => _masterRequisition; set => _masterRequisition = value; }
        public List<VMRequisitionsForApproval> ForApprovalLineItem { get => _forApprovalLineItem; set => _forApprovalLineItem = value; }
        public decimal TotalWithoutTaxes { get => _TotalWithoutTaxes; set => _TotalWithoutTaxes = value; }
    }

    public class VMMasterRequisitionRepository
    {
        VMRequisitionForApprovalRepository VMReqRepository = new VMRequisitionForApprovalRepository();
        MasterRequisitionRepository MReqRepository = new MasterRequisitionRepository();

        public List<VMMasterRequisition> GetVMMasterRequisitionsByUserName(string UserName)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            List<VMMasterRequisition> lstResponse = new List<VMMasterRequisition>();

            string strSQL = SQL.GetMasterRequisitionForApprovalByUserName(UserName, strSchema);

            TACDataTableType dtt = new TACDataTableType();

            try
            {
                dtt = Data.GetTACDataRows(strSQL, cCon);

                if(dtt.HasData)
                {
                    foreach(DataRow dr in dtt.Dtable.Rows)
                    {
                        VMMasterRequisition MReq = new VMMasterRequisition();

                        MReq.VmMasterRequisition = MReqRepository.GetMasterRequisitionByID(dr.ItemArray[0].ToString());

                        MReq.ForApprovalLineItem = VMReqRepository.GetRequisitionsforApprovalByUserNameAndMasterRequisitionID(UserName, MReq.VmMasterRequisition.MasterReqID.ToString());

                        MReq.TotalWithoutTaxes = 0;
                        foreach (VMRequisitionsForApproval r in MReq.ForApprovalLineItem)
                        {
                            MReq.TotalWithoutTaxes += r.RequisitionLineItem.LineTotal;
                        }
                        
                        lstResponse.Add(MReq);
                    }
                }
            }
            catch { }

            return lstResponse;
        }

    }

}