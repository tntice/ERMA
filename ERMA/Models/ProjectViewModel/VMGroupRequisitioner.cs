using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Code;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;
using System.Data.Odbc;
using System.IO;

namespace ERMA.Models.ProjectViewModel
{
    public class VMGroupRequisitioner
    {
        private Int32 _PGID = 0;
        private ProjectGroup _prjGroup = new ProjectGroup();
        private List<Requisitioner> _RequisitionerList = new List<Requisitioner>();
        private List<MNGroupRequisitioner> _grpRequisitionerList = new List<MNGroupRequisitioner>();
        private string _selectedRequisitioner = string.Empty;

        public int PGID { get => _PGID; set => _PGID = value; }
        public ProjectGroup PrjGroup { get => _prjGroup; set => _prjGroup = value; }
        public List<Requisitioner> RequisitionerList { get => _RequisitionerList; set => _RequisitionerList = value; }
        public List<MNGroupRequisitioner> GrpRequisitionerList { get => _grpRequisitionerList; set => _grpRequisitionerList = value; }
        public string SelectedRequisitioner { get => _selectedRequisitioner; set => _selectedRequisitioner = value; }
    }

    public class VMGroupRequisitionerRepository
    {
        private static ProjectGroupRepository pgRepository = new ProjectGroupRepository();
        private static MNGroupRequisitionerRepository grpReqRepository = new MNGroupRequisitionerRepository();
        private static RequisitionerRepository reqRepository = new RequisitionerRepository();

        public VMGroupRequisitioner GetByPGID(Int32 PGID)
        {
            VMGroupRequisitioner grpReq = new VMGroupRequisitioner();

            grpReq.PGID = PGID;
            grpReq.PrjGroup = pgRepository.GetProjectGroupByID(PGID.ToString());
            grpReq.GrpRequisitionerList = grpReqRepository.GetMNGroupRequisitionerByPGID(PGID.ToString());
            grpReq.RequisitionerList = reqRepository.GetAllRequisitioners();

            return grpReq;
        }
    }
}