using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ERMA.Code;
using System.Data;
using TACData;
using TACUtility;


namespace ERMA.Models
{
    public class MasterRequisition
    {
        private decimal _masterReqID = 0;
        [StringLength(50)]
        private string _description1 = string.Empty;
        private string _description2 = string.Empty;
        private string _description3 = string.Empty;
        private string _CMSDatabase = string.Empty;
        private string _servLang = "ENU";
        private string _servPlntCode = "DFT";
        private string _requestID = "";
        private string _requestMode = "";
        private string _requisitioner = string.Empty;
        private string _requisitionerUserName = string.Empty;
        private string _buyer = string.Empty;
        private bool _masterReqNbrSpecified = true;
        private string _notes = string.Empty;
        private string _purpose = string.Empty;
        private string _quoteNumber = string.Empty;
        private string _projectCodeSummary = string.Empty;
        private bool _isReimbursable = false;
        private string _vendorCode = string.Empty;
        private string _deptCode = string.Empty;
        private string _RequiredDate = string.Empty;
        private string _reason = string.Empty;
        private string _howShipped = string.Empty;
        private string _commodityCode = string.Empty;
        private string _glAccount = string.Empty;
        private string _selectedVendorCode = string.Empty;
        private List<Vendor> _vendorList = new List<Vendor>();


        public decimal MasterReqID { get => _masterReqID; set => _masterReqID = value; }
        [StringLength(50)]
        [Required(ErrorMessage = "Please provide a description")]
        [Display(Name = "Description 1")]
        public string Description1 { get => _description1; set => _description1 = value; }
        [StringLength(50)]
        [Display(Name = "Description 2")]
        public string Description2 { get => _description2; set => _description2 = value; }
        [StringLength(50)]
        [Display(Name = "Description 3")]
        public string Description3 { get => _description3; set => _description3 = value; }
        [Required(ErrorMessage = "CMSDatabase is a required field")]
        public string CMSDatabase { get => _CMSDatabase; set => _CMSDatabase = value; }
        [Required(ErrorMessage = "Service Language is a required field")]
        [Display(Name = "Service language")]
        public string ServLang { get => _servLang; set => _servLang = value; }
        [Required(ErrorMessage = "Service Plant Code is a required field")]
        [Display(Name = "Service Plant Code")]
        public string ServPlntCode { get => _servPlntCode; set => _servPlntCode = value; }
        [Required(ErrorMessage = "Request ID is a required field")]
        [Display(Name = "Request ID")]
        public string RequestID { get => _requestID; set => _requestID = value; }
        [Required(ErrorMessage = "Request Mode is a required field")]
        [Display(Name = "Request Mode")]
        public string RequestMode { get => _requestMode; set => _requestMode = value; }
        [Required(ErrorMessage = "Requisitioner is a required field")]
        public string Requisitioner { get => _requisitioner; set => _requisitioner = value; }
        [Required(ErrorMessage = "Buyer is a required field")]
        public string Buyer { get => _buyer; set => _buyer = value; }
        [Display(Name = "Master Requisition Number Specified")]
        public bool MasterReqNbrSpecified { get => _masterReqNbrSpecified; set => _masterReqNbrSpecified = value; }
        public string Notes { get => _notes; set => _notes = value; }
        public string Purpose { get => _purpose; set => _purpose = value; }
        [Display(Name = "Quote Number")]
        public string QuoteNumber { get => _quoteNumber; set => _quoteNumber = value; }
        [Display(Name = "Project Code")]
        public string ProjectCodeSummary { get => _projectCodeSummary; set => _projectCodeSummary = value; }
        [Display(Name = "Reimbursable?")]
        public bool IsReimbursable { get => _isReimbursable; set => _isReimbursable = value; }
        [Display(Name = "Vendor Code")]
        [Required(ErrorMessage = "Vendor Code is Required")]
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        [Display(Name = "Department")]
        public string DeptCode { get => _deptCode; set => _deptCode = value; }
        [Display(Name = "Required Date")]
        public string RequiredDate { get => _RequiredDate; set => _RequiredDate = value; }
        [Required(ErrorMessage = "Reason is required")]
        public string Reason { get => _reason; set => _reason = value; }
        [Display(Name = "How Shipped?")]
        public string HowShipped { get => _howShipped; set => _howShipped = value; }
        [Display(Name = "Commodity Code")]
        public string CommodityCode { get => _commodityCode; set => _commodityCode = value; }
        [Display(Name = "GL Account")]
        public string GlAccount { get => _glAccount; set => _glAccount = value; }
        public string SelectedVendorCode { get => _selectedVendorCode; set => _selectedVendorCode = value; }
        public List<Vendor> VendorList { get => _vendorList; set => _vendorList = value; }
        public string RequisitionerUserName { get => _requisitionerUserName; set => _requisitionerUserName = value; }

        public MasterRequisition() { }
    }

    public class MasterRequisitionRepository
    {
        public List<MasterRequisition> GetMasterRequisitionByEmpNum(string EmpNum)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            List<MasterRequisition> mReqs = new List<MasterRequisition>();

            string strSQL = SQL.GetMasterRequisitionByEmpNum(EmpNum, strSchema);
            TACDataTableType tdt = new TACDataTableType();

            try
            {
                tdt = Data.GetTACDataRows(strSQL, cCon);

                if (tdt.HasData)
                {
                    foreach (DataRow dr in tdt.Dtable.Rows)
                    {
                        MasterRequisition mr = new MasterRequisition();

                        mr.MasterReqID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        mr.Description1 = dr.ItemArray[1].ToString();
                        mr.Description2 = dr.ItemArray[2].ToString();
                        mr.Description3 = dr.ItemArray[3].ToString();
                        mr.Requisitioner = dr.ItemArray[4].ToString().ToUpper().Trim();
                        mr.Buyer = dr.ItemArray[5].ToString().ToUpper().Trim();
                        mr.ServPlntCode = dr.ItemArray[6].ToString();
                        mr.ProjectCodeSummary = dr.ItemArray[14].ToString();
                        mr.RequisitionerUserName = dr.ItemArray[16].ToString().ToUpper();
                        mReqs.Add(mr);
                    }
                }

            }
            catch { }

            return mReqs;
        }

        public MasterRequisition GetMasterRequisitionByID(string ID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            MasterRequisition mReq = new MasterRequisition();

            string strSQL = SQL.GetMasterRequisitionByID(ID, strSchema);
            TACDataRowType drt = new TACDataRowType();

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    mReq.MasterReqID = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                    mReq.Description1 = drt.Drow.ItemArray[1].ToString();
                    mReq.Description2 = drt.Drow.ItemArray[2].ToString();
                    mReq.Description3 = drt.Drow.ItemArray[3].ToString();
                    mReq.Requisitioner = drt.Drow.ItemArray[4].ToString().ToUpper().Trim();
                    mReq.Buyer = drt.Drow.ItemArray[5].ToString().ToUpper().Trim();
                    mReq.ServPlntCode = drt.Drow.ItemArray[6].ToString();
                    mReq.ProjectCodeSummary = drt.Drow.ItemArray[14].ToString();
                    mReq.RequisitionerUserName = drt.Drow.ItemArray[16].ToString().ToUpper();

                }

            }
            catch { }

            return mReq;
        }
    }
}