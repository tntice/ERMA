using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERMA.Code;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;
using System.Data.Odbc;
using System.IO;

namespace ERMA.Models.ProjectViewModel
{
    public class VMGroupProject
    {

        private Int32 _PGID = 0;
        private ProjectGroup _prjGroup = new ProjectGroup();
        private List<Project> _projectList = new List<Project>();
        private List<MNGroupProject> _grpProjectList = new List<MNGroupProject>();
        private List<Project> _projectNotInGroupList = new List<Project>();
        private List<Project> _projectInGroupList = new List<Project>();
        private string _projectStatusText = string.Empty;
        private string _selectedProject = string.Empty;
        private List<SelectListItem> _cboProjectsNotInGroup = new List<SelectListItem>();


        public int PGID { get => _PGID; set => _PGID = value; }
        public ProjectGroup PrjGroup { get => _prjGroup; set => _prjGroup = value; }
        public List<Project> ProjectList { get => _projectList; set => _projectList = value; }
        public List<MNGroupProject> GrpProjectList { get => _grpProjectList; set => _grpProjectList = value; }
        public PagingInfo PagingInfo { get; set; }
        public List<Project> ProjectNotInGroupList { get => _projectNotInGroupList; set => _projectNotInGroupList = value; }
        public List<Project> ProjectInGroupList { get => _projectInGroupList; set => _projectInGroupList = value; }
        public string ProjectStatusText { get => _projectStatusText; set => _projectStatusText = value; }
        public string SelectedProject { get => _selectedProject; set => _selectedProject = value; }
        public List<SelectListItem> CboProjectsNotInGroup { get => _cboProjectsNotInGroup; set => _cboProjectsNotInGroup = value; }
    }

    public class VMGroupProjectRepository
    {
        private static ProjectGroupRepository pgRepository = new ProjectGroupRepository();
        private static ProjectRepository prjRepository = new ProjectRepository();
        private static MNGroupProjectRepository mnGrpPrjRepository = new MNGroupProjectRepository();

        public VMGroupProject GetByPGID(Int32 PGID)
        {
            VMGroupProject vmgrpProject = new VMGroupProject();

            vmgrpProject.PGID = PGID;
            vmgrpProject.PrjGroup = pgRepository.GetProjectGroupByID(PGID.ToString());
            vmgrpProject.ProjectList = prjRepository.getProjectFullList();
            vmgrpProject.GrpProjectList = mnGrpPrjRepository.GetMNGroupProjectByPGID(PGID.ToString());
            vmgrpProject.ProjectInGroupList = prjRepository.getProjectInGroup(PGID.ToString());
            vmgrpProject.ProjectNotInGroupList = prjRepository.getProjectNotInGroup(PGID.ToString(), "ALL");
            vmgrpProject.ProjectStatusText = "ALL";
            
            if(vmgrpProject.SelectedProject.Equals("ALL"))
            {
                vmgrpProject.CboProjectsNotInGroup.Add(new SelectListItem() { Text = "All", Value = "ALL", Selected = true });
            }
            else
            {
                vmgrpProject.CboProjectsNotInGroup.Add(new SelectListItem() { Text = "All", Value = "ALL"});
            }
             foreach(Project myProject in vmgrpProject.ProjectNotInGroupList)
            {
                if (vmgrpProject.SelectedProject.ToUpper().Equals(myProject.Code.ToUpper()))
                {
                    SelectListItem it = new SelectListItem() { Text = myProject.Code.ToUpper() + " - " + myProject.StatusDesc, Value = myProject.Code.ToUpper(), Selected = true };
                    vmgrpProject.CboProjectsNotInGroup.Add(it);
                }
                else
                {
                    SelectListItem it = new SelectListItem() { Text = myProject.Code.ToUpper() + " - " + myProject.StatusDesc, Value = myProject.Code.ToUpper()};
                    vmgrpProject.CboProjectsNotInGroup.Add(it);
                }
            }
            
            return vmgrpProject;
        }

        public VMGroupProject GetByPGIDAndStatus(Int32 PGID, string status)
        {
            VMGroupProject vmgrpProject = new VMGroupProject();

            vmgrpProject.PGID = PGID;
            vmgrpProject.PrjGroup = pgRepository.GetProjectGroupByID(PGID.ToString());
            vmgrpProject.ProjectList = prjRepository.getProjectFullList();
            vmgrpProject.GrpProjectList = mnGrpPrjRepository.GetMNGroupProjectByPGID(PGID.ToString());
            vmgrpProject.ProjectInGroupList = prjRepository.getProjectInGroup(PGID.ToString());
            vmgrpProject.ProjectNotInGroupList = prjRepository.getProjectNotInGroup(PGID.ToString(), status);
            vmgrpProject.ProjectStatusText = status.ToUpper();

            if (vmgrpProject.SelectedProject.Equals("ALL"))
            {
                vmgrpProject.CboProjectsNotInGroup.Add(new SelectListItem() { Text = "All", Value = "ALL", Selected = true });
            }
            else
            {
                vmgrpProject.CboProjectsNotInGroup.Add(new SelectListItem() { Text = "All", Value = "ALL" });
            }
            foreach (Project myProject in vmgrpProject.ProjectNotInGroupList)
            {
                if (vmgrpProject.SelectedProject.ToUpper().Equals(myProject.Code.ToUpper()))
                {
                    SelectListItem it = new SelectListItem() { Text = myProject.Code.ToUpper() + " - " + myProject.StatusDesc, Value = myProject.Code.ToUpper(), Selected = true };
                    vmgrpProject.CboProjectsNotInGroup.Add(it);
                }
                else
                {
                    SelectListItem it = new SelectListItem() { Text = myProject.Code.ToUpper() + " - " + myProject.StatusDesc, Value = myProject.Code.ToUpper() };
                    vmgrpProject.CboProjectsNotInGroup.Add(it);
                }
            }

            return vmgrpProject;
        }

    }
}