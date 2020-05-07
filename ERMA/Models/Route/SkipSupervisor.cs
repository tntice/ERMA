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

namespace ERMA.Models.Route
{
    public class SkipSupervisor
    {
        private string _skipID = string.Empty;
        private string _department = string.Empty;
        private string _jobClass = string.Empty;

        public string SkipID { get => _skipID; set => _skipID = value; }
        public string Department { get => _department; set => _department = value; }
        public string JobClass { get => _jobClass; set => _jobClass = value; }

        public SkipSupervisor() { }
    }

    public class SkipSupervisortRepository
    {

        public SkipSupervisor GetSkipSupervisor(string DepartmentCode, string JobClass)
        {
            SkipSupervisor skip = new SkipSupervisor();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetSkipSupervisor(DepartmentCode, JobClass);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    skip.SkipID = drt.Drow.ItemArray[0].ToString();
                    skip.Department = drt.Drow.ItemArray[1].ToString();
                    skip.JobClass = drt.Drow.ItemArray[2].ToString();
                }

            }
            catch
            {

            }
            return skip;
        }

        public bool IsSupervisorSkipped(string DepartmentCode, string JobClass)
        {
            bool isSkipped = false;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetSkipSupervisor(DepartmentCode, JobClass);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);
                isSkipped = drt.HasData;
            }
            catch
            {
                isSkipped = false;
            }
            return isSkipped;
        }
    }
}