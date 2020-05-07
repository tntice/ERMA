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

namespace ERMA.Code
{
    public class Program
    {
        /// <summary> 
        /// /// Start. 
        /// /// </summary> 
        /// /// <param name="args"></param>
        static void Main(string[] args)
        {
            // set services manager to trust non-trusted certificates 
            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;

            new Program();
        }

        /// <summary> 
        /// Constructor. 
        /// </summary> 
        public Program()
        {
            // set the client to use SOAP 1.2
            //CMSwsIV001PortTypeClient client = new CMSwsIV001PortTypeClient("CMSwsIV001HttpSoap12Endpoint");
            ERMA.IVRequisition.ivpwsNPO001PortTypeClient client = new ERMA.IVRequisition.ivpwsNPO001PortTypeClient("CMSwsIV001HttpSoap12Endpoint");

            // set so that service manager uses custom credentials class instead of default. 
            client.ChannelFactory.Endpoint.Behaviors.Remove < System.ServiceModel.Description.ClientCredentials > ();
            client.ChannelFactory.Endpoint.Behaviors.Add(new CustomCredentials());

            // set the credentials and path to the certificate. 
            client.ClientCredentials.UserName.UserName = "PCUTIL";
            client.ClientCredentials.UserName.Password = "pcutil";
            client.ClientCredentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber, "4b 56 0e 01");
            //inquireGlobalPart(client);
            inquireGlobalRequisition(client);

        }

        private void ExploreProduction(IVProduction.ivpwsPD001PortTypeClient client)
        {
            IVProduction.AddMethodRoutingRequest request = new IVProduction.AddMethodRoutingRequest();
            IVProduction.Service_AddMethodRoutingType addRoute = new IVProduction.Service_AddMethodRoutingType();

            addRoute.AlternateMethodNbr = "1";


            IVProduction.InquireMethodRoutingRequest request2 = new IVProduction.InquireMethodRoutingRequest();
            IVProduction.Service_InquireMethodRoutingType inquireRoute = new IVProduction.Service_InquireMethodRoutingType();

            inquireRoute.AlternateMethodNbr = "2";

            IVProduction.Service_InquireMethodRoutingOperationDescriptionType i2 = new IVProduction.Service_InquireMethodRoutingOperationDescriptionType();
            i2.AlternateMethodNbr = 3;

            IVProduction.Service_InquireMethodRoutingOperationDetailType i3 = new IVProduction.Service_InquireMethodRoutingOperationDetailType();
            i3.AlternateMethodNbr = 4;

            IVProduction.Service_InquireMethodRoutingOperationDescriptionType i4 = new IVProduction.Service_InquireMethodRoutingOperationDescriptionType();
            i4.AlternateMethodNbr = 5;

            //IVProduction.InquireMethodRoutingOperationDescriptionResponse r4 = ((IVProduction.ivpwsPD001PortType)client).InquireMethodRoutingOperationDescription(request2);


        }

        private void addVendor(IVMain.ivpwsMAIN001PortTypeClient client)
        {
            IVMain.AddVendorRequest request = new IVMain.AddVendorRequest();
            IVMain.Service_AddVendorType addVen = new IVMain.Service_AddVendorType();
            request.Service_AddVendor = addVen;

            addVen.CMSDataBase = "CMSDATEDI";
            addVen.VendorName = "TNToys";
            addVen.VendorCode = "1023";
            addVen.AddressLine01 = "1 Sakura Drive";


            try
            {
                IVMain.AddVendorResponse response = ((ivpwsMAIN001PortType)client).AddVendor(request);
                //CMS_ServiceResponse_AddVendorType;
                IVMain.CMS_ServiceResponseType GlobalResponse = response.CMS_ServiceResponse;
                Console.WriteLine(GlobalResponse.Messages);

                
                //AddPORequisitionResponse response = ((ivpwsNPO001PortType)client).AddPORequisition(request);
                //CMS_ServiceResponse_AddPORequisitionType addGlobalPOReqResponse = response.CMS_ServiceResponse_AddPORequisition;
                //Console.WriteLine(addGlobalPOReqResponse.RequisitionNumber);


            }
            catch { }
        }


