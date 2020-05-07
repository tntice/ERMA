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
    public class Supervisor
    {
        private string _underlingEmpNum = string.Empty;
        private string _underlingFirstName = string.Empty;
        private string _underlingLastName = string.Empty;
        private string _underlingSupervisorID = string.Empty;
        private string _underlingJobClass = string.Empty;
        private string _underlingTitle = string.Empty;
        private string _supervisorEmpNum = string.Empty;
        private string _supervisorFirstName = string.Empty;
        private string _supervisorLastName = string.Empty;
        private string _supervisorDescription = string.Empty;
        private string _supervisorJobClass = string.Empty;
        private string _supervisorTitle = string.Empty;

        public string UnderlingEmpNum { get => _underlingEmpNum; set => _underlingEmpNum = value; }
        public string UnderlingFirstName { get => _underlingFirstName; set => _underlingFirstName = value; }
        public string UnderlingLastName { get => _underlingLastName; set => _underlingLastName = value; }
        public string UnderlingSupervisorID { get => _underlingSupervisorID; set => _underlingSupervisorID = value; }
        public string UnderlingJobClass { get => _underlingJobClass; set => _underlingJobClass = value; }
        public string UnderlingTitle { get => _underlingTitle; set => _underlingTitle = value; }
        public string SupervisorEmpNum { get => _supervisorEmpNum; set => _supervisorEmpNum = value; }
        public string SupervisorFirstName { get => _supervisorFirstName; set => _supervisorFirstName = value; }
        public string SupervisorLastName { get => _supervisorLastName; set => _supervisorLastName = value; }
        public string SupervisorDescription { get => _supervisorDescription; set => _supervisorDescription = value; }
        public string SupervisorJobClass { get => _supervisorJobClass; set => _supervisorJobClass = value; }
        public string SupervisorTitle { get => _supervisorTitle; set => _supervisorTitle = value; }

        public Supervisor() { }

    }

    public class SupervisorRepository
    {
        public Supervisor GetSupervisor(string EmployeeNumber)
        {
            Supervisor sup = new Supervisor();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetSupervisorByID(EmployeeNumber);

            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    sup.UnderlingEmpNum = drt.Drow.ItemArray[0].ToString().Trim();
                    sup.UnderlingFirstName = drt.Drow.ItemArray[1].ToString().ToUpper().Trim();
                    sup.UnderlingLastName = drt.Drow.ItemArray[2].ToString().ToUpper().Trim();
                    sup.UnderlingSupervisorID = drt.Drow.ItemArray[3].ToString();
                    sup.UnderlingJobClass = drt.Drow.ItemArray[4].ToString().ToUpper().Trim();
                    sup.SupervisorEmpNum = drt.Drow.ItemArray[5].ToString().Trim();
                    sup.SupervisorFirstName = drt.Drow.ItemArray[6].ToString().ToUpper().Trim();
                    sup.SupervisorLastName = drt.Drow.ItemArray[7].ToString().ToUpper().Trim();
                    sup.SupervisorDescription = drt.Drow.ItemArray[8].ToString();
                    sup.SupervisorJobClass = drt.Drow.ItemArray[9].ToString().ToUpper().Trim();
                    sup.SupervisorTitle = drt.Drow.ItemArray[10].ToString().ToUpper().Trim();
                    sup.UnderlingTitle = drt.Drow.ItemArray[11].ToString().ToUpper().Trim();
                }
            }
            catch { }

            return sup;
        }
    }
}