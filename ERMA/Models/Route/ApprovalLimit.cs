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
    public class ApprovalLimitRepository
    {
        public Int32 GetManagerLimit()
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetManagerApprovalLimit();
            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if(drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch { }
            return returnValue;
        }

        public Int32 GetApprovalLimit(string JobClass, string Title)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetApprovalLimit(JobClass, Title);
            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch { }
            return returnValue;
        }

        public Int32 GetNextSupervisorApprovalLimit(string EmployeeNumber)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType drt = new TACDataRowType();
            string strSQL = SQL.GetNextSupervisorApprovalLimit(EmployeeNumber);
            try
            {
                drt = Data.GetTACDataRow(strSQL, cCon);

                if (drt.HasData)
                {
                    returnValue = Convert.ToInt32(drt.Drow.ItemArray[0].ToString());
                }
            }
            catch { }
            return returnValue;
        }

    }
}