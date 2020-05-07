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

namespace ERMA.Models
{
    public class Project
    {
        private string _code = string.Empty;
        private string _userName = string.Empty;
        private string _level = string.Empty;
        private string _description = string.Empty;
        private string _statusCode = string.Empty;
        private string _statusDesc = string.Empty;

        [Display(Name = "Project #")]
        public string Code { get => _code; set => _code = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Level { get => _level; set => _level = value; }
        [Display(Name = "Project Description")]
        public string Description { get => _description; set => _description = value; }
        [Display(Name = "Status Code")]
        public string StatusCode { get => _statusCode; set => _statusCode = value; }
        [Display(Name = "Status")]
        public string StatusDesc { get => _statusDesc; set => _statusDesc = value; }
    }

    public class ProjectRepository
    {
        public List<Project> GetProjectList(string userName)
        {
            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectPermissionList(userName, strSchema);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.UserName = dr.ItemArray[1].ToString().Trim();
                        prj.Level = dr.ItemArray[2].ToString().Trim();
                        prj.Description = dr.ItemArray[3].ToString().Trim();
                        prj.Description = prj.Code + " - " + prj.Description;
                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {

            }
            return lstReturn;

        }

        public List<Project> getProjectFullList()
        {

            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectFullList(strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.Description = dr.ItemArray[1].ToString().Trim();
                        
                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {
            }
            return lstReturn;

        }

        public Project GetProjectByID(string ProjNum)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            Project prj = new Project();

            string strSQL = SQL.GetProjectByID(ProjNum, strSchema);

            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    prj.Code = dr.ItemArray[0].ToString().Trim();
                    prj.Description = dr.ItemArray[1].ToString().Trim();
                }
            }
            catch
            {
            }
            return prj;
        }

        public List<Project> getProjectActiveList()
        {

            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectActiveList(strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.Description = dr.ItemArray[1].ToString().Trim();

                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {
            }
            return lstReturn;

        }

        public List<Project> getProjectInActiveList()
        {

            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectInActiveList(strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.Description = dr.ItemArray[1].ToString().Trim();

                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {
            }
            return lstReturn;

        }

        public Project GetProjectDescription(string UserName, string ProjectNum )
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            Project prj = new Project();
                       
            string strSQL = SQL.GetProjectCodeDescription(UserName, ProjectNum, strSchema);

            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    prj.Code = dr.ItemArray[0].ToString().Trim();
                    prj.UserName = dr.ItemArray[1].ToString().Trim().ToUpper();
                    prj.Level = dr.ItemArray[2].ToString().Trim();
                    prj.Description = dr.ItemArray[3].ToString().Trim();
                    prj.Description = prj.Code + " - " + prj.Description;
                }
            }
            catch
            {
            }
            return prj;

        }

        public List<Project> getProjectInGroup(string PGID)
        {

            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectsInProjectGroupByPGID(PGID);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.Description = dr.ItemArray[1].ToString().Trim();
                        prj.StatusCode = dr.ItemArray[2].ToString().Trim();
                        prj.StatusDesc = dr.ItemArray[3].ToString().Trim();

                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {
            }
            return lstReturn;

        }

        public List<Project> getProjectNotInGroup(string PGID, string filter)
        {

            List<Project> lstReturn = new List<Project>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            DataTable dt = new DataTable();

            string sSQL = SQL.GetProjectsNotInProjectGroupByPGID(PGID, filter);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Project prj = new Project();
                        prj.Code = dr.ItemArray[0].ToString().Trim();
                        prj.Description = dr.ItemArray[1].ToString().Trim();
                        prj.StatusCode = dr.ItemArray[2].ToString().Trim();
                        prj.StatusDesc = dr.ItemArray[3].ToString().Trim();

                        lstReturn.Add(prj);
                    }
                }
            }
            catch
            {
            }
            return lstReturn;

        }
    }
}