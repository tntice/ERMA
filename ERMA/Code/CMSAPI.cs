using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ERMA.Code;
using System.Security.Cryptography.X509Certificates;
using ERMA.IVRequisition;
using ERMA.IVMain;
using ERMA.IVProduction;
using System.Net.Security;
using System.ServiceModel;
using System.Xml;
using System.IO;
using System.Net;
using ERMA.Models;
using TACData;
using TACUtility;
using SecurityTools;


namespace ERMA.Code
{
    public class CMSAPI
    {
        private Int32 requestID = 1;

        public CMSAPI()
        {
            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;
        }
        

        public void AddVendor(Vendor ven, string database)
        {
            ERMA.IVMain.ivpwsMAIN001PortTypeClient client = new ivpwsMAIN001PortTypeClient("ivpwsMAIN001HttpSoap12Endpoint");

            // set so that service manager uses custom credentials class instead of default. 
            client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
            client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

            // set the credentials and path to the certificate. 
            client.ClientCredentials.UserName.UserName = ERMA.Properties.Settings.Default.WebUser.ToString();
            client.ClientCredentials.UserName.Password = ERMA.Properties.Settings.Default.WebPassword.ToString().ToLower();
            client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4d 00 eb c7");

            IVMain.AddVendorRequest request = new IVMain.AddVendorRequest();
            IVMain.Service_AddVendorType addVen = new IVMain.Service_AddVendorType();
            request.Service_AddVendor = addVen;

            addVen.RequestID = "MAINID".ToUniqueID();
            addVen.RequestMode = "MAINMODE".ToUniqueID();

            addVen.CMSDataBase = database;
            addVen.ServPlntCod = "DFT";
            addVen.ServLang = "ENU";

            //Page 1
            addVen.VendorCode = ven.VendorCode;
            addVen.VendorName = ven.VendorName;
            addVen.AddressLine01 = ven.Address1;
            addVen.AddressLine02 = ven.City + ", " + ven.State;
            addVen.City = ven.City;
            addVen.PostalCode = ven.Zip;
            addVen.ProvinceCode = ven.State;

            addVen.WebPageAddress = ven.WebSite;
            addVen.TelephoneNumber = ven.BusinessPhone;
            addVen.FAXNumber = ven.FaxPhone;
            addVen.TermsCode = ven.PaymentTerms;
            addVen.TaxGroupCode = ven.TaxGroupCode;
            addVen.GoodsTaxRateCode = ven.GoodsTaxRateCode;
            addVen.ServicesTaxRateCode = ven.ServicesTaxRateCode;
            addVen.VendorStatus = ven.VendorStatus;
            addVen.InactiveReason = ven.InactiveReason;
            addVen.CountryCode = ven.Country;
            addVen.ContactName = ven.Contact;
            addVen.DunsNumber = ven.DunsNum;
            // Federal Tax ID  = ven.FederalTaxID;
            // Freight Charges
            
            addVen.RemitToVendorCode = ven.RemitTo;
            addVen.APBankCode = ven.ApBank;
            addVen.YTDPurchasesAmount = ven.YtdPurchases;
            addVen.FOBCode = ven.FobCode;
            addVen.OutsideServiceStockroomCode = ven.OsStockroom;
            addVen.VendorClass = ven.VendorClass;
            addVen.CurrencyCode = ven.Currency;
            addVen.MinorityIndicatorFlag = ven.MinorityFlag;
            addVen.AutoVoucherFlag = ven.AutoVoucher;
            addVen.ChequeOrVoucherFlag = ven.ChequeVoucher;
            addVen.Applicable1099 = ven.T1099;

            //Page 2

            addVen.CreatePlanningSchedule = ven.CreatePlanningSchedule;
            addVen.PlanningScheduleFrequency = ven.PlanningScheduleFrequency;
            addVen.PlanningScheduleDay = ven.PlanningScheduleDay;
            addVen.PlanningScheduleDate = ven.PlanningScheduleDate;
            addVen.CreateShippingSchedule = ven.CreateShippingSchedule;
            addVen.ShippingScheduleFrequency = ven.ShippingScheduleFrequency;
            addVen.ShippingScheduleDay = ven.ShippingScheduleDay;
            addVen.ShippingScheduleDate = ven.ShippingScheduleDate;
            addVen.GenerateScoreCard = ven.GenerateScoreCard;
            addVen.ScanSupplierSerialNbrs = ven.ScanSupplierSerialNum;
            addVen.POSendMode = ven.PoSendMode;
            addVen.ShippingLeadTime = ven.ShippingLeadTime;
            addVen.ShippingLeadTimeUnit = ven.ShippingLeadTimeUnit;
            addVen.RoundShippingHoursToDays = ven.RoundShippingHoursToDays;
            // Skip W. Days for EDI Exp Date
            addVen.RequireEDIRemittanceAdvice = ven.RequireEDIRemittanceAdvice;
            addVen.PartialPaymentAllowedForEDI = ven.EDIPartialPaymentAllowed;
            addVen.APControl = ven.ApVoucherControl;

            //Page 3
            addVen.ProcessSerializedDtls = ven.ProcessSerializedInASN;
            addVen.TimeZoneCode = ven.TimeZone;
            addVen.FederalID = ven.FederalID;
            addVen.VendorType = ven.VendorType;
            addVen.VendorDumpingClass = ven.DumpingClass;
            addVen.CarrierCode = ven.DefaultCarrier;
            addVen.CustomBrokerCode = ven.DefaultCustomBkr;
            addVen.USRDEFVerificationTemplate = ven.UserVerTempl;
            addVen.QCInspectorCode = ven.QcInspector;
            addVen.InitialDisplayScheduleRelease = ven.InitialDisplayScheduleRelease;
            addVen.RepairLocation = ven.RepairLocation;
            addVen.POCommodityCategoryCode = ven.CommodityCategory;
            addVen.ASNMapping = ven.ASNMapping;

            //Page 4
            addVen.AutoPrintPOWhenApproved = ven.AutoPrintApprovedPO;
            addVen.ScanFormat2D = ven.TwoDScanFormat;
            addVen.DefaultTo2DScan = ven.DefaultTo2DScan;
            addVen.Override2DScanLotNumber = ven.Override2DScanLotNum;
            addVen.DefaultSupplierCode = ven.DefaultSupplierCode;
            addVen.LanguageCode = ven.PrintLanguage;
            addVen.VoucherWithoutReceiptRequireNotepad = ven.VoucherWithOutReceiptRequiresNotepad;
            addVen.NotesForRecurringVouchers = ven.EnterNotesforRecurringVouchers;
            addVen.MinimumOrderAmount = ven.MinimumOrderAmount;
            addVen.FreeFreightThreshold = ven.FreeFreightThreshold;
            addVen.DisplayPrintSendMRPNotification = ven.DisplayMRPNotification;
            addVen.ImportVendorFlag = ven.ImportVendor;
            addVen.EasyBusinessAsnUpdatePoShipmentsInTransit = ven.EZASNUpdatePOShpmntInTransit;

            //Page 5
            addVen.DefaultEdiSpecialHandlingCode = ven.DefaultEDISpecialHandlingCode;
            addVen.CommodityAuthorization = ven.CommodityAuthorization;
            addVen.VendorReturnPrimaryAsnTemplateCode = ven.VendorReturnASNVerificationTemplate;
            addVen.OutsideServicePrimaryAsnTemplateCode = ven.OutsideServiceASNVerificationTemplate;
            addVen.RepairPrimaryAsnTemplateCode = ven.RepairASNVerificationTemplate;
            addVen.SourcePlantCode = ven.SourcePlantCode;
            addVen.SupplyingPlantCode = ven.SupplyingPlantCode;
            addVen.OSScrapTransactionReasonCode = ven.OSScrapTransReason;
            addVen.Incoming810QtyToleranceCheck = ven.Incoming810QuantityToleranceRangeCheck;
            addVen.Incoming810QtyToleranceLowerRangePerc = ven.RangeLower.ToString();
            addVen.Incoming810QtyToleranceUpperRangePerc = ven.RangeUpper.ToString();
            addVen.Tier2ScdVendor = ven.Tier2SCDVendor;

            //Page 6
            addVen.DusDiscDateTermCalcMethod = ven.DiscountDueDateFromTermCode;
            addVen.UseForeignFFL = ven.UseForeignFFL;
            addVen.AutoImportPostIncomingNonSCDASN = ven.AutoImportPostIncomingNonSCDASN;

            //EFT
            addVen.ARContact = ven.ArContact;
            addVen.ARPhoneNumber = ven.ArPhoneNumber;
            addVen.ARFaxNumber = ven.ArFaxNumber;
            addVen.EFTBankCode = ven.EFTBankCode;
            //Deposit Transit
            

            try
            {
                IVMain.AddVendorResponse response = ((ivpwsMAIN001PortType)client).AddVendor(request);
                //CMS_ServiceResponse_AddVendorType;
                IVMain.CMS_ServiceResponseType GlobalResponse = response.CMS_ServiceResponse;
                //Console.WriteLine(GlobalResponse.Messages);
                
            }
            catch { }

        }

