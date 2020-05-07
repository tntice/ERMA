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
    public class TaxRateDetail
    {
        private string _taxGroup = string.Empty;
        private string _taxCode = string.Empty;
        private decimal _taxRate = 0;

        public string TaxGroup { get => _taxGroup; set => _taxGroup = value; }
        public string TaxCode { get => _taxCode; set => _taxCode = value; }
        public decimal TaxRate { get => _taxRate; set => _taxRate = value; }

        public TaxRateDetail() { }

        public TaxRateDetail(string taxGroup, string taxCode, decimal taxRate)
        {
            _taxGroup = taxGroup;
            _taxCode = taxCode;
            _taxRate = taxRate;

        }
    }

    public class TaxRateDetailRepository 
    {
        public TaxRateDetail getTaxRate(string taxGroup, string taxCode)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TaxRateDetail trd = new TaxRateDetail();
            string sSQL = SQL.GetTaxRate(taxGroup, taxCode, strSchema);

            TACDataRowType dr = Data.GetTACDataRow(sSQL, cCon);

            try
            {
                if(dr.HasData)
                {
                    trd.TaxGroup = taxGroup;
                    trd.TaxCode = taxCode;
                    trd.TaxRate = Convert.ToDecimal(dr.Drow.ItemArray[2].ToString());
                }
                else
                {
                    trd.TaxGroup = taxGroup;
                    trd.TaxCode = taxCode;
                    trd.TaxRate = 0;
                }
            }
            catch
            {

            }
            return trd;            
        }

        public decimal getTaxRateValue(string taxGroup, string taxCode)
        {
            TaxRateDetail trd = getTaxRate(taxGroup, taxCode);
            decimal dReturnVal = 0;

            dReturnVal = trd.TaxRate / 100;

            return dReturnVal;
        }

    }


}