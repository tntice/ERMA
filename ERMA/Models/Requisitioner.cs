using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Code;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;

namespace ERMA.Models
{
    public class Requisitioner
    {

        private string _ID = string.Empty;
        private string _userName = string.Empty;
        private string _Description = string.Empty;

        public string ID { get => _ID; set => _ID = value; }

        public string Description { get => _Description; set => _Description = value; }

        public string UserName { get => _userName; set => _userName = value; }

        public Requisitioner() { }

    }

    public class RequisitionerRepository
    {
        public Requisitioner GetRequisitionerByID(string UserName)
        {
            Requisitioner rq = new Requisitioner();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            
            string strSQL = SQL.GetBuyer(UserName, strSchema);
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if(!(dr is null))
                {
                    rq.ID = dr.ItemArray[0].ToString().Trim();
                    rq.Description = dr.ItemArray[1].ToString().Trim().ToUpper();
                }
            }
            catch
            {
            }
            return rq;

        }

        public Requisitioner GetRequisitionerByUserName(string UserName)
        {
            Requisitioner rq = new Requisitioner();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitionerByUserName(strSchema, UserName);
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if(!(dr is null))
                {
                    rq.ID = dr.ItemArray[0].ToString().Trim();
                    rq.UserName = dr.ItemArray[1].ToString().ToUpper().Trim();
                    rq.Description = dr.ItemArray[2].ToString().ToUpper().Trim();
                }
            }
            catch
            {

            }
            return rq;
        }

        public Requisitioner GetRequisitionerByEmpNum(string EmpNum)
        {
            Requisitioner rq = new Requisitioner();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitionerInfoByEmpNum(strSchema, EmpNum);
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    rq.ID = dr.ItemArray[0].ToString().Trim();
                    rq.UserName = dr.ItemArray[1].ToString().ToUpper().Trim();
                    rq.Description = dr.ItemArray[2].ToString().ToUpper().Trim();
                }
            }
            catch
            {

            }
            return rq;
        }

        public List<Requisitioner> GetAllRequisitioners()
        {
            List<Requisitioner> reqrs = new List<Requisitioner>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitioners(strSchema);

            DataTable dt = new DataTable();
            try
            {
                dt = Data.GetDataRows(strSQL, cCon);
                if(dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Requisitioner rq = new Requisitioner();

                        rq.ID = dr.ItemArray[0].ToString().Trim();
                        rq.UserName = dr.ItemArray[1].ToString().ToUpper().Trim();
                        rq.Description = dr.ItemArray[2].ToString().ToUpper().Trim();

                        reqrs.Add(rq);
                    }
                }
            }
            catch { }
            return reqrs;
        }

    }
}