        public void AddWorkOrder(string sPart, DateTime sDate, string sTime, string sQty)
        {
            ERMA.IVProduction.ivpwsPD001PortTypeClient client = new ivpwsPD001PortTypeClient("ivpwsPD001HttpSoap12Endpoint");
            client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
            client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

            // set the credentials and path to the certificate. 
            client.ClientCredentials.UserName.UserName = ERMA.Properties.Settings.Default.WebUser.ToString();
            client.ClientCredentials.UserName.Password = ERMA.Properties.Settings.Default.WebPassword.ToString().ToLower();
            client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4d 00 eb c7");

            AddFirmPlannedOrderRequest request = new AddFirmPlannedOrderRequest();
            Service_AddFirmPlannedOrderType iFirmedPlannedOrder = new Service_AddFirmPlannedOrderType();

            request.Service_AddFirmPlannedOrder = iFirmedPlannedOrder;
            request.Service_AddFirmPlannedOrder.RequestID = "ID".ToUniqueID();
            request.Service_AddFirmPlannedOrder.RequestMode = "MODE".ToUniqueID();
            request.Service_AddFirmPlannedOrder.CMSDataBase = ERMA.Properties.Settings.Default.DB2Database.ToString();
            request.Service_AddFirmPlannedOrder.ServPlntCod = "DFT";
            request.Service_AddFirmPlannedOrder.AutoPost = "1";
            request.Service_AddFirmPlannedOrder.Part = sPart.Trim();
            request.Service_AddFirmPlannedOrder.RequiredDate = sDate;
            request.Service_AddFirmPlannedOrder.RequiredTime = sTime;
            request.Service_AddFirmPlannedOrder.RequiredQuantity = Convert.ToDecimal(sQty);
            request.Service_AddFirmPlannedOrder.RequiredQuantitySpecified = true;

            try
            {
                requestID += 1;
                AddFirmPlannedOrderResponse response = ((ivpwsPD001PortType)client).AddFirmPlannedOrder(request);
                CMS_ServiceResponse_AddFirmPlannedOrderType addGlobalFirmPlannedOrderResponse = response.CMS_ServiceResponse_AddFirmPlannedOrder;

            }
            catch(Exception ex) {
                string msg = ex.Message;
            }
        }

