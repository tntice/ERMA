using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Code;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;

namespace ERMA.Models
{
    public class Vendor
    {

        #region Variables

        private string _vendorCode = string.Empty;
        private string _selectedVendorCode = string.Empty;
        private string _vendorName = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;
        private string _address3 = string.Empty;
        private string _city = string.Empty;
        private string _state = string.Empty;
        private string _zip = string.Empty;
        private string _businessPhone = string.Empty;
        private string _faxPhone = string.Empty;
        private string _email = string.Empty;
        private string _webSite = string.Empty;
        private string _paymentTerms = string.Empty;
        private bool _useCreditCard = false;
        private string _taxGroupCode = "VEN";
        private string _goodsTaxRateCode = string.Empty;
        private string _servicesTaxRateCode = string.Empty;
        private string _vendorStatus = "A";

        private string _inactiveReason = string.Empty;
        private string _country = string.Empty;
        private string _contact = string.Empty;
        private string _dunsNum = string.Empty;
        private string _federalTaxID = string.Empty;
        private string _freightCharges = string.Empty;
        private string _remitTo = string.Empty;
        private string _apBank = string.Empty;
        private decimal _ytdPurchases = 0;
        private string _fobCode = string.Empty;
        private string _osStockroom = string.Empty;
        private string _vendorClass = string.Empty;
        private string _currency = "US";
        private string _minorityFlag = string.Empty;
        private string _autoVoucher = "N";
        private string _chequeVoucher = "N";
        private string _T1099 = "N";

        //Page 2
        private string _createPlanningSchedule = "N";
        private string _planningScheduleFrequency = "W";
        private string _planningScheduleDay = "1";
        private string _planningScheduleDate = "S";
        private string _sendViaEDI = "N";
        private string _createShippingSchedule = "N";
        private string _shippingScheduleFrequency = "W";
        private string _shippingScheduleDay = "1";
        private string _shippingScheduleDate = "S";
        private string _generateScoreCard = "2";
        private string _scanSupplierSerialNum = "N";
        private string _poSendMode = "1";
        private decimal _shippingLeadTime = 0;
        private string _shippingLeadTimeUnit = "1";
        private string _roundShippingHoursToDays = "3";
        private string _skipWorkDaysforEDIExpDate = "3";
        private string _requireEDIRemittanceAdvice = "2";
        private string _EDIPartialPaymentAllowed = "2";
        private string _apVoucherControl = "1";

        //Page 3
        private string _processSerializedInASN = "1";
        private string _timeZone = string.Empty;
        private string _federalID = string.Empty;
        private string _vendorType = "1";
        private string _dumpingClass = string.Empty;
        private string _defaultCarrier = string.Empty;
        private string _defaultCustomBkr = string.Empty;
        private string _userVerTempl = string.Empty;
        private string _qcInspector = string.Empty;
        private string _initialDisplayScheduleRelease = "2";
        private string _repairLocation = string.Empty;
        private string _commodityCategory = string.Empty;
        private string _ASNMapping = "1";

        //Page 4
        private string _autoPrintApprovedPO = "2";
        private string _TwoDScanFormat = string.Empty;
        private string _defaultTo2DScan = "2";
        private string _override2DScanLotNum = "2";
        private string _defaultSupplierCode = string.Empty;
        private string _printLanguage = string.Empty;
        private string _voucherWithOutReceiptRequiresNotepad = "2";
        private string _enterNotesforRecurringVouchers = "2";
        private decimal _minimumOrderAmount = 0;
        private decimal _freeFreightThreshold = 0;
        private string _displayMRPNotification = "1";
        private string _importVendor = "2";
        private string _EZASNUpdatePOShpmntInTransit = "3";

        //Page 5
        private string _EDISpecHandlingInstrTable = string.Empty;
        private string _defaultEDISpecialHandlingCode = string.Empty;
        private string _commodityAuthorization = "2";
        private string _vendorReturnASNVerificationTemplate = string.Empty;
        private string _outsideServiceASNVerificationTemplate = string.Empty;
        private string _repairASNVerificationTemplate = string.Empty;
        private string _sourcePlantCode = string.Empty;
        private string _supplyingPlantCode = string.Empty;
        private string _OSScrapTransReason = string.Empty;
        private string _incoming810QuantityToleranceRangeCheck = "2";
        private decimal _rangeLower = 0;
        private decimal _rangeUpper = 0;
        private string _tier2SCDVendor = "2";

