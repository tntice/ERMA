using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ERMA.Code;
using System.Data;
using TACData;
using TACUtility;
using System.Text;


namespace ERMA.Models
{
    public class ReqDetail
    {
        private decimal _requisitionNumber = 0;
        private bool _requisitionNumberSpecified = true;
        private decimal _masterRequisitionNumber = 0;
        private bool _masterRequisitionNbrSpecified = true;
        private string _vendorPartNumber = string.Empty;
        private string _TACPartNumber = string.Empty;
        private Int32 _qty = 0;
        private bool _qtyOrderedSpecified = true;
        private string _unitOfMeasure = string.Empty;
        private string _orderUnit = string.Empty;
        private decimal _perUnitPrice = 0;
        private DateTime _requiredDate;
        private string _projectCode = string.Empty;
        private decimal _glAccount = 0;
        private string _inventoryStockroom = string.Empty;
        private bool _isTaxable = true;
        private string _taxRate = "0";
        private Part _reqPart = new Part();
        private string _CMSDatabase = string.Empty;
        private string _servLang = "ENU";
        private string _servPlntCode = "DFT";
        private string _requestID = string.Empty;
        private string _requestMode = string.Empty;
        private string _deptCode = string.Empty;
        private string _vendorCode = string.Empty;
        private string _taxGroupCode = string.Empty;
        private string _buyer = string.Empty;
        private string _requisitioner = string.Empty;
        private string _productDescription = string.Empty;
        private List<Vendor> _vendorList = new List<Vendor>();
        private string _selectedVendorCode = string.Empty;
        private List<GLAccount> _GLAccountList = new List<GLAccount>();
        private Int32 _selectedGLAccount = -1;
        private string _vendorName = string.Empty;
        private string _selectedUoM1 = string.Empty;
        private string _selectedUoM2 = string.Empty;
        private List<UoM> _UoMList = new List<UoM>();
        private string _selectedTaxGroupCode = string.Empty;
        private List<TaxGroup> _taxGroupList = new List<TaxGroup>();
        private List<TaxRate> _taxRateList = new List<TaxRate>();
        private List<Project> _projectList = new List<Project>();
        private string _selectedVendorPart = string.Empty;
        private List<VendorPart> _vendorPartList = new List<VendorPart>();
        private string _status = string.Empty;
        private string _approvedStatus = string.Empty;
        private DateTime _submittedDate;
        private string _htmlSelectProject = string.Empty;
        private string _disabled = string.Empty;


