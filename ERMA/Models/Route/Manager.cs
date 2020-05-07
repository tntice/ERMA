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
    public class Manager
    {
        private string _empNum = string.Empty;
        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _middleName = string.Empty;
        private string _organization = string.Empty;
        private string _jobClass = string.Empty;
        private string _title = string.Empty;

        public string EmpNum { get => _empNum; set => _empNum = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        public string Organization { get => _organization; set => _organization = value; }
        public string JobClass { get => _jobClass; set => _jobClass = value; }
        public string Title { get => _title; set => _title = value; }

        public Manager() { }

    }

    public class ManagerRepository
    {

        public Manager GetManagerByDept(string DepartmentCode)
        {
            Manager mgr = new Manager();

            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetManagerByDept(DepartmentCode);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if(drt.HasData)
                {
                    mgr.EmpNum = drt.Drow.ItemArray[0].ToString().Trim();
                    mgr.LastName = drt.Drow.ItemArray[1].ToString().ToUpper().Trim();
                    mgr.FirstName = drt.Drow.ItemArray[2].ToString().ToUpper().Trim();
                    mgr.MiddleName = drt.Drow.ItemArray[3].ToString().ToUpper().Trim();
                    mgr.Organization = drt.Drow.ItemArray[4].ToString();
                    mgr.JobClass = drt.Drow.ItemArray[5].ToString().ToUpper().Trim();
                    mgr.Title = drt.Drow.ItemArray[6].ToString().ToUpper().Trim();

                }

            }
            catch { }
            return mgr; 
        }

    }
}