        //Page 6
        private string _discountDueDateFromTermCode = "1";
        private string _useForeignFFL = "2";
        private string _autoImportPostIncomingNonSCDASN = "2";

        //EFT
        private string _arContact = string.Empty;
        private string _arPhoneNumber = string.Empty;
        private string _arFaxNumber = string.Empty;
        private string _EFTBankCode = string.Empty;

        #endregion

        #region Properties

        public string VendorDisplay
        {
            get => _vendorCode + " - " + _vendorName;
        }
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        
        public string VendorName { get => _vendorName; set => _vendorName = value; }
        public string Address1 { get => _address1; set => _address1 = value; }
        public string Address2 { get => _address2; set => _address2 = value; }
        public string Address3 { get => _address3; set => _address3 = value; }
        public string City { get => _city; set => _city = value; }
        public string State { get => _state; set => _state = value; }
        public string Zip { get => _zip; set => _zip = value; }
        public string BusinessPhone { get => _businessPhone; set => _businessPhone = value; }
        public string FaxPhone { get => _faxPhone; set => _faxPhone = value; }
        public string Email { get => _email; set => _email = value; }
        public string PaymentTerms { get => _paymentTerms; set => _paymentTerms = value; }
        public bool UseCreditCard { get => _useCreditCard; set => _useCreditCard = value; }
        public string WebSite { get => _webSite; set => _webSite = value; }
        public string TaxGroupCode { get => _taxGroupCode; set => _taxGroupCode = value; }
        public string GoodsTaxRateCode { get => _goodsTaxRateCode; set => _goodsTaxRateCode = value; }
        public string ServicesTaxRateCode { get => _servicesTaxRateCode; set => _servicesTaxRateCode = value; }
        public string InactiveReason { get => _inactiveReason; set => _inactiveReason = value; }
        public string Country { get => _country; set => _country = value; }
        public string Contact { get => _contact; set => _contact = value; }
        public string DunsNum { get => _dunsNum; set => _dunsNum = value; }
        public string FederalTaxID { get => _federalTaxID; set => _federalTaxID = value; }
        public string FreightCharges { get => _freightCharges; set => _freightCharges = value; }
        public string RemitTo { get => _remitTo; set => _remitTo = value; }
        public string ApBank { get => _apBank; set => _apBank = value; }
        public decimal YtdPurchases { get => _ytdPurchases; set => _ytdPurchases = value; }
        public string FobCode { get => _fobCode; set => _fobCode = value; }
        public string OsStockroom { get => _osStockroom; set => _osStockroom = value; }
        public string VendorClass { get => _vendorClass; set => _vendorClass = value; }
        public string Currency { get => _currency; set => _currency = value; }
        public string MinorityFlag { get => _minorityFlag; set => _minorityFlag = value; }
        public string AutoVoucher { get => _autoVoucher; set => _autoVoucher = value; }
        public string ChequeVoucher { get => _chequeVoucher; set => _chequeVoucher = value; }
        public string T1099 { get => _T1099; set => _T1099 = value; }
        public string VendorStatus { get => _vendorStatus; set => _vendorStatus = value; }
        public string CreatePlanningSchedule { get => _createPlanningSchedule; set => _createPlanningSchedule = value; }
        public string PlanningScheduleFrequency { get => _planningScheduleFrequency; set => _planningScheduleFrequency = value; }
        public string PlanningScheduleDay { get => _planningScheduleDay; set => _planningScheduleDay = value; }
        public string PlanningScheduleDate { get => _planningScheduleDate; set => _planningScheduleDate = value; }
        public string SendViaEDI { get => _sendViaEDI; set => _sendViaEDI = value; }
        public string CreateShippingSchedule { get => _createShippingSchedule; set => _createShippingSchedule = value; }
        public string ShippingScheduleFrequency { get => _shippingScheduleFrequency; set => _shippingScheduleFrequency = value; }
        public string ShippingScheduleDay { get => _shippingScheduleDay; set => _shippingScheduleDay = value; }
        public string ShippingScheduleDate { get => _shippingScheduleDate; set => _shippingScheduleDate = value; }
        public string GenerateScoreCard { get => _generateScoreCard; set => _generateScoreCard = value; }
        public string ScanSupplierSerialNum { get => _scanSupplierSerialNum; set => _scanSupplierSerialNum = value; }
        public string PoSendMode { get => _poSendMode; set => _poSendMode = value; }
        public decimal ShippingLeadTime { get => _shippingLeadTime; set => _shippingLeadTime = value; }
        public string ShippingLeadTimeUnit { get => _shippingLeadTimeUnit; set => _shippingLeadTimeUnit = value; }
        public string RoundShippingHoursToDays { get => _roundShippingHoursToDays; set => _roundShippingHoursToDays = value; }
        public string SkipWorkDaysforEDIExpDate { get => _skipWorkDaysforEDIExpDate; set => _skipWorkDaysforEDIExpDate = value; }
        public string RequireEDIRemittanceAdvice { get => _requireEDIRemittanceAdvice; set => _requireEDIRemittanceAdvice = value; }
        public string EDIPartialPaymentAllowed { get => _EDIPartialPaymentAllowed; set => _EDIPartialPaymentAllowed = value; }
        public string ApVoucherControl { get => _apVoucherControl; set => _apVoucherControl = value; }
        public string ProcessSerializedInASN { get => _processSerializedInASN; set => _processSerializedInASN = value; }
        public string TimeZone { get => _timeZone; set => _timeZone = value; }
        public string FederalID { get => _federalID; set => _federalID = value; }
        public string VendorType { get => _vendorType; set => _vendorType = value; }
        public string DumpingClass { get => _dumpingClass; set => _dumpingClass = value; }
        public string DefaultCarrier { get => _defaultCarrier; set => _defaultCarrier = value; }
        public string DefaultCustomBkr { get => _defaultCustomBkr; set => _defaultCustomBkr = value; }
        public string UserVerTempl { get => _userVerTempl; set => _userVerTempl = value; }
        public string QcInspector { get => _qcInspector; set => _qcInspector = value; }
        public string InitialDisplayScheduleRelease { get => _initialDisplayScheduleRelease; set => _initialDisplayScheduleRelease = value; }
        public string RepairLocation { get => _repairLocation; set => _repairLocation = value; }
        public string CommodityCategory { get => _commodityCategory; set => _commodityCategory = value; }
        public string ASNMapping { get => _ASNMapping; set => _ASNMapping = value; }
        public string AutoPrintApprovedPO { get => _autoPrintApprovedPO; set => _autoPrintApprovedPO = value; }
        public string TwoDScanFormat { get => _TwoDScanFormat; set => _TwoDScanFormat = value; }
        public string DefaultTo2DScan { get => _defaultTo2DScan; set => _defaultTo2DScan = value; }
        public string DefaultSupplierCode { get => _defaultSupplierCode; set => _defaultSupplierCode = value; }
        public string PrintLanguage { get => _printLanguage; set => _printLanguage = value; }
        public string VoucherWithOutReceiptRequiresNotepad { get => _voucherWithOutReceiptRequiresNotepad; set => _voucherWithOutReceiptRequiresNotepad = value; }
        public string EnterNotesforRecurringVouchers { get => _enterNotesforRecurringVouchers; set => _enterNotesforRecurringVouchers = value; }
        public decimal MinimumOrderAmount { get => _minimumOrderAmount; set => _minimumOrderAmount = value; }
        public decimal FreeFreightThreshold { get => _freeFreightThreshold; set => _freeFreightThreshold = value; }
        public string DisplayMRPNotification { get => _displayMRPNotification; set => _displayMRPNotification = value; }
        public string ImportVendor { get => _importVendor; set => _importVendor = value; }
        public string EZASNUpdatePOShpmntInTransit { get => _EZASNUpdatePOShpmntInTransit; set => _EZASNUpdatePOShpmntInTransit = value; }
        public string Override2DScanLotNum { get => _override2DScanLotNum; set => _override2DScanLotNum = value; }
        public string EDISpecHandlingInstrTable { get => _EDISpecHandlingInstrTable; set => _EDISpecHandlingInstrTable = value; }
        public string DefaultEDISpecialHandlingCode { get => _defaultEDISpecialHandlingCode; set => _defaultEDISpecialHandlingCode = value; }
        public string CommodityAuthorization { get => _commodityAuthorization; set => _commodityAuthorization = value; }
        public string VendorReturnASNVerificationTemplate { get => _vendorReturnASNVerificationTemplate; set => _vendorReturnASNVerificationTemplate = value; }
        public string OutsideServiceASNVerificationTemplate { get => _outsideServiceASNVerificationTemplate; set => _outsideServiceASNVerificationTemplate = value; }
        public string RepairASNVerificationTemplate { get => _repairASNVerificationTemplate; set => _repairASNVerificationTemplate = value; }
        public string SourcePlantCode { get => _sourcePlantCode; set => _sourcePlantCode = value; }
        public string SupplyingPlantCode { get => _supplyingPlantCode; set => _supplyingPlantCode = value; }
        public string OSScrapTransReason { get => _OSScrapTransReason; set => _OSScrapTransReason = value; }
        public string Incoming810QuantityToleranceRangeCheck { get => _incoming810QuantityToleranceRangeCheck; set => _incoming810QuantityToleranceRangeCheck = value; }
        public decimal RangeLower { get => _rangeLower; set => _rangeLower = value; }
        public decimal RangeUpper { get => _rangeUpper; set => _rangeUpper = value; }
        public string Tier2SCDVendor { get => _tier2SCDVendor; set => _tier2SCDVendor = value; }
        public string DiscountDueDateFromTermCode { get => _discountDueDateFromTermCode; set => _discountDueDateFromTermCode = value; }
        public string UseForeignFFL { get => _useForeignFFL; set => _useForeignFFL = value; }
        public string AutoImportPostIncomingNonSCDASN { get => _autoImportPostIncomingNonSCDASN; set => _autoImportPostIncomingNonSCDASN = value; }
        public string ArContact { get => _arContact; set => _arContact = value; }
        public string ArPhoneNumber { get => _arPhoneNumber; set => _arPhoneNumber = value; }
        public string ArFaxNumber { get => _arFaxNumber; set => _arFaxNumber = value; }
        public string EFTBankCode { get => _EFTBankCode; set => _EFTBankCode = value; }
        public string SelectedVendorCode { get => _selectedVendorCode; set => _selectedVendorCode = value; }

