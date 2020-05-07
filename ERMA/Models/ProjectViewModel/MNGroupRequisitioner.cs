using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Code;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;
using System.Data.Odbc;
using System.IO;

namespace ERMA.Models.ProjectViewModel
{
    public class MNGroupRequisitioner
    {
        private Int32 _MNPRGID = 0;
        private Int32 _PGID = 0;
        private String _UserID = string.Empty;

        private ProjectGroup _selectedProjectGroup = new ProjectGroup();
        private Requisitioner _selectedRequisitioner = new Requisitioner();

        private string _createdBy = string.Empty;
        private DateTime _createdDate = DateTime.Now;
        private string _updatedBy = string.Empty;
        private DateTime _updatedDate = DateTime.Now;

        public int MNPRGID { get => _MNPRGID; set => _MNPRGID = value; }
        public int PGID { get => _PGID; set => _PGID = value; }
        public string UserID { get => _UserID; set => _UserID = value; }
        public ProjectGroup SelectedProjectGroup { get => _selectedProjectGroup; set => _selectedProjectGroup = value; }
        public Requisitioner SelectedRequisitioner { get => _selectedRequisitioner; set => _selectedRequisitioner = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string UpdatedBy { get => _updatedBy; set => _updatedBy = value; }
        public DateTime UpdatedDate { get => _updatedDate; set => _updatedDate = value; }

        public MNGroupRequisitioner() { }
    }

