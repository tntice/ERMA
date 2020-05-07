using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TACData;
using TACUtility;
using ERMA.Code;
using ERMA.Models;
using ERMA.Models.Route;
using System.ComponentModel.DataAnnotations;
using System.Data;


namespace ERMA.Models.RouteViewModel
{
    public class VMApproval
    {
        private Approval _thisApprovals = new Approval();
        private List<MasterRequisition> _mastReqs = new List<MasterRequisition>();
        private List<ReqDetail> _reqDets = new List<ReqDetail>();

        public Approval ThisApprovals { get => _thisApprovals; set => _thisApprovals = value; }
        public List<MasterRequisition> MastReqs { get => _mastReqs; set => _mastReqs = value; }
        public List<ReqDetail> ReqDets { get => _reqDets; set => _reqDets = value; }

        public VMApproval()
        {
            
        }
    }
}