using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERMA.Code;
using ERMA.App_Code;
using ERMA.Models;
using System.Data;
using System.ComponentModel.DataAnnotations;
using TACData;
using TACUtility;
using System.Data.Odbc;
using System.IO;

namespace ERMA.Models.ProjectViewModel
{
    public class CMSPermission
    {
        private string _projectID = string.Empty;
        private string _requisitionerID = string.Empty;
        private string _level = string.Empty;
        private string _createdUserID = string.Empty;
        private string _createdDate = string.Empty;
        private string _createdTime = string.Empty;
        private string _updatedUserID = string.Empty;
        private string _updatedDate = string.Empty;
        private string _updatedTime = string.Empty;

        public string ProjectID { get => _projectID; set => _projectID = value; }
        public string RequisitionerID { get => _requisitionerID; set => _requisitionerID = value; }
        public string Level { get => _level; set => _level = value; }
        public string CreatedUserID { get => _createdUserID; set => _createdUserID = value; }
        public string CreatedDate { get => _createdDate; set => _createdDate = value; }
        public string CreatedTime { get => _createdTime; set => _createdTime = value; }
        public string UpdatedUserID { get => _updatedUserID; set => _updatedUserID = value; }
        public string UpdatedDate { get => _updatedDate; set => _updatedDate = value; }
        public string UpdatedTime { get => _updatedTime; set => _updatedTime = value; }

        public CMSPermission() { }

    }

    public class CMSPermissionRepository
    {
        public CMSPermission GetCMSPermission(string ProjectID, string Requisitioner)
        {
            CMSPermission CMS = new CMSPermission();

            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataRowType dr = new TACDataRowType();

            string strSQL = SQL.GetCMSPermissions(strSchema, ProjectID, Requisitioner);

            try
            {
                dr = Data.GetTACDataRow(strSQL, cCon);
                if(dr.HasData)
                {
                    CMS.ProjectID = ProjectID;
                    CMS.RequisitionerID = Requisitioner;
                    CMS.Level = dr.Drow.ItemArray[2].ToString();
                    CMS.CreatedUserID = dr.Drow.ItemArray[3].ToString();
                    CMS.CreatedDate = dr.Drow.ItemArray[4].ToString();
                    CMS.CreatedTime = dr.Drow.ItemArray[5].ToString();
                    CMS.UpdatedUserID = dr.Drow.ItemArray[6].ToString();
                    CMS.UpdatedDate = dr.Drow.ItemArray[7].ToString();
                    CMS.UpdatedTime = dr.Drow.ItemArray[8].ToString();
                }
            }
            catch
            {

            }
            return CMS;

        }

        public Int32 DeleteCMSPermission(string ProjectID, string Requisitioner)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACNonDataType ndt = new TACNonDataType();

