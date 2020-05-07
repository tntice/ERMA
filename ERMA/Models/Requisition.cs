using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Models.Route;


namespace ERMA.Models
{
    public class Requisition
    {
        private string _masterRequisitionNumber = string.Empty;
        private string _requisitionNumber = string.Empty;
        private ReqHeader _requisitionHeader = new ReqHeader();
        private ReqFooter _requisitionFooter = new ReqFooter();
        private List<ReqDetail> _requisitionDetails = new List<ReqDetail>();
        private Vendor _reqVendor = new Vendor();
        private List<Attachment> _reqAttachments = new List<Attachment>();
        private List<Approval> _reqApprovals = new List<Approval>();


        public string MasterRequisitionNumber { get => _masterRequisitionNumber; set => _masterRequisitionNumber = value; }
        public string RequisitionNumber { get => _requisitionNumber; set => _requisitionNumber = value; }
        public ReqHeader RequisitionHeader { get => _requisitionHeader; set => _requisitionHeader = value; }
        public ReqFooter RequisitionFooter { get => _requisitionFooter; set => _requisitionFooter = value; }
        public List<ReqDetail> RequisitionDetails { get => _requisitionDetails; set => _requisitionDetails = value; }
        public Vendor ReqVendor { get => _reqVendor; set => _reqVendor = value; }
        public List<Attachment> ReqAttachments { get => _reqAttachments; set => _reqAttachments = value; }
        public List<Approval> ReqApprovals { get => _reqApprovals; set => _reqApprovals = value; }

        public Requisition() { }
    }
}