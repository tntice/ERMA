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
    public class TaxRate
    {
        private string _code = string.Empty;
        private string _description = string.Empty;
        private string _taxGroup = string.Empty;


        public string Code { get => _code; set => _code = value; }
        public string Description { get => _description; set => _description = value; }

        public string ListDisplay { get => _code + " - " + _description; }
        public string TaxGroup { get => _taxGroup; set => _taxGroup = value; }
    }

    public class TaxRateRepository
    {
        public List<TaxRate> GetTaxRateList(string TaxGroup)
        {
            List<TaxRate> lstReturn = new List<TaxRate>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();

            string sSQL = SQL.GetTaxRateList(TaxGroup, strSchema);

            try
            {

                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TaxRate tr = new TaxRate();
                        tr.TaxGroup = dr.ItemArray[0].ToString().Trim();
                        tr.Code = dr.ItemArray[1].ToString().Trim();
                        tr.Description = dr.ItemArray[2].ToString().Trim();

                        lstReturn.Add(tr);
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