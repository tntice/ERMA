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
    public class GLAccount
    {
        private decimal _number = 0;
        private string _code = string.Empty;
        private string _description = string.Empty;
        private string _title = string.Empty;

        public decimal Number { get => _number; set => _number = value; }
        public string Code { get => _code; set => _code = value; }
        public string Description { get => _description; set => _description = value; }
        public string Title { get => _title; set => _title = value; }

        public GLAccount() { }

        public GLAccount(decimal number, string code, string description, string title)
        {
            _number = number;
            _code = code;
            _description = description;
            _title = title;
        }
    }

    public class GLAccountRepository
    {


        public List<GLAccount> GetGLAccountList(string UserName)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Database.ToString();
            strSchema = "TOYOTOMI";

            DataTable dt = new DataTable();
            List<GLAccount> GLAccounts = new List<GLAccount>();

            string sSQL = SQL.GetGLAccounts(UserName.ToUpper(), strSchema);

            try
            {
                dt = Data.GetDataRows(sSQL, cCon);

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GLAccount gl = new GLAccount();

                        gl.Number = Convert.ToDecimal(dr.ItemArray[0].ToString());
                        gl.Description =  gl.Number + " - " + dr.ItemArray[2].ToString();
                        gl.Title = dr.ItemArray[2].ToString().Trim();

                        GLAccounts.Add(gl);                      
                    }
                }
            }
            catch
            {

            }
            return GLAccounts;

        }
    }
}