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
using System.Net.Mail;


namespace ERMA.Models
{
    public class Person
    {
        private string _ID = string.Empty;
        private string _userName = string.Empty;
        private string _fullName = string.Empty;
        private string _email = string.Empty;


        public string EmpNum { get => _ID; set => _ID = value; }

        public string FullName { get => _fullName; set => _fullName = value; }

        public string UserName { get => _userName; set => _userName = value; }

        public string Email
        {
            get
            {
                if (_email.Length > 0)
                {
                    return _email;
                }
                else
                {
                    return string.Format("{0}@TOYOTOMIAM.COM", _userName.ToUpper().Trim());
                }
            }
            set => _email = value;
        }

        public Person() { }

    }

    public class PersonRepository
    {
        public Person GetPersonByUserName(string UserName)
        {
            Person per = new Person();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitionerByUserName(strSchema, UserName); //All users in system must be in the Requisitioner Tables
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    per.EmpNum = dr.ItemArray[0].ToString().Trim();
                    per.UserName = dr.ItemArray[1].ToString().Trim().ToUpper();
                    per.FullName = dr.ItemArray[2].ToString().Trim().ToUpper();
                }
            }
            catch
            {
            }
            return per;

        }
        public Person GetPersonByEmpNum(string EmpNum)
        {
            Person per = new Person();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitionerInfoByEmpNum(strSchema, EmpNum);
            DataRow dr;

            try
            {
                dr = Data.GetDataRow(strSQL, cCon);
                if (!(dr is null))
                {
                    per.EmpNum = dr.ItemArray[0].ToString().Trim();
                    per.UserName = dr.ItemArray[1].ToString().ToUpper().Trim();
                    per.FullName = dr.ItemArray[2].ToString().ToUpper().Trim();
                }
            }
            catch
            {

            }
            return per;
        }
        public List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            string strSQL = SQL.GetRequisitioners(strSchema);

            DataTable dt = new DataTable();
            try
            {
                dt = Data.GetDataRows(strSQL, cCon);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Person per = new Person();

                        per.EmpNum = dr.ItemArray[0].ToString().Trim();
                        per.UserName = dr.ItemArray[1].ToString().ToUpper().Trim();
                        per.FullName = dr.ItemArray[2].ToString().ToUpper().Trim();

                        persons.Add(per);
                    }
                }
            }
            catch { }
            return persons;
        }

    }
}