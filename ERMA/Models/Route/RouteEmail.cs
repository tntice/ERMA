using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TACData;
using TACUtility;
using ERMA.Code;
using ERMA.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Net.Mail;


namespace ERMA.Models.Route
{
    public class RouteEmail
    {
        private Person _recipient = new Person();
        private string _from = string.Empty;
        private string _subject = string.Empty;
        private StringBuilder _body = new StringBuilder();
        private List<ReqDetail> _reqLineItems = new List<ReqDetail>();
        private MasterRequisition _mReq = new MasterRequisition();


        public Person Recipient { get => _recipient; set => _recipient = value; }
        public string From { get => _from; set => _from = value; }
        public string Subject { get => _subject; set => _subject = value; }
        public StringBuilder Body { get => _body; set => _body = value; }
        public List<ReqDetail> ReqLineItems { get => _reqLineItems; set => _reqLineItems = value; }
        public MasterRequisition MReq { get => _mReq; set => _mReq = value; }

        public RouteEmail() { }

    }
}