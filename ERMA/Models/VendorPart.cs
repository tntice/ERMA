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
    public class VendorPart
    {
        private string _vendorCode = string.Empty;
        private string _partNumber = string.Empty;
        private string _description = string.Empty;

        public string VendorCode { get => _vendorCode; set => _vendorCode = value; }
        public string PartNumber { get => _partNumber; set => _partNumber = value; }
        public string Description { get => _description; set => _description = value; }
    }

    public class VendorPartRepository
    {
        public List<VendorPart> GetVendorPartList(string filter, string VendorCode)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            
            DataTable dt = new DataTable();
            List<VendorPart> vendorParts = new List<VendorPart>();

            string sSQL = SQL.GetVendorPartList(filter, VendorCode, strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);
                
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        VendorPart vp = new VendorPart();
                        vp.VendorCode = dr.ItemArray[0].ToString().Trim();
                        vp.PartNumber = dr.ItemArray[1].ToString().Trim();
                        vp.Description = dr.ItemArray[2].ToString().Trim();
                        
                        vendorParts.Add(vp);
                    }
                }
            }
            catch
            {

            }
            return vendorParts;

        }
        public VendorPart GetVendorPart(string VendorPart, string VendorCode)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();

            TACDataRowType dr = new TACDataRowType();
            string sSQL = SQL.GetVendorPart(VendorPart, VendorCode, strSchema);
            VendorPart vp = new VendorPart();

            try
            {
                dr = Data.GetTACDataRow(sSQL, cCon);
                if (dr.HasData)
                {
                    
                    vp.VendorCode = dr.Drow.ItemArray[0].ToString().Trim();
                    vp.PartNumber = dr.Drow.ItemArray[1].ToString().Trim();
                    string desc1 = dr.Drow.ItemArray[2].ToString().Trim();
                    string desc2 = dr.Drow.ItemArray[3].ToString().Trim();
                    string desc3 = dr.Drow.ItemArray[4].ToString().Trim();

                    string genDesc = desc1;
                    if (desc2.Length > 0) { genDesc += Environment.NewLine + desc2; }
                    if (desc3.Length > 0) { genDesc += Environment.NewLine + desc3; }

                    vp.Description = genDesc;
                }
            }
            catch { }

            return vp;
        }
    }

}