        private void inquireGlobalRequisition(IVRequisition.ivpwsNPO001PortTypeClient client)
        {
            
            AddPORequisitionRequest request = new AddPORequisitionRequest();
            Service_AddPORequisitionType addReq = new Service_AddPORequisitionType();

            request.Service_AddPORequisition = addReq;

            addReq.CMSDataBase = "CMSDATEDI";
            addReq.ServPlntCod = "DFT";
            addReq.ServLang = "ENU";
            addReq.Buyer = "TTILLOTSON";
            addReq.CommodityCategoryCode = "XYZ";
            addReq.CoOAddress1 = "1 Sakura Drive";
            addReq.CoOAddress2 = "";
            addReq.CoOCity = "Springfield";
            addReq.CoOStateCode = "KY";
            addReq.CoOCountryCode = "USA";
            addReq.CountryOfOrigin = "United States of America";
            
            addReq.DateRequired = DateTime.Now;
            addReq.DepartmentNumber = "02015";
            addReq.FOBAddress1 = "210 Abe Way";
            addReq.FOBCity = "Hodgenville";
            addReq.FOBStateCode = "KY";
            addReq.GLAccountNumber = 2010213;
            addReq.GLAccountNumberSpecified = true;
            addReq.InventoryStockroom = "B103";
            addReq.ItemType = "SVR";
            addReq.JobNumber = "aodfi";
            addReq.JobSequenceNumber = 1;
            addReq.JobSequenceNumberSpecified = true;
            addReq.DateRequiredSpecified = true;
            addReq.ProjectCostCode = "project Code";
            //addReq.RequisitionNumber = 1;
            


            try
            {
                AddPORequisitionResponse response = ((ivpwsNPO001PortType)client).AddPORequisition(request);
                CMS_ServiceResponse_AddPORequisitionType addGlobalPOReqResponse = response.CMS_ServiceResponse_AddPORequisition;
                Console.WriteLine(addGlobalPOReqResponse.RequisitionNumber);


            }
            catch(FaultException<IVRequisition.ServerFaultResponse> ex)
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter xtw = new XmlTextWriter(sw);
                //ex.Detail.WriteXml(xtw);
                xtw.Close();
                Console.WriteLine(sw);
            }
            catch (ProtocolException pe)
            {
                WebException we = pe.InnerException as WebException;
                Console.WriteLine(getFaultAsString(we));
            }

        }



               




        /// <summary> 
        /// Inquire the Global Part.
        /// </summary> 
        /// <param name="client"></param> 
        //private void inquireGlobalPart(IVRequisition.ivpwsNPO001PortTypeClient client)
        //{
        //    InquireGlobalPartRequest request = new InquireGlobalPartRequest();
        //    Service_InquireGlobalPartType inquireGlobalPart = new Service_InquireGlobalPartType();
        //    request.Service_InquireGlobalPart = inquireGlobalPart;
        //    inquireGlobalPart.RequestID = "TESTID0001";
        //    inquireGlobalPart.RequestMode = "TESTMODE0001";
        //    inquireGlobalPart.CMSDataBase = "CMSDATA";
        //    inquireGlobalPart.ServLang = "ENU";
        //    inquireGlobalPart.ServPlntCod = "DFT";
        //    inquireGlobalPart.InternalPartNumber = "100000";
        //    try
        //    {
        //        InquireGlobalPartResponse response = ((CMSwsIV001PortType)client).InquireGlobalPart(request);
        //        CMS_ServiceResponse_InquireGlobalPartType inquireGlobalPartResponse = response.CMS_ServiceResponse_InquireGlobalPart;
        //        Console.WriteLine(inquireGlobalPartResponse.CMSDataBase);
        //    }
        //    catch (FaultException<ServerFaultResponse> ex)
        //    {
        //        StringWriter sw = new StringWriter();
        //        XmlTextWriter xtw = new XmlTextWriter(sw);
        //        ex.Detail.WriteXml(xtw);
        //        xtw.Close();
        //        Console.WriteLine(sw);
        //    }
        //    catch (ProtocolException pe)
        //    {
        //        WebException we = pe.InnerException as WebException;
        //        Console.WriteLine(getFaultAsString(we));
        //    }
        //}

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