            string strSQL = SQL.DeleteCMSPermissions(strSchema, ProjectID, Requisitioner);

            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if (ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public Int32 CMSPermissionHasData(string ProjectID, string Requisitioner)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();

            TACDataRowType dr = new TACDataRowType();

            string strSQL = SQL.GetCMSPermissions(strSchema, ProjectID, Requisitioner);

            try
            {
                dr = Data.GetTACDataRow(strSQL, cCon);
                if(dr.HasData)
                {
                    returnValue = 1;
                }
                else
                {
                    returnValue = 0;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }

        public Int32 InsertCMSPermission(string ProjectID, string Requisitioner)
        {
            Int32 returnValue = 0;
            string cCon = ERMA.Properties.Settings.Default.WOW.ToString();
            string strSchema = ERMA.Properties.Settings.Default.DB2Staging.ToString();
            string LoginUserID = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().ToUserIDWithoutDomain().ToUpper();
            //LoginUserID = Session[SessionName.UserName.ToString()].ToString();

            TACNonDataType ndt = new TACNonDataType();

            CMSPermission CMS = new CMSPermission();
            CMS.ProjectID = ProjectID;
            CMS.RequisitionerID = Requisitioner;
            CMS.Level = "1";
            CMS.CreatedUserID = LoginUserID;
            CMS.CreatedDate = DateTime.Now.ToString().ToDateWithDashes();
            CMS.CreatedTime = DateTime.Now.ToTimeWithDots();
            CMS.UpdatedUserID = CMS.CreatedUserID;
            CMS.UpdatedDate = CMS.CreatedDate;
            CMS.UpdatedTime = CMS.CreatedTime;

            string strSQL = SQL.InsertCMSPermissions(CMS);
            try
            {
                ndt = Data.nonTACQuery(strSQL, cCon);
                if(ndt.IsSuccessful)
                {
                    returnValue = ndt.CountAffected;
                }
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;

        }
    }

    public class CMSMethods
    {

        CMSPermissionRepository CMSPermRepository = new CMSPermissionRepository();
        MNGroupProjectRepository MNGrpPrjRepository = new MNGroupProjectRepository();
        MNGroupRequisitionerRepository MNGrpReqRepository = new MNGroupRequisitionerRepository();
        ProjectGroupRepository prjGrpRepository = new ProjectGroupRepository();

        public void AddSinglePermissionByProject(string PGID, string ProjectID)
        {
            List<MNGroupRequisitioner> mnGroupReqs = new List<MNGroupRequisitioner>();

            mnGroupReqs = MNGrpReqRepository.GetMNGroupRequisitionerByPGID(PGID);

            foreach(MNGroupRequisitioner requer in mnGroupReqs)
            {
                Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(ProjectID, requer.UserID);

                if(iCountPermission < 1)
                {
                    Int32 iCountAddPermission = CMSPermRepository.InsertCMSPermission(ProjectID, requer.UserID);
                }
            }
        }

        public void AddSinglePermissionByRequisitioner(string PGID, string RequisitionerID)
        {
            List<MNGroupProject> mnGroupProjs = new List<MNGroupProject>();

            mnGroupProjs = MNGrpPrjRepository.GetMNGroupProjectByPGID(PGID);

            foreach(MNGroupProject grpPrj in mnGroupProjs)
            {
                Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(grpPrj.ProjectID, RequisitionerID);

                if(iCountPermission < 1)
                {
                    Int32 iCountAddPermission = CMSPermRepository.InsertCMSPermission(grpPrj.ProjectID, RequisitionerID);
                }
            }
        }

        public void AddAllPermissions(string PGID)
        {
            List<MNGroupRequisitioner> mnGroupReqs = new List<MNGroupRequisitioner>();
            List<MNGroupProject> mnGroupProjs = new List<MNGroupProject>();

            mnGroupReqs = MNGrpReqRepository.GetMNGroupRequisitionerByPGID(PGID);
            mnGroupProjs = MNGrpPrjRepository.GetMNGroupProjectByPGID(PGID);

            foreach (MNGroupRequisitioner requer in mnGroupReqs)
            {
                foreach (MNGroupProject grpPrj in mnGroupProjs)
                {
                    Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(grpPrj.ProjectID, requer.UserID);

                    if(iCountPermission < 1)
                    {
                        Int32 iCountAddPermission = CMSPermRepository.InsertCMSPermission(grpPrj.ProjectID, requer.UserID);
                    }
                }
            }

        }

        public void RemoveSinglePermissionByProject(string PGID, string ProjectID)
        {
            Int32 iCount = 0;
            List<MNGroupRequisitioner> mnGroupReqs = new List<MNGroupRequisitioner>();
            mnGroupReqs = MNGrpReqRepository.GetMNGroupRequisitionerByPGID(PGID);

            List<MNGroupProject> mnGroupProj = new List<MNGroupProject>();
            mnGroupProj = MNGrpPrjRepository.GetMNGroupProjectByProjNumWithoutPGID(PGID, ProjectID);

            foreach (MNGroupRequisitioner requer in mnGroupReqs)
            {
                iCount = 0;
                foreach(MNGroupProject grpPrj in mnGroupProj)
                {
                    List<MNGroupRequisitioner> mnGroupReqsCopy = new List<MNGroupRequisitioner>();
                    mnGroupReqsCopy = MNGrpReqRepository.GetMNGroupRequisitionerByUserIDWithoutPGID(PGID, requer.UserID);

                    if(mnGroupReqsCopy.Count > 0)
                    {
                        iCount++;  //Duplicate found.  Can't delete if it exists in another project group
                    }
                }
                if(iCount == 0)
                {
                    Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(ProjectID, requer.UserID);

                    if (iCountPermission > 0)
                    {
                        Int32 iCountDeletePermission = CMSPermRepository.DeleteCMSPermission(ProjectID, requer.UserID);
                    }
                }
            }
        }

        public void RemoveSinglePermissionByRequisitioner(string PGID, string RequisitionerID)
        {
            Int32 iCount = 0;
            List<MNGroupProject> mnGroupProjs = new List<MNGroupProject>();
            mnGroupProjs = MNGrpPrjRepository.GetMNGroupProjectByPGID(PGID);  //All Projects in the current Project Group

            List<MNGroupRequisitioner> mnGroupReqrs = new List<MNGroupRequisitioner>();
            mnGroupReqrs = MNGrpReqRepository.GetMNGroupRequisitionerByUserIDWithoutPGID(PGID, RequisitionerID); //All Requisitioners outside this project group

            foreach(MNGroupProject grpPrj in mnGroupProjs)
            {
                iCount = 0;
                foreach(MNGroupRequisitioner grpReq in mnGroupReqrs) //All Requisitioners outside this Project Group that has this requisitionerID
                {
                    List<MNGroupProject> mnGrpPrjCopy = new List<MNGroupProject>();
                    mnGrpPrjCopy = MNGrpPrjRepository.GetMNGroupProjectByProjNumWithoutPGID(PGID, grpPrj.ProjectID); //All Projects outside this Project Group
                    if(mnGrpPrjCopy.Count > 0)
                    {
                        iCount++;
                    }
                }
                if(iCount == 0)
                {
                    Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(grpPrj.ProjectID, RequisitionerID);
                    if(iCountPermission > 0)
                    {
                        Int32 iCountRemovePermission = CMSPermRepository.DeleteCMSPermission(grpPrj.ProjectID, RequisitionerID);
                    }
                }
            }
        }

        public void RemoveAllPermissions(string PGID)
        {
            Int32 iCount = 0;
            Int32 iCount2 = 0;

            List<MNGroupProject> mnGroupProjs = new List<MNGroupProject>();
            mnGroupProjs = MNGrpPrjRepository.GetMNGroupProjectByPGID(PGID);  //All Projects in the current Project Group

            List<MNGroupRequisitioner> mnGroupReqs = new List<MNGroupRequisitioner>();
            mnGroupReqs = MNGrpReqRepository.GetMNGroupRequisitionerByPGID(PGID);
            try
            {


                foreach (MNGroupProject gp in mnGroupProjs)
                {
                    foreach (MNGroupRequisitioner gr in mnGroupReqs)
                    {
                        //Loop Requisitioners
                        List<MNGroupRequisitioner> mnGroupReqrsOutside = new List<MNGroupRequisitioner>();
                        mnGroupReqrsOutside = MNGrpReqRepository.GetMNGroupRequisitionerByUserIDWithoutPGID(PGID, gr.UserID); //All Requisitioners outside this project group
                        iCount = 0;
                        foreach (MNGroupRequisitioner grpReq in mnGroupReqrsOutside)
                        {
                            List<MNGroupProject> mnGrpPrjCopy = new List<MNGroupProject>();
                            mnGrpPrjCopy = MNGrpPrjRepository.GetMNGroupProjectByProjNumWithoutPGID(PGID, gp.ProjectID);
                            if (mnGrpPrjCopy.Count > 0)
                            {
                                iCount++;
                            }
                        }
                        if (iCount == 0)
                        {
                            Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(gp.ProjectID, gr.UserID);
                            if (iCountPermission > 0)
                            {
                                Int32 iCountRemovePermission = CMSPermRepository.DeleteCMSPermission(gp.ProjectID, gr.UserID);
                            }
                        }

                        //Loop Projects
                        List<MNGroupProject> mnGroupPrjOutside = new List<MNGroupProject>();
                        mnGroupPrjOutside = MNGrpPrjRepository.GetMNGroupProjectByProjNumWithoutPGID(PGID, gp.ProjectID); //All Projects ouside Project Group
                        iCount2 = 0;
                        foreach (MNGroupProject grpPrj in mnGroupPrjOutside)
                        {
                            List<MNGroupRequisitioner> mnGrpReqCopy = new List<MNGroupRequisitioner>();
                            mnGrpReqCopy = MNGrpReqRepository.GetMNGroupRequisitionerByUserIDWithoutPGID(PGID, gr.UserID);
                            if (mnGrpReqCopy.Count > 0)
                            {
                                iCount2++;
                            }
                        }
                        if (iCount2 == 0)
                        {
                            Int32 iCountPermission = CMSPermRepository.CMSPermissionHasData(gp.ProjectID, gr.UserID);
                            if (iCountPermission > 0)
                            {
                                Int32 iCountRemovePermission = CMSPermRepository.DeleteCMSPermission(gp.ProjectID, gr.UserID);
                            }
                        }

                    }
                }
            }catch(Exception ex)
            {
                string message = ex.Message;
            }
        }

    }
}