        public string AddMasterRequisition(MasterRequisition mReq, string UserName, string Password)
        {
            string returnValue = string.Empty;  

            ERMA.IVRequisition.ivpwsNPO001PortTypeClient client = new ivpwsNPO001PortTypeClient("ivpwsNPO001HttpSoap12Endpoint");
            client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
            client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

            // set the credentials and path to the certificate. 
            client.ClientCredentials.UserName.UserName = UserName;
            client.ClientCredentials.UserName.Password = Password;
            client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4d 00 eb c7");

            AddPORequsitionMasterRequest request = new AddPORequsitionMasterRequest();
            Service_AddPORequsitionMasterType addMReq = new Service_AddPORequsitionMasterType();

            addMReq.CMSDataBase = mReq.CMSDatabase;
            addMReq.ServPlntCod = mReq.ServPlntCode;
            addMReq.ServLang = mReq.ServLang;
            addMReq.RequestID = mReq.RequestID;
            addMReq.RequestMode = mReq.RequestMode;
            addMReq.Requisitioner = mReq.Requisitioner;
            addMReq.Buyer = mReq.Buyer;
            addMReq.Description1 = mReq.Description1;
            addMReq.Description2 = mReq.Description2;
            addMReq.Description3 = mReq.Description3;
            addMReq.MasterRequisitionNbrSpecified = mReq.MasterReqNbrSpecified;
            addMReq.MasterRequisitionNbr = mReq.MasterReqID;
            addMReq.CommodityCategoryCode = mReq.CommodityCode;
            addMReq.ProjectNumber = mReq.ProjectCodeSummary;
            
            ERMA.IVRequisition.NotepadTextType[] nts = new ERMA.IVRequisition.NotepadTextType[25];
            Int32 linePerPage = 0;
            Int32 page = 1;
            Int32 index = 0;
            string newline = string.Empty;

            Parsing ps = new Parsing();
            if (!(mReq.Notes is null))
            {
                if (mReq.Notes.Length > 0)
                {
                    ps.Parse(mReq.Notes, 70);

                    foreach (string ln in ps.Notes)
                    {
                        linePerPage++;
                        if (linePerPage <= 10)
                        {
                            ERMA.IVRequisition.NotepadTextType nt = new IVRequisition.NotepadTextType();
                            nt.PageNumber = page;
                            nt.LineNumber = linePerPage;
                            if (ln == Environment.NewLine)
                            {
                                newline = " ";
                            }
                            else
                            {
                                newline = ln;
                            }
                            nt.LineText = newline;
                            if (index < 10)
                            {
                                nts.SetValue(nt, index);
                            }
                            index++;

                        }
                        else
                        {
                            page++;
                            linePerPage = 0;
                        }
                    }
                }
            }
            if (!(mReq.Purpose is null))
            {
                if (mReq.Purpose.Length > 0)
                {
                    Parsing ps2 = new Parsing();
                    ps2.Parse(mReq.Purpose, 70);
                    linePerPage = 0;
                    page = 11;
                    index = 10;

                    foreach (string ln in ps2.Notes)
                    {
                        linePerPage++;
                        if (linePerPage <= 10)
                        {
                            ERMA.IVRequisition.NotepadTextType nt = new IVRequisition.NotepadTextType();
                            nt.PageNumber = page;
                            nt.LineNumber = linePerPage;
                            if (ln == Environment.NewLine)
                            {
                                newline = " ";
                            }
                            else
                            {
                                newline = ln;
                            }
                            nt.LineText = newline;
                            if (index < 20)
                            {
                                nts.SetValue(nt, index);
                            }
                            index++;

                        }
                        else
                        {
                            page++;
                            linePerPage = 0;
                        }
                    }
                }
            }
            page = 20;
            linePerPage = 1;
            index = 20;

            if (!(mReq.HowShipped is null))
            {
                if (mReq.HowShipped.Length > 0)
                {
                    ERMA.IVRequisition.NotepadTextType nt20 = new IVRequisition.NotepadTextType();
                    nt20.PageNumber = page;
                    nt20.LineNumber = linePerPage;
                    nt20.LineText = mReq.HowShipped;
                    nts.SetValue(nt20, index);
                }
            }
            linePerPage++;
            index++;

            if (!(mReq.Reason is null))
            {
                if (mReq.Reason.Length > 0)
                {
                    ERMA.IVRequisition.NotepadTextType nt21 = new IVRequisition.NotepadTextType();
                    nt21.PageNumber = page;
                    nt21.LineNumber = linePerPage;
                    nt21.LineText = mReq.Reason;
                    nts.SetValue(nt21, index);
                }
            }
            linePerPage++;
            index++;

            if (!(mReq.QuoteNumber is null))
            {
                if (mReq.QuoteNumber.Length > 0)
                {
                    ERMA.IVRequisition.NotepadTextType nt22 = new IVRequisition.NotepadTextType();
                    nt22.PageNumber = page;
                    nt22.LineNumber = linePerPage;
                    nt22.LineText = mReq.QuoteNumber;
                    nts.SetValue(nt22, index);
                }
            }

            linePerPage++;
            index++;

            ERMA.IVRequisition.NotepadTextType nt23 = new IVRequisition.NotepadTextType();
            nt23.PageNumber = page;
            nt23.LineNumber = linePerPage;
            nt23.LineText = mReq.IsReimbursable.ToString();
            nts.SetValue(nt23, index);

            addMReq.NotepadTexts = nts;


            request.Service_AddPORequsitionMaster = addMReq;

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                AddPORequsitionMasterResponse response = ((ivpwsNPO001PortType)client).AddPORequsitionMaster(request);
                CMS_ServiceResponse_AddPORequsitionMasterType addGlobalPOMasterReqResponse = response.CMS_ServiceResponse_AddPORequsitionMaster;
                //Console.WriteLine(addGlobalPOMasterReqResponse.MasterRequisitionNbr);
                returnValue = addGlobalPOMasterReqResponse.MasterRequisitionNbr.ToString();

            }
            catch (FaultException<IVRequisition.ServerFaultResponse> ex)
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter xtw = new XmlTextWriter(sw);
                //ex.Detail.WriteXml(xtw);
                xtw.Close();
                //Console.WriteLine(sw);
                returnValue = ex.Message;

            }
            catch (ProtocolException pe)
            {
                WebException we = pe.InnerException as WebException;
                //Console.WriteLine(getFaultAsString(we));
                returnValue = we.Message;
            }
            return returnValue;
        }


        //public string InquireRequisition(string ReqID, string UserName, string Password)
        //{
        //    string returnValue = string.Empty;
        //    ERMA.IVRequisition.ivpwsNPO001PortTypeClient client = new ERMA.IVRequisition.ivpwsNPO001PortTypeClient("ivpwsNPO001HttpSoap12Endpoint");

        //    // set so that service manager uses custom credentials class instead of default. 
        //    client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
        //    client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

        //    // set the credentials and path to the certificate. 
        //    client.ClientCredentials.UserName.UserName = UserName;
        //    client.ClientCredentials.UserName.Password = Password;
        //    client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4d 00 eb c7");

        //    InquirePoRequisitionerCodeRequest request = new InquirePoRequisitionerCodeRequest();
        //    //Service_UpdatePORequisitionType
        //    UpdatePORequisitionRequest requisitionRequest = new UpdatePORequisitionRequest();

        //    return string.Empty;

        //}

        public string AddRequisition(ReqDetail rd, string UserName, string Password)
        {

            string returnValue = string.Empty;

            ERMA.IVRequisition.ivpwsNPO001PortTypeClient client = new ERMA.IVRequisition.ivpwsNPO001PortTypeClient("ivpwsNPO001HttpSoap12Endpoint");

            // set so that service manager uses custom credentials class instead of default. 
            client.ChannelFactory.Endpoint.Behaviors.Remove<System.ServiceModel.Description.ClientCredentials>();
            client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

            // set the credentials and path to the certificate. 
            client.ClientCredentials.UserName.UserName = UserName;
            client.ClientCredentials.UserName.Password = Password;
            client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4d 00 eb c7");

            AddPORequisitionRequest request = new AddPORequisitionRequest();
            Service_AddPORequisitionType addReq = new Service_AddPORequisitionType();

            
            //InquirePoRequisitionerCodeRequest

            addReq.ServPlntCod = rd.ServPlntCode;
            addReq.ServLang = rd.ServLang;
            addReq.MasterRequisitionNbrSpecified = rd.MasterRequisitionNbrSpecified;
            addReq.CMSDataBase = rd.CMSDatabase;
            addReq.MasterRequisitionNbr = rd.MasterRequisitionNumber;
            addReq.RequestID = rd.RequestID;
            addReq.RequestMode = rd.RequestMode;
            addReq.PlantCode = rd.ServPlntCode; //DFT
            addReq.ItemType = " ";
            addReq.RequisitionNumberSpecified = rd.RequisitionNumberSpecified;
            addReq.VendorPartNumber = rd.VendorPartNumber;
            addReq.PartNumber = rd.TACPartNumber;
            addReq.QuantityOrdered = rd.Qty;
            addReq.QuantityOrderedSpecified = rd.QtyOrderedSpecified;
            addReq.PriceUnit = rd.UnitOfMeasure;
            addReq.OrderUnit = rd.OrderUnit;

            addReq.UnitPrice = Convert.ToDecimal(rd.PerUnitPrice);
            addReq.UnitPriceSpecified = true;

            addReq.DateRequired = rd.RequiredDate;

            if (!(rd.ProjectCode is null))
            {
                if (rd.ProjectCode != "0")
                {
                    addReq.ProjectNumber = rd.ProjectCode.Trim().ToUpper();
                }
                else
                {
                    addReq.ProjectNumber = "";
                }
            }
            else
            {
                addReq.ProjectNumber = "";
            }

            
            addReq.GLAccountNumber = rd.GlAccount;
            addReq.GLAccountNumberSpecified = true;

            addReq.InventoryStockroom = rd.InventoryStockroom;
            addReq.TaxRateCode = rd.TaxRate;
            addReq.Requisitioner = rd.Requisitioner;
            addReq.InventoryStockroom = rd.InventoryStockroom;
            addReq.TaxGroupCode = rd.TaxGroupCode;
            addReq.VendorNumber = rd.VendorCode;
            addReq.Buyer = rd.Buyer;

            addReq.DepartmentNumber = rd.DeptCode;
            
            addReq.DateRequiredSpecified = true;

            //addReq.UserDefinedFields.SetValue("1", 1);

            Int32 linePerPage = 0;
            Int32 index = 0;
            string newline = string.Empty;


            RequisitionDescriptionType[] dts = new RequisitionDescriptionType[10];

            Parsing ps = new Parsing();
            if (!(rd.ProductDescription is null))
            {
                if (rd.ProductDescription.Length > 0)
                {
                    ps.Parse(rd.ProductDescription, 30);

                    foreach (string ln in ps.Notes)
                    {
                        linePerPage++;
                        if (linePerPage <= 10)
                        {
                            RequisitionDescriptionType dt = new RequisitionDescriptionType();
                            dt.LineNumber = linePerPage;
                            dt.LineNumberSpecified = true;
                            dt.Description =  ln;

                            dts.SetValue(dt, index);
                            
                        }
                        index++;
                    }
                }
            }

            
            addReq.RequisitionDescriptions = dts;


            request.Service_AddPORequisition = addReq;


            try
            {
                
                AddPORequisitionResponse response = ((ivpwsNPO001PortType)client).AddPORequisition(request);
                CMS_ServiceResponse_AddPORequisitionType addGlobalPOReqResponse = response.CMS_ServiceResponse_AddPORequisition;
                returnValue = addGlobalPOReqResponse.RequisitionNumber;
            }
            catch (FaultException<IVRequisition.ServerFaultResponse> ex)
            {              
                //Error returns that Plant Code not Found
                returnValue = ex.Detail.CMS_ServiceResponse.Messages[0].MessageText.ToString() + Environment.NewLine + "Element = " + ex.Detail.CMS_ServiceResponse.Messages[0].Element.ToString();
            }
            catch (ProtocolException pe)
            {
                WebException we = pe.InnerException as WebException;
                returnValue = we.Message;
            }

            return returnValue;
        }

        

        /// <summary>
        /// Trust all certificates. 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="cert"></param> 
        /// <param name="chain"></param> 
        /// <param name="errors"></param> 
        /// <returns></returns> 
        public static bool TrustAllCertificatesCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary> 
        /// Transform exception into String 
        /// </summary> 
        /// <param name="we"></param> 
        /// <returns></returns> 
        public String getFaultAsString(WebException we)
        {
            String fault = "";
            if (we == null) { return fault; }
            Stream s = we.Response.GetResponseStream();
            StringBuilder sb = new StringBuilder("");
            try
            {
                byte[] readBuffer = new byte[1000];
                int count = s.Read(readBuffer, 0, readBuffer.Length);
                while (count > 0)
                {
                    sb.Append(System.Text.Encoding.UTF8.GetString(readBuffer, 0, count));
                    count = s.Read(readBuffer, 0, readBuffer.Length);
                }
                fault = sb.ToString();
            }
            catch (Exception ex)
            {
                fault = ex.Message;
            }
            finally
            {
                s.Close();
            }
            return fault;
        }

    }
}