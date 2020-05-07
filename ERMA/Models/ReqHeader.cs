using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERMA.Models
{
    public class ReqHeader
    {
        private string _reqNum = string.Empty;
        private string _vendorCode = string.Empty;
        private DateTime _createdDate;
        private string _buyerName = string.Empty;
        private string _originator = string.Empty;
        private string _reqStatus = string.Empty;
        private string _deptCode = string.Empty;
        private string _RequiredDate = string.Empty;
        private string _reason = string.Empty;
        private string _howShipped = string.Empty;
        //Project Code, 
        public string ReqNum { get => _reqNum; set => _reqNum = value; }
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string BuyerName { get => _buyerName; set => _buyerName = value; }
        public string Originator { get => _originator; set => _originator = value; }
        public string ReqStatus { get => _reqStatus; set => _reqStatus = value; }
        public string DeptCode { get => _deptCode; set => _deptCode = value; }
        public string RequiredDate { get => _RequiredDate; set => _RequiredDate = value; }
        public string Reason { get => _reason; set => _reason = value; }
        public string HowShipped { get => _howShipped; set => _howShipped = value; }

        public ReqHeader() { }
    }
}