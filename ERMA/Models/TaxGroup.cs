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
    public class TaxGroup
    {
        private string _code = string.Empty;
        private string _description = string.Empty;

        public string Code { get => _code; set => _code = value; }
        public string Description { get => _description; set => _description = value; }

        public string ListDisplay { get => _code + " - " + _description; }
    }

    public class TaxGroupRepository
    {
        public List<TaxGroup> GetTaxGroupList()
        {
            List<TaxGroup> lstReturn = new List<TaxGroup>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();

            string sSQL = SQL.GetTaxGroupList(strSchema);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TaxGroup tg = new TaxGroup();
                        tg.Code = dr.ItemArray[0].ToString().Trim();
                        tg.Description = dr.ItemArray[1].ToString().Trim();

                        lstReturn.Add(tg);
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