        [Display(Name = "Requisition Number")]
        public decimal RequisitionNumber { get => _requisitionNumber; set => _requisitionNumber = value; }
        [Display(Name = "Vendor Part Number")]
        [Required(ErrorMessage = "Vendor Part Number is Required")]
        public string VendorPartNumber { get => _vendorPartNumber; set => _vendorPartNumber = value; }
        [Display(Name = "TAC Part Number")]
        public string TACPartNumber { get => _TACPartNumber; set => _TACPartNumber = value; }
        [Required(ErrorMessage = "Qty is Required")]
        [Range(1, 150000.00, ErrorMessage = "Quantity cannot exceed 150,000 per Requisition and must be > 0")]
        public int Qty { get => _qty; set => _qty = value; }
        [Display(Name = "Price UoM")]
        [Required(ErrorMessage = "Price UoM is Required")]
        public string UnitOfMeasure { get => _unitOfMeasure; set => _unitOfMeasure = value; }
        [Display(Name = "Per Unit Price")]
        [Required(ErrorMessage = "Per Unit Price is Required")]
        [Range(0.001, 10000000.00, ErrorMessage = "Unit Price must be > 0 and <= 10,000,000")]
        public decimal PerUnitPrice { get => _perUnitPrice; set => _perUnitPrice = value; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage ="Please provide the required date")]
        [Display(Name = "Required Date")]
        public DateTime RequiredDate { get => _requiredDate; set => _requiredDate = value; }
        [Display(Name = "Project Code")]
        public string ProjectCode { get => _projectCode; set => _projectCode = value; }
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        [Display(Name = "GL Account")]
        [Required(ErrorMessage ="THe GL Account is required")]
        public decimal GlAccount { get => _glAccount; set => _glAccount = value; }
        [Display(Name = "Inventory Stockroom")]
        public string InventoryStockroom { get => _inventoryStockroom; set => _inventoryStockroom = value; }
        [Display (Name ="Taxable?")]
        public bool IsTaxable { get => _isTaxable; set => _isTaxable = value; }
        [Required(ErrorMessage ="Tax Code is Required")]
        [Display(Name = "Tax Rate Code")]
        public string TaxRate { get => _taxRate; set => _taxRate = value; }
        [Display(Name = "Requisition Part")]
        public Part ReqPart { get => _reqPart; set => _reqPart = value; }
        [Display (Name = "Master Req Num")]
        public decimal MasterRequisitionNumber { get => _masterRequisitionNumber; set => _masterRequisitionNumber = value; }
        public string CMSDatabase { get => _CMSDatabase; set => _CMSDatabase = value; }
        public string ServLang { get => _servLang; set => _servLang = value; }
        public string ServPlntCode { get => _servPlntCode; set => _servPlntCode = value; }
        public bool MasterRequisitionNbrSpecified { get => _masterRequisitionNbrSpecified; set => _masterRequisitionNbrSpecified = value; }
        public bool RequisitionNumberSpecified { get => _requisitionNumberSpecified; set => _requisitionNumberSpecified = value; }
        public bool QtyOrderedSpecified { get => _qtyOrderedSpecified; set => _qtyOrderedSpecified = value; }
        public string RequestID { get => _requestID; set => _requestID = value; }
        public string RequestMode { get => _requestMode; set => _requestMode = value; }
        [Display(Name = "Department")]
        public string DeptCode { get => _deptCode; set => _deptCode = value; }
        [Required(ErrorMessage ="Vendor Code is Required")]
        [Display (Name = "Vendor Code")]
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        [Required(ErrorMessage ="Tax Group Code is Required")]
        [Display(Name = "Tax Group Code")]
        public string TaxGroupCode { get => _taxGroupCode; set => _taxGroupCode = value; }
        public string Buyer { get => _buyer; set => _buyer = value; }
        public string Requisitioner { get => _requisitioner; set => _requisitioner = value; }
        [Required (ErrorMessage ="Qty UoM is Required")]
        [Display(Name = "Qty UoM")]
        public string OrderUnit { get => _orderUnit; set => _orderUnit = value; }
        [Display(Name = "Product Description")]
        [Required(ErrorMessage ="Product Description is Required")]
        public string ProductDescription { get => _productDescription; set => _productDescription = value; }
        public List<Vendor> VendorList { get => _vendorList; set => _vendorList = value; }
        public string SelectedVendorCode { get => _selectedVendorCode; set => _selectedVendorCode = value; }
        public List<GLAccount> GLAccountList { get => _GLAccountList; set => _GLAccountList = value; }
        public int SelectedGLAccount { get => _selectedGLAccount; set => _selectedGLAccount = value; }
        [Display(Name = "Vendor Name")]
        public string VendorName { get => _vendorName; set => _vendorName = value; }
        public string SelectedUoM1 { get => _selectedUoM1; set => _selectedUoM1 = value; }
        public string SelectedUoM2 { get => _selectedUoM2; set => _selectedUoM2 = value; }
        public List<UoM> UoMList { get => _UoMList; set => _UoMList = value; }
        public string SelectedTaxGroupCode { get => _selectedTaxGroupCode; set => _selectedTaxGroupCode = value; }
        public List<TaxGroup> TaxGroupList { get => _taxGroupList; set => _taxGroupList = value; }
        public List<TaxRate> TaxRateList { get => _taxRateList; set => _taxRateList = value; }
        public List<Project> ProjectList { get => _projectList; set => _projectList = value; }
        public string SelectedVendorPart { get => _selectedVendorPart; set => _selectedVendorPart = value; }
        public List<VendorPart> VendorPartList { get => _vendorPartList; set => _vendorPartList = value; }
        public string Status { get => _status; set => _status = value; }
        public string ApprovedStatus { get => _approvedStatus; set => _approvedStatus = value; }
        public DateTime SubmittedDate { get => _submittedDate; set => _submittedDate = value; }
        
        public Decimal LineTotal
        {
            get { return Qty * PerUnitPrice; }
        }

        public string HtmlSelectProject { get => _htmlSelectProject; set => _htmlSelectProject = value; }
        public string Disabled { get => _disabled; set => _disabled = value; }