    public class MNGroupRequisitionerRepository
    {
        public List<MNGroupRequisitioner> GetMNGroupRequisitionerByPGID(string PGID)
        {
            List<MNGroupRequisitioner> returnList = new List<MNGroupRequisitioner>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();

            string strSQL = SQL.GetMNGroupRequisitionerByPGID(strSchema, PGID);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if(dt.HasData)
                {
                    foreach(DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupRequisitioner grReq = new MNGroupRequisitioner();

                        grReq.MNPRGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        grReq.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(grReq.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            grReq.SelectedProjectGroup.PGID = grReq.PGID;
                            grReq.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            grReq.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            grReq.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            grReq.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            grReq.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            grReq.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        grReq.UserID = dr.ItemArray[3].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetRequisitionerByUserName(strSchema, grReq.UserID);
                        TACDataRowType dr3;
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if(dr3.HasData)
                        {
                            grReq.SelectedRequisitioner.ID = dr3.Drow.ItemArray[0].ToString();
                            grReq.SelectedRequisitioner.UserName = dr3.Drow.ItemArray[1].ToString().ToUpper().Trim();
                            grReq.SelectedRequisitioner.Description = dr3.Drow.ItemArray[2].ToString().ToUpper().Trim();
                        }

                        grReq.CreatedBy = dr.ItemArray[5].ToString().ToUpper().Trim();
                        grReq.CreatedDate = Convert.ToDateTime(dr.ItemArray[6].ToString());
                        grReq.UpdatedBy = dr.ItemArray[7].ToString().ToUpper().Trim();
                        grReq.UpdatedDate = Convert.ToDateTime(dr.ItemArray[8].ToString());

                        returnList.Add(grReq);
                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupRequisitioner> GetMNGroupRequisitionerByPGIDAndUserID(string PGID, string UserID)
        {
            List<MNGroupRequisitioner> returnList = new List<MNGroupRequisitioner>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();

            string strSQL = SQL.GetMNGroupRequisitionerByPGIDAndUser(strSchema, PGID, UserID);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if(dt.HasData)
                {
                    foreach(DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupRequisitioner grReq = new MNGroupRequisitioner();

                        grReq.MNPRGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        grReq.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(grReq.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            grReq.SelectedProjectGroup.PGID = grReq.PGID;
                            grReq.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            grReq.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            grReq.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            grReq.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            grReq.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            grReq.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        grReq.UserID = dr.ItemArray[3].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetRequisitionerByUserName(strSchema, grReq.UserID);
                        TACDataRowType dr3;
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if(dr3.HasData)
                        {
                            grReq.SelectedRequisitioner.ID = dr3.Drow.ItemArray[0].ToString();
                            grReq.SelectedRequisitioner.UserName = dr3.Drow.ItemArray[1].ToString().ToUpper().Trim();
                            grReq.SelectedRequisitioner.Description = dr3.Drow.ItemArray[2].ToString().ToUpper().Trim();
                        }

                        grReq.CreatedBy = dr.ItemArray[5].ToString().ToUpper().Trim();
                        grReq.CreatedDate = Convert.ToDateTime(dr.ItemArray[6].ToString());
                        grReq.UpdatedBy = dr.ItemArray[7].ToString().ToUpper().Trim();
                        grReq.UpdatedDate = Convert.ToDateTime(dr.ItemArray[8].ToString());

                        returnList.Add(grReq);
                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupRequisitioner> GetMNGroupRequisitionerByUserIDWithoutPGID(string PGID, string UserID)
        {
            List<MNGroupRequisitioner> returnList = new List<MNGroupRequisitioner>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();

            string strSQL = SQL.GetMNGroupRequisitionerAndUserWithoutPGID(strSchema, PGID, UserID);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupRequisitioner grReq = new MNGroupRequisitioner();

                        grReq.MNPRGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        grReq.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(grReq.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            grReq.SelectedProjectGroup.PGID = grReq.PGID;
                            grReq.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            grReq.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            grReq.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            grReq.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            grReq.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            grReq.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        grReq.UserID = dr.ItemArray[3].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetRequisitionerByUserName(strSchema, grReq.UserID);
                        TACDataRowType dr3;
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if (dr3.HasData)
                        {
                            grReq.SelectedRequisitioner.ID = dr3.Drow.ItemArray[0].ToString();
                            grReq.SelectedRequisitioner.UserName = dr3.Drow.ItemArray[1].ToString().ToUpper().Trim();
                            grReq.SelectedRequisitioner.Description = dr3.Drow.ItemArray[2].ToString().ToUpper().Trim();
                        }

                        grReq.CreatedBy = dr.ItemArray[5].ToString().ToUpper().Trim();
                        grReq.CreatedDate = Convert.ToDateTime(dr.ItemArray[6].ToString());
                        grReq.UpdatedBy = dr.ItemArray[7].ToString().ToUpper().Trim();
                        grReq.UpdatedDate = Convert.ToDateTime(dr.ItemArray[8].ToString());

                        returnList.Add(grReq);
                    }
                }
            }
            catch { }

            return returnList;

        }

        public List<MNGroupRequisitioner> GetMNGroupRequisitionerByMNPRGID(string MNPRGID)
        {
            List<MNGroupRequisitioner> returnList = new List<MNGroupRequisitioner>();
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataTableType dt = new TACDataTableType();

            string strSQL = SQL.GetMNGroupRequisitionerByMNPRGID(strSchema, MNPRGID);

            try
            {
                dt = Data.GetTACDataRows(strSQL, cCon);
                if (dt.HasData)
                {
                    foreach (DataRow dr in dt.Dtable.Rows)
                    {
                        MNGroupRequisitioner grReq = new MNGroupRequisitioner();

                        grReq.MNPRGID = Convert.ToInt32(dr.ItemArray[0].ToString());
                        grReq.PGID = Convert.ToInt32(dr.ItemArray[1].ToString());

                        string strSQL2 = SQL.GetProjectGroupByID(grReq.PGID);
                        TACDataRowType dt2 = new TACDataRowType();

                        dt2 = Data.GetTACDataRow(strSQL2, cCon);
                        if (dt2.HasData)
                        {
                            grReq.SelectedProjectGroup.PGID = grReq.PGID;
                            grReq.SelectedProjectGroup.Name = dt2.Drow.ItemArray[1].ToString();
                            grReq.SelectedProjectGroup.Status = dt2.Drow.ItemArray[2].ToString();
                            grReq.SelectedProjectGroup.CreatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[3].ToString());
                            grReq.SelectedProjectGroup.CreatedBy = dt2.Drow.ItemArray[4].ToString().ToUpper();
                            grReq.SelectedProjectGroup.UpdatedDate = Convert.ToDateTime(dt2.Drow.ItemArray[5].ToString());
                            grReq.SelectedProjectGroup.UpdatedBy = dt2.Drow.ItemArray[6].ToString().ToUpper();

                        }
                        grReq.UserID = dr.ItemArray[3].ToString().ToUpper().Trim();

                        string strSQL3 = SQL.GetRequisitionerByUserName(strSchema, grReq.UserID);
                        TACDataRowType dr3;
                        dr3 = Data.GetTACDataRow(strSQL3, cCon);
                        if (dr3.HasData)
                        {
                            grReq.SelectedRequisitioner.ID = dr3.Drow.ItemArray[0].ToString();
                            grReq.SelectedRequisitioner.UserName = dr3.Drow.ItemArray[1].ToString().ToUpper().Trim();
                            grReq.SelectedRequisitioner.Description = dr3.Drow.ItemArray[2].ToString().ToUpper().Trim();
                        }

                        grReq.CreatedBy = dr.ItemArray[5].ToString().ToUpper().Trim();
                        grReq.CreatedDate = Convert.ToDateTime(dr.ItemArray[6].ToString());
                        grReq.UpdatedBy = dr.ItemArray[7].ToString().ToUpper().Trim();
                        grReq.UpdatedDate = Convert.ToDateTime(dr.ItemArray[8].ToString());

                        returnList.Add(grReq);
                    }
                }
            }
            catch { }

            return returnList;

        }

        public Int32 Insert(MNGroupRequisitioner grReq)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            grReq.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            grReq.UpdatedBy = grReq.CreatedBy;

            Int32 returnValue = 0;

            string strSQL = SQL.InsertMNGroupRequisitioner(grReq);
            TACNonDataType ndt = new TACNonDataType();

            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }
            }
            catch { }

            return returnValue;
        }

        public Int32 Delete(string MNPRGID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            Int32 returnValue = 0;

            string strSQL = SQL.DeleteMNGroupRequisitioner(MNPRGID);

            TACNonDataType ndt = new TACNonDataType();
            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public Int32 Delete(Int32 PGID)
        {
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            Int32 returnValue = 0;

            string strSQL = SQL.DeleteMNGroupRequisitionerByPGID(PGID.ToString());

            TACNonDataType ndt = new TACNonDataType();
            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
                else
                {
                    returnValue = -1;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }
    }
}