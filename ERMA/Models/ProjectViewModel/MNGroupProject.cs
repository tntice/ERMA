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
    public class MNGroupProject
    {
        private Int32 _MNPGID = 0;
        private Int32 _PGID = 0;
        private string _ProjectID = string.Empty;
        private ProjectGroup _selectedProjectGroup = new ProjectGroup();
        private Project _selectedProject = new Project();
        private string _createdBy = string.Empty;
        private DateTime _createdDate = DateTime.Now;
        private string _updatedBy = string.Empty;
        private DateTime _updatedDate = DateTime.Now;
        private List<Project> _projectList = new List<Project>();


        public int MNPGID { get => _MNPGID; set => _MNPGID = value; }
        public int PGID { get => _PGID; set => _PGID = value; }
        public string ProjectID { get => _ProjectID; set => _ProjectID = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string UpdatedBy { get => _updatedBy; set => _updatedBy = value; }
        public DateTime UpdatedDate { get => _updatedDate; set => _updatedDate = value; }
        public List<Project> ProjectList { get => _projectList; set => _projectList = value; }
        public ProjectGroup SelectedProjectGroup { get => _selectedProjectGroup; set => _selectedProjectGroup = value; }
        public Project SelectedProject { get => _selectedProject; set => _selectedProject = value; }

        public MNGroupProject() { }
    }

    public class MNGroupProjectRepository
    {

        public List<MNGroupProject> GetMNGroupProjectByPGID(string PGID)
        {
            List<MNGroupProject> returnList = new List<MNGroupProject>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();


            string strSQL = SQL.GetMNGroupProjectByPGID(PGID, strSchema);
            
            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if(dt.HasData)
                {
                    foreach(DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupProject gp = new MNGroupProject();
                        gp.MNPGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        gp.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(gp.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if(dt2.HasData)
                        {
                            gp.SelectedProjectGroup.PGID = gp.PGID;
                            gp.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            gp.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            gp.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            gp.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            gp.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            gp.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        gp.ProjectID = dr.ItemArray[4].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetProjectByID(gp.ProjectID, strSchema);
                        TACDataRowType dr3 = new TACDataRowType();
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if(dr3.HasData)
                        {
                            gp.SelectedProject.Code = dr3.Drow.ItemArray[0].ToString();
                            gp.SelectedProject.Description = dr3.Drow.ItemArray[1].ToString();
                        }

                        gp.CreatedBy = dr.ItemArray[6].ToString();
                        gp.CreatedDate = Convert.ToDateTime(dr.ItemArray[7].ToString());
                        gp.UpdatedBy = dr.ItemArray[8].ToString();
                        gp.UpdatedDate = Convert.ToDateTime(dr.ItemArray[9].ToString());

                        returnList.Add(gp);

                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupProject> GetMNGroupProjectByPGIDAndProjNum(string PGID, string ProjNum)
        {
            List<MNGroupProject> returnList = new List<MNGroupProject>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();


            string strSQL = SQL.GetMNGroupProjectByPGIDAndProjNum(PGID, ProjNum, strSchema);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupProject gp = new MNGroupProject();
                        gp.MNPGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        gp.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(gp.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            gp.SelectedProjectGroup.PGID = gp.PGID;
                            gp.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            gp.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            gp.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            gp.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            gp.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            gp.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        gp.ProjectID = dr.ItemArray[4].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetProjectByID(gp.ProjectID, strSchema);
                        TACDataRowType dr3 = new TACDataRowType();
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if (dr3.HasData)
                        {
                            gp.SelectedProject.Code = dr3.Drow.ItemArray[0].ToString();
                            gp.SelectedProject.Description = dr3.Drow.ItemArray[1].ToString();
                        }

                        gp.CreatedBy = dr.ItemArray[6].ToString();
                        gp.CreatedDate = Convert.ToDateTime(dr.ItemArray[7].ToString());
                        gp.UpdatedBy = dr.ItemArray[8].ToString();
                        gp.UpdatedDate = Convert.ToDateTime(dr.ItemArray[9].ToString());

                        returnList.Add(gp);

                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupProject> GetMNGroupProjectByProjNumWithoutPGID(string PGID, string ProjNum)
        {
            List<MNGroupProject> returnList = new List<MNGroupProject>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();


            string strSQL = SQL.GetMNGroupProjectByProjNumWithoutPGID(PGID, ProjNum, strSchema);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupProject gp = new MNGroupProject();
                        gp.MNPGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        gp.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(gp.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            gp.SelectedProjectGroup.PGID = gp.PGID;
                            gp.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            gp.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            gp.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            gp.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            gp.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            gp.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        gp.ProjectID = dr.ItemArray[4].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetProjectByID(gp.ProjectID, strSchema);
                        TACDataRowType dr3 = new TACDataRowType();
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if (dr3.HasData)
                        {
                            gp.SelectedProject.Code = dr3.Drow.ItemArray[0].ToString();
                            gp.SelectedProject.Description = dr3.Drow.ItemArray[1].ToString();
                        }

                        gp.CreatedBy = dr.ItemArray[6].ToString();
                        gp.CreatedDate = Convert.ToDateTime(dr.ItemArray[7].ToString());
                        gp.UpdatedBy = dr.ItemArray[8].ToString();
                        gp.UpdatedDate = Convert.ToDateTime(dr.ItemArray[9].ToString());

                        returnList.Add(gp);

                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupProject> GetMNGroupProjectByMNPGID(string MNPGID)
        {
            List<MNGroupProject> returnList = new List<MNGroupProject>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();


            string strSQL = SQL.GetMNGroupProjectByMNPGID(MNPGID, strSchema);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupProject gp = new MNGroupProject();
                        gp.MNPGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        gp.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(gp.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            gp.SelectedProjectGroup.PGID = gp.PGID;
                            gp.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            gp.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            gp.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            gp.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            gp.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            gp.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        gp.ProjectID = dr.ItemArray[4].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetProjectByID(gp.ProjectID, strSchema);
                        TACDataRowType dr3 = new TACDataRowType();
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if (dr3.HasData)
                        {
                            gp.SelectedProject.Code = dr3.Drow.ItemArray[0].ToString();
                            gp.SelectedProject.Description = dr3.Drow.ItemArray[1].ToString();
                        }

                        gp.CreatedBy = dr.ItemArray[6].ToString();
                        gp.CreatedDate = Convert.ToDateTime(dr.ItemArray[7].ToString());
                        gp.UpdatedBy = dr.ItemArray[8].ToString();
                        gp.UpdatedDate = Convert.ToDateTime(dr.ItemArray[9].ToString());

                        returnList.Add(gp);

                    }
                }
            }
            catch { }

            return returnList;

        }

        public Int32 Insert(MNGroupProject prjGroup)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            prjGroup.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            prjGroup.UpdatedBy = prjGroup.CreatedBy;

            Int32 returnValue = 0;
            string strSQL = SQL.InsertMNGroupProject(prjGroup);
            TACNonDataType ndt = new TACNonDataType();

            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }

            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public Int32 Delete(string MNPGID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            Int32 returnValue = 0;
            string strSQL = SQL.DeleteMNGroupProject(MNPGID);
            TACNonDataType ndt = new TACNonDataType();

            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if(ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public Int32 Delete(Int32 PGID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            Int32 returnValue = 0;
            string strSQL = SQL.DeleteMNGroupProjectByPGID(PGID.ToString());

            TACNonDataType ndt = new TACNonDataType();
            try
            {

                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }
            }
            catch
            {
                returnValue = -1;
            }

            return returnValue;
        }

    }
}