        #endregion


        public Vendor() { }
    }

    public class VendorRepository
    {

        public Vendor GetVendorByID(string VendorCode)
        {
            Vendor vn = new Vendor();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            string strSQL = SQL.GetVendor(VendorCode, strSchema);
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    vn.VendorCode = VendorCode;
                    vn.VendorName = dr.ItemArray[0].ToString().Trim();
                    vn.VendorStatus = dr.ItemArray[1].ToString().Trim();
                    vn.TaxGroupCode = dr.ItemArray[3].ToString().Trim();
                    vn.RemitTo = dr.ItemArray[4].ToString().Trim();
                    vn.Address1 = dr.ItemArray[5].ToString().Trim();
                    vn.Address2 = dr.ItemArray[6].ToString().Trim();
                    vn.Address3 = dr.ItemArray[7].ToString().Trim();
                    vn.Zip = dr.ItemArray[8].ToString().Trim();
                }
            }
            catch
            {
            }
            return vn;
        }

        public List<Vendor> GetVendorList(string filter)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();
            List<Vendor> vendors = new List<Vendor>();
            
            string sSQL = SQL.GetVendorList(filter.ToUpper(), strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if(dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Vendor vn = new Vendor();
                        vn.VendorName = dr.ItemArray[0].ToString().Trim();
                        vn.VendorStatus = dr.ItemArray[1].ToString().Trim();
                        vn.VendorCode = dr.ItemArray[2].ToString().Trim();
                        vn.TaxGroupCode = dr.ItemArray[3].ToString().Trim();
                        vn.RemitTo = dr.ItemArray[4].ToString().Trim();

                        vendors.Add(vn);
                    }
                }
            }
            catch
            {

            }
            return vendors;

        }

    }
}