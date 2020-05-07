using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERMA.Models
{
    public class RequisitionLineItemDefaults
    {
        private string _glAccount = string.Empty;
        private string _deptCode = string.Empty;
        private string _requiredDate = DateTime.Now.AddDays(14).ToShortDateString();
        private string _vendorCode = string.Empty;

        [Display(Name ="GL Account")]
        public string GlAccount { get => _glAccount; set => _glAccount = value; }
        [Display(Name = "Department")]
        public string DeptCode { get => _deptCode; set => _deptCode = value; }
        [Display(Name = "Date Required")]
        public string RequiredDate { get => _requiredDate; set => _requiredDate = value; }
        [Display(Name = "Vendor Code")]
        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
    }
}