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


namespace ERMA.Models.ProjectViewModel
{
    public class ProjectGroup
    {
        private Int32 _PGID = 0;
        private string _name = string.Empty;
        private string _status = string.Empty;
        private DateTime _createdDate = DateTime.Now;
        private string _createdBy = string.Empty;
        private DateTime _updatedDate = DateTime.Now;
        private string _updatedBy = string.Empty;
        private List<Status> _statusList = new List<Status>();

        public int PGID { get => _PGID; set => _PGID = value; }
        [Display(Name="Group Name")]
        public string Name { get => _name; set => _name = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public DateTime UpdatedDate { get => _updatedDate; set => _updatedDate = value; }
        public string UpdatedBy { get => _updatedBy; set => _updatedBy = value; }
        public List<Status> StatusList { get => _statusList; set => _statusList = value; }

        public ProjectGroup() { }

        public ProjectGroup(Int32 pgid, string name, string status, string userId)
        {
            _PGID = pgid;
            _name = name;
            _status = status;
            _createdBy = userId;
            _updatedBy = userId;
        }
    }

    public class ProjectGroupRepository
    {
        public List<ProjectGroup> GetProjectGroups()
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSQL = SQL.GetProjectGroupList();

            TACDataTableType dt = new TACDataTableType();
            List<ProjectGroup> projgroups = new List<ProjectGroup>();

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        ProjectGroup pg = new ProjectGroup();
                        pg.PGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        pg.Name = dr.ItemArray[1].ToString().Trim();
                        pg.Status = dr.ItemArray[2].ToString().Trim().ToUpper();
                        pg.CreatedDate = Convert.ToDateTime(dr.ItemArray[3].ToString());
                        pg.CreatedBy = dr.ItemArray[4].ToString().ToUpper().Trim();
                        pg.UpdatedDate = Convert.ToDateTime(dr.ItemArray[5].ToString());
                        pg.UpdatedBy = dr.ItemArray[6].ToString().ToUpper().Trim();

                        projgroups.Add(pg);
                    }
                }
            }
            catch { }

            return projgroups;
        }

        public List<ProjectGroup> GetProjectGroupsByStatus(string status)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSQL = SQL.GetProjectGroupListByStatus(status);

            TACDataTableType dt = new TACDataTableType();
            List<ProjectGroup> projgroups = new List<ProjectGroup>();

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if(dt.HasData)
                {
                    foreach(DataRow dr in dt.Dtable.Rows)
                    {
                        ProjectGroup pg = new ProjectGroup();
                        pg.PGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        pg.Name = dr.ItemArray[1].ToString().Trim();
                        pg.Status = dr.ItemArray[2].ToString().Trim().ToUpper();
                        pg.CreatedDate = Convert.ToDateTime(dr.ItemArray[3].ToString());
                        pg.CreatedBy = dr.ItemArray[4].ToString().ToUpper().Trim();
                        pg.UpdatedDate = Convert.ToDateTime(dr.ItemArray[5].ToString());
                        pg.UpdatedBy = dr.ItemArray[6].ToString().ToUpper().Trim();

                        projgroups.Add(pg);
                    }
                }
            }
            catch { }

            return projgroups;
        }

        public ProjectGroup GetProjectGroupByID(string PGID)
        {
            Int32 iPGID = 0;
            if(PGID.IsNumeric())
            {
                iPGID = Convert.ToInt32(PGID);
            }
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            ProjectGroup prjGroup = new ProjectGroup();

            string strSQL = SQL.GetProjectGroupByID(iPGID);
            TACDataRowType dr = new TACDataRowType();

            try
            {
                dr = Data.GetTACDataRow(strSQL, cCon);
                if(dr.HasData)
                {
                    prjGroup.PGID = iPGID;
                    prjGroup.Name = dr.Drow.ItemArray[1].ToString().ToUpper().Trim();
                    prjGroup.Status = dr.Drow.ItemArray[2].ToString().ToUpper().Trim();
                    prjGroup.CreatedDate = Convert.ToDateTime(dr.Drow.ItemArray[3].ToString());
                    prjGroup.CreatedBy = dr.Drow.ItemArray[4].ToString().ToUpper().Trim();
                    prjGroup.UpdatedDate = Convert.ToDateTime(dr.Drow.ItemArray[5].ToString());
                    prjGroup.UpdatedBy = dr.Drow.ItemArray[6].ToString().ToUpper().Trim();
                }
            }
            catch { }

            return prjGroup;

        }

        public Int32 Update(ProjectGroup prjGroup)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();

            prjGroup.UpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            prjGroup.UpdatedDate = DateTime.Now;

            string strSQL = SQL.UpdateProjectGroup(prjGroup);
            Int32 iCount = 0;
            TACNonDataType tacN = new TACNonDataType();
            try
            {
                tacN = Data.nonTACQuery(strSQL, cCon);
                if(tacN.IsSuccessful)
                {
                    iCount = tacN.CountAffected;
                }
            }
            catch { }

            return iCount;
        }

        public Int32 Delete(ProjectGroup prjGroup)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();

            prjGroup.UpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            prjGroup.UpdatedDate = DateTime.Now;

            string strSQL = SQL.DeleteProjectGroup(prjGroup);

            Int32 iCount = 0;
            TACNonDataType tacN = new TACNonDataType();

            try
            {
                tacN = Data.nonTACQuery(strSQL, cCon);
                if (tacN.IsSuccessful)
                {
                    iCount = tacN.CountAffected;
                }
            }
            catch { }
            return iCount;
        }

        public Int32 Insert(ProjectGroup prjGroup)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();

            prjGroup.UpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            prjGroup.UpdatedDate = DateTime.Now;
            prjGroup.CreatedBy = prjGroup.UpdatedBy;
            prjGroup.CreatedDate = prjGroup.UpdatedDate;
            prjGroup.Status = "A";

            string strSQL = SQL.InsertProjectGroup(prjGroup);
            Int32 iCount = 0;

            TACNonDataType tacN = new TACNonDataType();
            try
            {
                tacN = Data.nonTACQuery(strSQL, cCon);
                if(tacN.IsSuccessful)
                {
                    iCount = tacN.CountAffected;
                }
            }
            catch { }
            return iCount;     
        }
    }

}