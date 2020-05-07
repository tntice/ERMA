using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERMA.Models
{
    public class ReqFooter
    {
        private string _reqNumber = string.Empty;
        private string _ringiSho = string.Empty;
        private string _quoteNumber = string.Empty;
        private string _projectCodeSummary = string.Empty;
        private bool _isReimbursable = true;
        private string _comments = string.Empty;
        private string _purpose = string.Empty;

        public string ReqNumber { get => _reqNumber; set => _reqNumber = value; }
        public string RingiSho { get => _ringiSho; set => _ringiSho = value; }
        public string QuoteNumber { get => _quoteNumber; set => _quoteNumber = value; }
        public string ProjectCodeSummary { get => _projectCodeSummary; set => _projectCodeSummary = value; }
        public bool IsReimbursable { get => _isReimbursable; set => _isReimbursable = value; }
        public string Comments { get => _comments; set => _comments = value; }
        public string Purpose { get => _purpose; set => _purpose = value; }

        public ReqFooter() { }
    }
}