        public ReqDetail() { }
    }

    public class ReqDetailRepository
    {

        public ReqDetail GetRequisitionDetailByID(string ReqID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            ReqDetail rd = new ReqDetail();

            string strSQL = SQL.GetRequisitionByReqNum(ReqID, strSchema);

            TACDataRowType drt = new TACDataRowType();

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    rd.RequestID = drt.Drow.ItemArray[0].ToString();
                    rd.RequisitionNumber = Convert.ToDecimal(rd.RequestID);
                    rd.RequisitionNumberSpecified = true;
                    rd.SubmittedDate = Convert.ToDateTime(drt.Drow.ItemArray[1].ToString());
                    rd.Qty = Convert.ToInt32(Convert.ToDecimal(drt.Drow.ItemArray[2].ToString()));
                    rd.QtyOrderedSpecified = true;
                    rd.UnitOfMeasure = drt.Drow.ItemArray[3].ToString();
                    rd.TACPartNumber = drt.Drow.ItemArray[4].ToString();
                    rd.PerUnitPrice = Convert.ToDecimal(drt.Drow.ItemArray[5].ToString());
                    rd.OrderUnit = drt.Drow.ItemArray[6].ToString();
                    rd.RequiredDate = Convert.ToDateTime(drt.Drow.ItemArray[7].ToString());
                    rd.Requisitioner = drt.Drow.ItemArray[8].ToString();
                    rd.ApprovedStatus = drt.Drow.ItemArray[9].ToString();
                    rd.VendorCode = drt.Drow.ItemArray[10].ToString();
                    rd.VendorName = drt.Drow.ItemArray[11].ToString();
                    rd.VendorPartNumber = drt.Drow.ItemArray[12].ToString();
                    rd.GlAccount = Convert.ToDecimal(drt.Drow.ItemArray[13].ToString());
                    rd.ProjectCode = drt.Drow.ItemArray[16].ToString();
                    rd.DeptCode = drt.Drow.ItemArray[17].ToString();
                    rd.Status = drt.Drow.ItemArray[18].ToString();
                    rd.InventoryStockroom = drt.Drow.ItemArray[24].ToString();
                    rd.TaxGroupCode = drt.Drow.ItemArray[29].ToString();
                    rd.TaxRate = drt.Drow.ItemArray[30].ToString();
                    rd.Buyer = drt.Drow.ItemArray[31].ToString();
                    rd.ServPlntCode = drt.Drow.ItemArray[32].ToString();
                    rd.MasterRequisitionNumber = Convert.ToDecimal(drt.Drow.ItemArray[36].ToString());
                    rd.MasterRequisitionNbrSpecified = true;

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("<select id='ddProjCode" + rd.RequestID + "'>");
                    foreach(var line in rd.ProjectList)
                    {
                        if (line.Code.Equals(rd.ProjectCode))
                        {
                            sb.AppendLine("<option value='" + line.Code + "' selected>" + line.Description + "</option>");
                        }
                        else
                        {
                            sb.AppendLine("<option value='" + line.Code + "'>" + line.Description + "</option>");
                        }
                    }
                    sb.AppendLine("</select>");
                    rd.HtmlSelectProject = sb.ToString();
                }

            }
            catch { }

            return rd;
        }

        public List<ReqDetail> GetRequisitionsByMasterReqID(string MasterReqID)
        {
            List<ReqDetail> Reqs = new List<ReqDetail>();

            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            string strSQL = SQL.GetRequisitionByMasterReqID(MasterReqID, strSchema);

            TACDataTableType dtt = new TACDataTableType();

            try
            {
                dtt = Data.GetTACDataRows(strSQL, cCon);

                if (dtt.HasData)
                {
                    foreach (DataRow drt in dtt.Dtable.Rows)
                    {
                        ReqDetail rd = new ReqDetail();
                        rd.RequestID = drt.ItemArray[0].ToString();
                        rd.RequisitionNumber = Convert.ToDecimal(drt.ItemArray[0].ToString());
                        rd.RequisitionNumberSpecified = true;
                        rd.SubmittedDate = Convert.ToDateTime(drt.ItemArray[1].ToString());
                        rd.Qty = Convert.ToInt32(Convert.ToDecimal(drt.ItemArray[2].ToString()));
                        rd.QtyOrderedSpecified = true;
                        rd.UnitOfMeasure = drt.ItemArray[3].ToString();
                        rd.TACPartNumber = drt.ItemArray[4].ToString();
                        rd.PerUnitPrice = Convert.ToDecimal(drt.ItemArray[5].ToString());
                        rd.OrderUnit = drt.ItemArray[6].ToString();
                        rd.RequiredDate = Convert.ToDateTime(drt.ItemArray[7].ToString());
                        rd.Requisitioner = drt.ItemArray[8].ToString();
                        rd.ApprovedStatus = drt.ItemArray[9].ToString();
                        rd.VendorCode = drt.ItemArray[10].ToString();
                        rd.VendorName = drt.ItemArray[11].ToString();
                        rd.VendorPartNumber = drt.ItemArray[12].ToString();
                        rd.GlAccount = Convert.ToDecimal(drt.ItemArray[13].ToString());
                        rd.ProjectCode = drt.ItemArray[16].ToString();
                        rd.DeptCode = drt.ItemArray[17].ToString();
                        rd.Status = drt.ItemArray[18].ToString();
                        rd.InventoryStockroom = drt.ItemArray[24].ToString();
                        rd.TaxGroupCode = drt.ItemArray[29].ToString();
                        rd.TaxRate = drt.ItemArray[30].ToString();
                        rd.Buyer = drt.ItemArray[31].ToString();
                        rd.ServPlntCode = drt.ItemArray[32].ToString();
                        rd.MasterRequisitionNumber = Convert.ToDecimal(drt.ItemArray[36].ToString());
                        rd.MasterRequisitionNbrSpecified = true;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("<select id='ddProjCode" + rd.RequestID + "'>");
                        foreach (var line in rd.ProjectList)
                        {
                            if (line.Code.Equals(rd.ProjectCode))
                            {
                                sb.AppendLine("<option value='" + line.Code + "' selected>" + line.Description + "</option>");
                            }
                            else
                            {
                                sb.AppendLine("<option value='" + line.Code + "'>" + line.Description + "</option>");
                            }
                        }
                        sb.AppendLine("</select>");
                        rd.HtmlSelectProject = sb.ToString();

                        Reqs.Add(rd);
                    }
                }
            }
            catch { }
            return Reqs;

        }

        public Int32 UpdateRequisitionApproval(ReqDetail req, string ApprovedBy)
        {
            Int32 returnValue = 0;

            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            TACNonDataType ndt = new TACNonDataType();

            string strSQL = SQL.UpdateRequisitionApproval(req, ApprovedBy, strSchema);

            try
            {
                ndt = TACData.Data.nonTACQuery(strSQL, cCon);
                returnValue = ndt.CountAffected;
            }
            catch 
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public List<ReqDetail> GetRequisitionsByEmpNum(string EmpNum)
        {
            List<ReqDetail> Reqs = new List<ReqDetail>();

            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            string strSQL = SQL.GetRequisitionByEmpNum(EmpNum, strSchema);

            TACDataTableType dtt = new TACDataTableType();

            try
            {
                dtt = Data.GetTACDataRows(strSQL, cCon);

                if (dtt.HasData)
                {
                    foreach (DataRow drt in dtt.Dtable.Rows)
                    {
                        ReqDetail rd = new ReqDetail();
                        rd.RequestID = drt.ItemArray[0].ToString();
                        rd.SubmittedDate = Convert.ToDateTime(drt.ItemArray[1].ToString());
                        rd.Qty = Convert.ToInt32(drt.ItemArray[2].ToString());
                        rd.QtyOrderedSpecified = true;
                        rd.UnitOfMeasure = drt.ItemArray[3].ToString();
                        rd.TACPartNumber = drt.ItemArray[4].ToString();
                        rd.PerUnitPrice = Convert.ToDecimal(drt.ItemArray[5].ToString());
                        rd.OrderUnit = drt.ItemArray[6].ToString();
                        rd.RequiredDate = Convert.ToDateTime(drt.ItemArray[7].ToString());
                        rd.Requisitioner = drt.ItemArray[8].ToString();
                        rd.ApprovedStatus = drt.ItemArray[9].ToString();
                        rd.VendorCode = drt.ItemArray[10].ToString();
                        rd.VendorName = drt.ItemArray[11].ToString();
                        rd.VendorPartNumber = drt.ItemArray[12].ToString();
                        rd.GlAccount = Convert.ToDecimal(drt.ItemArray[13].ToString());
                        rd.ProjectCode = drt.ItemArray[16].ToString();
                        rd.DeptCode = drt.ItemArray[17].ToString();
                        rd.Status = drt.ItemArray[18].ToString();
                        rd.InventoryStockroom = drt.ItemArray[24].ToString();
                        rd.TaxGroupCode = drt.ItemArray[29].ToString();
                        rd.TaxRate = drt.ItemArray[30].ToString();
                        rd.Buyer = drt.ItemArray[31].ToString();
                        rd.ServPlntCode = drt.ItemArray[32].ToString();
                        rd.MasterRequisitionNumber = Convert.ToDecimal(drt.ItemArray[36].ToString());
                        rd.MasterRequisitionNbrSpecified = true;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("<select id='ddProjCode" + rd.RequestID + "'>");
                        foreach (var line in rd.ProjectList)
                        {
                            if (line.Code.Equals(rd.ProjectCode))
                            {
                                sb.AppendLine("<option value='" + line.Code + "' selected>" + line.Description + "</option>");
                            }
                            else
                            {
                                sb.AppendLine("<option value='" + line.Code + "'>" + line.Description + "</option>");
                            }
                        }
                        sb.AppendLine("</select>");
                        rd.HtmlSelectProject = sb.ToString();

                        Reqs.Add(rd);
                    }
                }
            }
            catch { }
            return Reqs;

        }

    }
}