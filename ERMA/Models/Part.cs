using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERMA.Models
{
    public class Part
    {
        private string _supplierPartNumber = string.Empty;
        private string _TACPartNumber = string.Empty;
        private string _description = string.Empty;
        private string _commodityCode = string.Empty;
        private bool _isReviewByEnvironmental = false;
         
        public string SupplierPartNumber { get => _supplierPartNumber; set => _supplierPartNumber = value; }
        public string TACPartNumber { get => _TACPartNumber; set => _TACPartNumber = value; }
        public string Description { get => _description; set => _description = value; }
        public string CommodityCode { get => _commodityCode; set => _commodityCode = value; }
        public bool IsReviewByEnvironmental { get => _isReviewByEnvironmental; set => _isReviewByEnvironmental = value; }

        public Part() { }
    }
}