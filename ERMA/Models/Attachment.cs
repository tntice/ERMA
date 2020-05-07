using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERMA.Models
{
    public class Attachment
    {
        private string _requisitionNumber = string.Empty;
        private string _path = string.Empty;
        private string _fileType = string.Empty;

        public string RequisitionNumber { get => _requisitionNumber; set => _requisitionNumber = value; }
        public string Path { get => _path; set => _path = value; }
        public string FileType { get => _fileType; set => _fileType = value; }

        public Attachment() { }
    }
}