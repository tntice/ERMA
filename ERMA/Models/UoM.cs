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
    public class UoM
    {
        private string _code = string.Empty;
        private string _description = string.Empty;

        public string Code { get => _code; set => _code = value; }
        public string Description { get => _description; set => _description = value; }

        public string ListDisplay
        {
            get => _code + " - " + _description;
        }
    }

    public class UoMRepository
    {
        public List<UoM> GetUoMList()
        {
            List<UoM> lstReturn = new List<UoM>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();

            string sSQL = SQL.GetUoMList(strSchema);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UoM um = new UoM();
                        um.Code = dr.ItemArray[0].ToString().Trim();
                        um.Description = dr.ItemArray[1].ToString().Trim();

                        lstReturn.Add(um);
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