using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using ERMA.Models;
using ERMA.Models.ProjectViewModel;
using ERMA.Models.Route;
using TACUtility;


namespace ERMA.Code
{
    public static class SQL
    {
        #region Parts

        public static string GetBuyer(string username, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT AU8REQR, AU8USID ");
            sb.Append("FROM " + schema.Trim() + ".REQUID ");
            sb.Append("WHERE AU8USID = '" + username.Trim().ToUpper() + "' ");

            return sb.ToString();
        }

        public static string GetVendorList(string filter, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT BTNAME, BTSTAT, BTVEND, BTTAXG, BTRMIT ");
            sb.Append("FROM " + schema.Trim() + ".VEND ");
            sb.Append("WHERE BTSTAT = 'A' ");
            sb.Append("AND UPPER(BTNAME) LIKE ('%" + filter + "%') ");
            sb.Append("ORDER BY BTNAME, BTVEND ");

            return sb.ToString();
        }

        public static string GetVendorPartList(string filter, string VendorNumber, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT JRVND#, JRVPT#, JRDES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPTVN ");
            sb.Append("WHERE JRVND# = '" + VendorNumber + "' ");
            sb.Append("AND TRIM(UPPER(JRVPT#)) LIKE ('%" + filter.ToUpper().Trim() + "%') ");
            sb.Append("ORDER BY JRVPT# ");

            return sb.ToString();
        }

        public static string GetVendorPart(string PartNumber, string VendorNumber, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT JRVND#, JRVPT#, JRDES1, JRDES2, JRDES3 ");
            sb.Append("FROM " + schema.Trim() + ".POPTVN ");
            sb.Append("WHERE JRVND# = '" + VendorNumber + "' ");
            if (!(PartNumber is null))
            {
                sb.Append("AND TRIM(UPPER(JRVPT#)) = '" + PartNumber.ToUpper().Trim() + "' ");
            }
            return sb.ToString();
        }
        
        public static string GetVendor(string VendorCode, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT BTNAME, BTSTAT, BTVEND, BTTAXG, BTRMIT, BTADR1, BTADR2, BTADR3, BTPOST ");
            sb.Append("FROM " + schema.Trim() + ".VEND ");
            sb.Append("WHERE BTSTAT = 'A' ");
            if (!(VendorCode is null))
            {
                sb.Append("AND UPPER(BTVEND) = '" + VendorCode.ToUpper() + "' ");
            }
            return sb.ToString();
        }
        
        public static string GetGLAccounts(string UserName, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT BR8ACCN AS ACCOUNT, BR8CODE AS DEPTCODE, AZTITL ");
            sb.Append("FROM " + schema.Trim() + ".GPRDL ");
            sb.Append("LEFT JOIN " + schema.Trim() + ".GPRA on BR6CODE=BR8CODE ");
            sb.Append("LEFT JOIN " + schema.Trim() + ".MAST ");
            sb.Append("ON BR8ACCN = AZCODE ");
            sb.Append("WHERE TRIM(BR6PGMN)='PO006A' ");
            sb.Append("AND UPPER(BR6USER) = '" + UserName.Trim().ToUpper() + "' ");
            sb.Append("UNION ");
            sb.Append("SELECT BR9ACCN AS ACCOUNT, BR9CODE AS DEPTCODE, AZTITL ");
            sb.Append("FROM " + schema.Trim() + ".GPRDN ");
            sb.Append("LEFT JOIN " + schema.Trim() + ".GPRA on BR6CODE=BR9CODE ");
            sb.Append("LEFT JOIN " + schema.Trim() + ".MAST ");
            sb.Append("ON BR9ACCN = AZCODE ");
            sb.Append("WHERE TRIM(BR6PGMN)='PO006A' ");
            sb.Append("AND UPPER(BR6USER) = '" + UserName.Trim().ToUpper() + "' ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetProjectPermissionList(string userName, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT a.D70PROJ, a.D70USER, a.D70ALVL, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".PJUA a ");
            sb.Append("LEFT JOIN " + schema.Trim() + ".POPRJ p ");
            sb.Append("ON a.D70PROJ=p.JOPRJ# ");
            sb.Append("WHERE TRIM(UPPER(D70USER)) = '" + userName.Trim().ToUpper() + "' ");
            sb.Append("AND p.JOSTS NOT IN ('8', '9') ");

            return sb.ToString();
        }

        public static string GetProjectCodeDescription(string UserName, string ProjectNum, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT p.JOPRJ#, a.D70USER, a.D70ALVL, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPRJ p ");
            sb.Append("INNER JOIN " + schema.Trim() + ".PJUA a ");
            sb.Append("ON p.JOPRJ# = a.D70PROJ ");
            sb.Append("WHERE UPPER(p.JOPRJ#) = '" + ProjectNum.ToUpper() + "' ");
            sb.Append("AND UPPER(a.D70USER) = '" + UserName.ToUpper() + "' ");

            return sb.ToString();
        }

        public static string GetProjectFullList(string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT p.JOPRJ#, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPRJ p ");
            sb.Append("ORDER BY p.JOPRJ# ");

            return sb.ToString();

        }

        public static string GetProjectByID(string ProjNum, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT p.JOPRJ#, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPRJ p ");
            sb.Append("WHERE UPPER(p.JOPRJ#) = '" + ProjNum.ToUpper().Trim() + "' ");

            return sb.ToString();
        }

        public static string GetProjectActiveList(string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT p.JOPRJ#, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPRJ p ");
            sb.Append("WHERE p.JOSTS NOT IN ('8', '9') ");
            sb.Append("ORDER BY p.JOPRJ# ");

            return sb.ToString();

        }

        public static string GetProjectInActiveList(string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT p.JOPRJ#, p.JODES1 ");
            sb.Append("FROM " + schema.Trim() + ".POPRJ p ");
            sb.Append("WHERE p.JOSTS IN ('8', '9') ");
            sb.Append("ORDER BY p.JOPRJ# ");

            return sb.ToString();

        }

        public static string GetProjectsNotInProjectGroupByPGID(string PGID, string status)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT prj.JOPRJ#, prj.JODES1, prj.JOSTS ");
            sb.Append(", CASE(prj.JOSTS) ");
            sb.Append("    WHEN 1 THEN 'NEW' ");
            sb.Append("    WHEN 2 THEN 'ACTIVE' ");
            sb.Append("    WHEN 8 THEN 'HOLD' ");
            sb.Append("    WHEN 9 THEN 'INACTIVE' ");
            sb.Append("  END AS StatusDesc ");
            sb.Append("FROM CMSDATEDI.POPRJ prj ");
            sb.Append("WHERE prj.JOPRJ# NOT IN (SELECT mp.PROJ# ");
            sb.Append("		FROM WOW.MNGRPPRJ mp ");
            sb.Append("		WHERE mp.PGID = " + PGID + ") ");

            if (status.ToUpper().Equals("ACTIVE"))
            {
                sb.Append("AND prj.JOSTS IN ('1', '2') ");
            }
            else if (status.ToUpper().Equals("INACTIVE"))
            {
                sb.Append("AND prj.JOSTS IN ('8', '9') ");
            }
            else { }

            sb.Append("ORDER BY prj.JOPRJ# ");

            return sb.ToString();
        }

        public static string GetProjectsInProjectGroupByPGIDandStatus(string PGID, string status)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT prj.JOPRJ#, prj.JODES1, prj.JOSTS ");
            sb.Append(", CASE(prj.JOSTS) ");
            sb.Append("    WHEN 1 THEN 'NEW' ");
            sb.Append("    WHEN 2 THEN 'ACTIVE' ");
            sb.Append("    WHEN 8 THEN 'HOLD' ");
            sb.Append("    WHEN 9 THEN 'INACTIVE' ");
            sb.Append("  END AS StatusDesc ");
            sb.Append("FROM CMSDATEDI.POPRJ prj ");
            sb.Append("WHERE prj.JOPRJ# IN (SELECT mp.PROJ# ");
            sb.Append("		FROM WOW.MNGRPPRJ mp ");
            sb.Append("		WHERE mp.PGID = " + PGID + ") ");
            
            if (status.ToUpper().Equals("ACTIVE"))
            {
                sb.Append("AND prj.JOSTS IN ('1', '2') ");
            }
            else if (status.ToUpper().Equals("INACTIVE"))
            {
                sb.Append("AND prj.JOSTS IN ('8', '9') ");
            }
            else { }
            sb.Append("ORDER BY prj.JOPRJ# ");

            return sb.ToString();
        }

        public static string GetProjectsInProjectGroupByPGID(string PGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT prj.JOPRJ#, prj.JODES1, prj.JOSTS ");
            sb.Append(", CASE(prj.JOSTS) ");
            sb.Append("    WHEN 1 THEN 'NEW' ");
            sb.Append("    WHEN 2 THEN 'ACTIVE' ");
            sb.Append("    WHEN 8 THEN 'HOLD' ");
            sb.Append("    WHEN 9 THEN 'INACTIVE' ");
            sb.Append("  END AS StatusDesc ");
            sb.Append("FROM CMSDATEDI.POPRJ prj ");
            sb.Append("WHERE prj.JOPRJ# IN (SELECT mp.PROJ# ");
            sb.Append("		FROM WOW.MNGRPPRJ mp ");
            sb.Append("		WHERE mp.PGID = " + PGID + ") ");
            sb.Append("ORDER BY prj.JOPRJ# ");

            return sb.ToString();
        }

        public static string GetUoMList(string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT A9, A30 ");
            sb.Append("FROM " + schema.Trim() + ".CODE ");
            sb.Append("WHERE A2 = 'HH' ");
            sb.Append("ORDER BY A9 ");

            return sb.ToString();
        }

        public static string GetTaxGroupList(string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT NKGRP, NKDES ");
            sb.Append("FROM " + schema.Trim() + ".TXGP ");

            return sb.ToString();
        }

        public static string GetTaxRate(string TaxGroupCode, string TaxRateCode, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ELECT tx.NMGRP, tx.NMRTC, tx.NMRAT ");
            sb.Append("FROM " + schema.Trim() + ".TXRTD tx ");
            sb.Append("WHERE UPPER(TRIM(tx.NMGRP)) = '" + TaxGroupCode.Trim().ToUpper() + "' ");
            sb.Append("AND tx.NMRTC = '" + TaxRateCode + "' ");
            sb.Append("AND tx.NMDAT = (SELECT MAX(tx2.NMDAT) AS MaxDate ");
            sb.Append("	            FROM " + schema.Trim() + ".TXRTD tx2 ");
            sb.Append("             WHERE UPPER(TRIM(tx2.NMGRP)) = '" + TaxGroupCode.Trim().ToUpper() + "' ");
            sb.Append("             AND tx2.NMRTC = '" + TaxRateCode + "' ");
            sb.Append("             GROUP BY tx2.NMGRP, tx2.NMRTC) ");

            return sb.ToString();
        }

        public static string GetTaxRateList(string TaxGroupCode, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT NIGRP, NIRTC, NIDES ");
            sb.Append("FROM " + schema.Trim() + ".TXRT ");  
            sb.Append("WHERE TRIM(UPPER(NIGRP)) = '" + TaxGroupCode.Trim().ToUpper() + "' ");
            sb.Append("ORDER BY NIRTC");

            return sb.ToString();
        }

        #endregion

        #region Requisition

        public static string GetMasterRequisitionByEmpNum(string EmpNum, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT B44MREQ, B44DES1, B44DES2, B44DES3, B44REQR, ");
            sb.Append("B44BUYR, B44PLNT, B44CUSR, B44CDAT, B44CTME, B44UUSR, ");
            sb.Append("B44UDAT, B44UTME, B44CCDE, B44PROJ ");
            sb.Append(", r.AU8REQR, r.AU8USID ");
            sb.Append("FROM " + schema + ".PORQH ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON TRIM(B44REQR) = r.AU8REQR ");
            sb.Append("WHERE B44REQR = '" + EmpNum + "' ");

            return sb.ToString();
        }

        public static string GetMasterRequisitionByID(string ID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT B44MREQ, B44DES1, B44DES2, B44DES3, B44REQR, ");
            sb.Append("B44BUYR, B44PLNT, B44CUSR, B44CDAT, B44CTME, B44UUSR, ");
            sb.Append("B44UDAT, B44UTME, B44CCDE, B44PROJ ");
            sb.Append(", r.AU8REQR, r.AU8USID ");
            sb.Append("FROM " + schema + ".PORQH ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON TRIM(B44REQR) = r.AU8REQR ");
            sb.Append("WHERE B44MREQ = '" + ID + "' ");

            return sb.ToString();
        }

        public static string GetMasterRequisitionForApprovalByUserName(string UserName, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT h.B44MREQ ");
            sb.Append("FROM " + schema + ".PORQH h ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON h.B44MREQ = req.JIMREQ ");
            sb.Append("INNER JOIN WOW.PORROUTE rt ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON rt.ROEMP# = r.AU8REQR ");
            sb.Append("INNER JOIN " + schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("WHERE rt.ROSTS = 'P' ");
            sb.Append("AND UPPER(r.AU8USID) = '" + UserName.ToUpper().Trim() + "' ");
            sb.Append("GROUP BY h.B44MREQ ");

            return sb.ToString();
        }

        public static string GetRequisitionForApprovalByUserName(string UserName, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT rt.ROID, rt.ROREQ#, rt.ROEMP#, rt.ROSTS, rt.ROLVL, ");
            sb.Append("rt.ROPASS, rt.RORJTO, rt.RORJRS, rt.ROSUBDT, rt.ROSUBBY, rt.ROUPDT, rt.ROUPBY, r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append(", req.JIREQ#, req.JIQDAT, req.JIQTYO, req.JIOUNT, req.JIPT#, req.JIUPRC, req.JIPUNT, req.JIRDAT, ");
            sb.Append("req.JIREQR, req.JIAPRV, req.JIVND#, req.JIVNAM, req.JIVPT#, req.JIGL#, req.JIJOB#, req.JISEQ#, req.JIPRJ#, req.JIDEPT, ");
            sb.Append("req.JISTS, req.JIPO#, req.JIITM#, req.JIREL#, req.JIITYP, req.JIUSER, req.JIISTR, req.JIOSTR, req.JIRESN, req.JIAPBY, ");
            sb.Append("req.JICRCM, req.JITAXG, req.JITAXR, req.JIBUYR, req.JIPLNT, req.JIPPRT, req.JICSTC, req.JICSTT, req.JIMREQ ");
            sb.Append("FROM WOW.PORROUTE rt ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON TRIM(rt.ROEMP#) = r.AU8REQR ");
            sb.Append("INNER JOIN " + schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            //sb.Append("WHERE rt.ROSTS = 'P' ");
            sb.Append("WHERE 1 = 1 ");
            sb.Append("AND TRIM(UPPER(r.AU8USID)) = '" + UserName.ToUpper().Trim() + "' ");

            return sb.ToString();
        }

        public static string GetRequisitionForApprovalByUserNameAndMasterReqID(string UserName, string MasterReqID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT rt.ROID, rt.ROREQ#, rt.ROEMP#, rt.ROSTS, rt.ROLVL, ");
            sb.Append("rt.ROPASS, rt.RORJTO, rt.RORJRS, rt.ROSUBDT, rt.ROSUBBY, rt.ROUPDT, rt.ROUPBY, r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append(", req.JIREQ#, req.JIQDAT, req.JIQTYO, req.JIOUNT, req.JIPT#, req.JIUPRC, req.JIPUNT, req.JIRDAT, ");
            sb.Append("req.JIREQR, req.JIAPRV, req.JIVND#, req.JIVNAM, req.JIVPT#, req.JIGL#, req.JIJOB#, req.JISEQ#, req.JIPRJ#, req.JIDEPT, ");
            sb.Append("req.JISTS, req.JIPO#, req.JIITM#, req.JIREL#, req.JIITYP, req.JIUSER, req.JIISTR, req.JIOSTR, req.JIRESN, req.JIAPBY, ");
            sb.Append("req.JICRCM, req.JITAXG, req.JITAXR, req.JIBUYR, req.JIPLNT, req.JIPPRT, req.JICSTC, req.JICSTT, req.JIMREQ ");
            sb.Append("FROM WOW.PORROUTE rt ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON TRIM(rt.ROEMP#) = r.AU8REQR ");
            sb.Append("INNER JOIN " + schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            sb.Append("WHERE rt.ROSTS = 'P' ");
            sb.Append("AND TRIM(UPPER(r.AU8USID)) = '" + UserName.ToUpper().Trim() + "' ");
            sb.Append("AND TRIM(req.JIMREQ) = " + MasterReqID + " ");

            return sb.ToString();
        }

        public static string UpdateRequisitionApproval(ReqDetail req, string ApprovedBy, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + schema + ".PORQI ");
            sb.Append("SET JIAPRV = '" + req.ApprovedStatus + "' ");
            sb.Append(", JIAPBY = '" + ApprovedBy + "' ");
            sb.Append("WHERE JIREQ# = '" + req.RequisitionNumber.ToString() + "' ");

            return sb.ToString();

        }

        public static string GetApprovalMaxLevel(string MasterReqID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT req.JIMREQ,  MAX(ROLVL) As MaxLevel ");
            sb.Append("FROM WOW.PORROUTE rt ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            sb.Append("WHERE req.JIMREQ = " + MasterReqID + " ");
            sb.Append("GROUP BY req.JIMREQ ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetApprovalMaxPass(string MasterReqID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT req.JIMREQ,  MAX(ROPASS) As MaxPass ");
            sb.Append("FROM WOW.PORROUTE rt ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            sb.Append("WHERE req.JIMREQ = " + MasterReqID + " ");
            sb.Append("GROUP BY req.JIMREQ ");
            sb.Append("");

            return sb.ToString();
        }
        public static string GetRequisitionForApprovalByUserNameAndMasterID(string UserName, string MasterReqID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT rt.ROID, rt.ROREQ#, rt.ROEMP#, rt.ROSTS, rt.ROLVL, ");
            sb.Append("rt.ROPASS, rt.RORJTO, rt.RORJRS, rt.ROSUBDT, rt.ROSUBBY, rt.ROUPDT, rt.ROUPBY, r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append(", req.JIREQ#, req.JIQDAT, req.JIQTYO, req.JIOUNT, req.JIPT#, req.JIUPRC, req.JIPUNT, req.JIRDAT, ");
            sb.Append("req.JIREQR, req.JIAPRV, req.JIVND#, req.JIVNAM, req.JIVPT#, req.JIGL#, req.JIJOB#, req.JISEQ#, req.JIPRJ#, req.JIDEPT, ");
            sb.Append("req.JISTS, req.JIPO#, req.JIITM#, req.JIREL#, req.JIITYP, req.JIUSER, req.JIISTR, req.JIOSTR, req.JIRESN, req.JIAPBY, ");
            sb.Append("req.JICRCM, req.JITAXG, req.JITAXR, req.JIBUYR, req.JIPLNT, req.JIPPRT, req.JICSTC, req.JICSTT, req.JIMREQ ");
            sb.Append("FROM WOW.PORROUTE rt ");
            sb.Append("INNER JOIN " + schema + ".REQUID r ");
            sb.Append("ON TRIM(rt.ROEMP#) = r.AU8REQR ");
            sb.Append("INNER JOIN " + schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("INNER JOIN " + schema + ".PORQI req ");
            sb.Append("ON rt.ROREQ# = req.JIREQ# ");
            sb.Append("WHERE 1 = 1 ");
            //sb.Append("AND rt.ROSTS = 'P' ");
            sb.Append("AND TRIM(UPPER(r.AU8USID)) = '" + UserName.ToUpper().Trim() + "' ");
            sb.Append("AND TRIM(req.JIMREQ) = " + MasterReqID + " ");

            return sb.ToString();
        }

        public static string GetRequisitionByMasterReqID(string MasterReqID, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT JIREQ#, JIQDAT, JIQTYO, JIOUNT, JIPT#, JIUPRC, JIPUNT, JIRDAT, ");
            sb.Append("JIREQR, JIAPRV, JIVND#, JIVNAM, JIVPT#, JIGL#, JIJOB#, JISEQ#, JIPRJ#, JIDEPT, ");
            sb.Append("JISTS, JIPO#, JIITM#, JIREL#, JIITYP, JIUSER, JIISTR, JIOSTR, JIRESN, JIAPBY, ");
            sb.Append("JICRCM, JITAXG, JITAXR, JIBUYR, JIPLNT, JIPPRT, JICSTC, JICSTT, JIMREQ ");
            sb.Append("FROM " + schema + ".PORQI ");
            sb.Append("WHERE JIMREQ = " + MasterReqID + " ");

            return sb.ToString();
        }

        public static string GetRequisitionByReqNum(string ReqNum, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT JIREQ#, JIQDAT, JIQTYO, JIOUNT, JIPT#, JIUPRC, JIPUNT, JIRDAT, ");
            sb.Append("JIREQR, JIAPRV, JIVND#, JIVNAM, JIVPT#, JIGL#, JIJOB#, JISEQ#, JIPRJ#, JIDEPT, ");
            sb.Append("JISTS, JIPO#, JIITM#, JIREL#, JIITYP, JIUSER, JIISTR, JIOSTR, JIRESN, JIAPBY, ");
            sb.Append("JICRCM, JITAXG, JITAXR, JIBUYR, JIPLNT, JIPPRT, JICSTC, JICSTT, JIMREQ ");
            sb.Append("FROM " + schema + ".PORQI ");
            sb.Append("WHERE JIREQ# = '" + ReqNum + "' ");

            return sb.ToString();
        }
        
        public static string GetRequisitionByEmpNum(string EmpNum, string schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT JIREQ#, JIQDAT, JIQTYO, JIOUNT, JIPT#, JIUPRC, JIPUNT, JIRDAT, ");
            sb.Append("JIREQR, JIAPRV, JIVND#, JIVNAM, JIVPT#, JIGL#, JIJOB#, JISEQ#, JIPRJ#, JIDEPT, ");
            sb.Append("JISTS, JIPO#, JIITM#, JIREL#, JIITYP, JIUSER, JIISTR, JIOSTR, JIRESN, JIAPBY, ");
            sb.Append("JICRCM, JITAXG, JITAXR, JIBUYR, JIPLNT, JIPPRT, JICSTC, JICSTT, JIMREQ ");
            sb.Append("FROM " + schema + ".PORQI ");
            sb.Append("WHERE JIREQR = '" + EmpNum + "' ");
            sb.Append("AND JISTS = 'N' ");

            return sb.ToString();
        }
        
        #endregion

        #region ProjectGroup

        public static string GetProjectGroupList()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT PGID, PGNAME, PGSTS, PGCRDT, PGCRBY, PGUPDT, PGUPBY ");
            sb.Append("FROM WOW.PRJGRP ");

            return sb.ToString();
        }

        public static string GetProjectGroupListByStatus(string status)
        {
            StringBuilder sb = new StringBuilder();
            if (status != null)
            {
                sb.Append("SELECT PGID, PGNAME, PGSTS, PGCRDT, PGCRBY, PGUPDT, PGUPBY ");
                sb.Append("FROM WOW.PRJGRP ");
                sb.Append("WHERE UPPER(PGSTS) = '" + status.ToUpper() + "' ");
            }
            return sb.ToString();
        }

        public static string GetProjectGroupByID(Int32 id)
        {
            StringBuilder sb = new StringBuilder();
            if (id != 0)
            {
                sb.Append("SELECT PGID, PGNAME, PGSTS, PGCRDT, PGCRBY, PGUPDT, PGUPBY ");
                sb.Append("FROM WOW.PRJGRP ");
                sb.Append("WHERE PGID = " + id + " ");
            }
            return sb.ToString();
        }

        public static string InsertProjectGroup(ProjectGroup prjGroup)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO WOW.PRJGRP(PGNAME, PGSTS, PGCRBY, PGUPBY) ");
            sb.Append("VALUES('" + prjGroup.Name.ToUpper().Trim() + "', ");
            sb.Append("'" + prjGroup.Status.ToUpper().Trim() + "', ");
            sb.Append("'" + prjGroup.CreatedBy.ToUpper().Trim() + "', ");
            sb.Append("'" + prjGroup.UpdatedBy.ToUpper().Trim() + "') ");

            return sb.ToString();
        }

        public static string UpdateProjectGroup(ProjectGroup prjGroup)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE WOW.PRJGRP ");
            sb.Append("SET PGNAME = '" + prjGroup.Name.ToUpper().Trim() + "', ");
            sb.Append("PGSTS = '" + prjGroup.Status.ToUpper().Trim() + "', ");
            sb.Append("PGUPDT = '" + prjGroup.UpdatedDate.ToTimeStamp() + "', ");
            sb.Append("PGUPBY = '" + prjGroup.UpdatedBy.ToUpper().Trim() + "' ");
            sb.Append("WHERE PGID = " + prjGroup.PGID + " ");
            sb.Append("");
            sb.Append("");

            return sb.ToString();
        }

        public static string DeleteProjectGroup(Int32 pgid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE WOW.PRJGRP ");
            sb.Append("SET PGSTS = 'I' ");
            sb.Append("WHERE PGID = " + pgid + " ");

            return sb.ToString();
        }

        public static string DeleteProjectGroup(ProjectGroup prjGroup)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE WOW.PRJGRP ");
            sb.Append("SET PGSTS = 'I', ");
            sb.Append("PGUPDT = '" + prjGroup.UpdatedDate.ToTimeStamp() + "', ");
            sb.Append("PGUPBY = '" + prjGroup.UpdatedBy.ToUpper().Trim() + "' ");
            sb.Append("WHERE PGID = " + prjGroup.PGID + " ");
            sb.Append("");

            return sb.ToString();
        }

        #endregion

        #region MNGroupProject

        public static string GetMNGroupProjectByPGID(string PGID, string Schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT mp.MNPGID, p.PGID,  p.PGNAME, p.PGSTS, mp.PROJ#, prj.JODES1, mp.CRBY, mp.CRDT, mp.UPBY, mp.UPDT, prj.JOSTS ");
            sb.Append("FROM WOW.MNGRPPRJ mp ");
            sb.Append("INNER JOIN WOW.PRJGRP p ");
            sb.Append("ON mp.PGID = p.PGID ");
            sb.Append("INNER JOIN " + Schema + ".POPRJ prj ");
            sb.Append("ON mp.PROJ# = prj.JOPRJ# ");
            sb.Append("WHERE p.PGID = " + PGID + " ");

            return sb.ToString();
        }

        public static string GetMNGroupProjectByMNPGID(string MNPGID, string Schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT mp.MNPGID, p.PGID,  p.PGNAME, p.PGSTS, mp.PROJ#, prj.JODES1, mp.CRBY, mp.CRDT, mp.UPBY, mp.UPDT, prj.JOSTS ");
            sb.Append("FROM WOW.MNGRPPRJ mp ");
            sb.Append("INNER JOIN WOW.PRJGRP p ");
            sb.Append("ON mp.PGID = p.PGID ");
            sb.Append("INNER JOIN " + Schema + ".POPRJ prj ");
            sb.Append("ON mp.PROJ# = prj.JOPRJ# ");
            sb.Append("WHERE mp.MNPGID = " + MNPGID + " ");

            return sb.ToString();
        }

        public static string GetMNGroupProjectByPGIDAndProjNum(string PGID, string ProjNum, string Schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT mp.MNPGID, p.PGID,  p.PGNAME, p.PGSTS, mp.PROJ#, prj.JODES1, mp.CRBY, mp.CRDT, mp.UPBY, mp.UPDT, prj.JOSTS ");
            sb.Append("FROM WOW.MNGRPPRJ mp ");
            sb.Append("INNER JOIN WOW.PRJGRP p ");
            sb.Append("ON mp.PGID = p.PGID ");
            sb.Append("INNER JOIN " + Schema + ".POPRJ prj ");
            sb.Append("ON mp.PROJ# = prj.JOPRJ# ");
            sb.Append("WHERE p.PGID = " + PGID + " ");
            sb.Append("AND UPPER(mp.PROJ#) = '" + ProjNum.ToUpper() + "' ");

            return sb.ToString();
        }

        public static string GetMNGroupProjectByProjNumWithoutPGID(string PGID, string ProjNum, string Schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT mp.MNPGID, p.PGID,  p.PGNAME, p.PGSTS, mp.PROJ#, prj.JODES1, mp.CRBY, mp.CRDT, mp.UPBY, mp.UPDT, prj.JOSTS ");
            sb.Append("FROM WOW.MNGRPPRJ mp ");
            sb.Append("INNER JOIN WOW.PRJGRP p ");
            sb.Append("ON mp.PGID = p.PGID ");
            sb.Append("INNER JOIN " + Schema + ".POPRJ prj ");
            sb.Append("ON mp.PROJ# = prj.JOPRJ# ");
            sb.Append("WHERE p.PGID <> " + PGID + " ");
            sb.Append("AND UPPER(mp.PROJ#) = '" + ProjNum.ToUpper() + "' ");

            return sb.ToString();
        }

        public static string InsertMNGroupProject(MNGroupProject grpProject)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO WOW.MNGRPPRJ(PGID, PROJ#, CRBY, UPBY) ");
            sb.Append("VALUES(" + grpProject.PGID + ", ");
            sb.Append("'" + grpProject.ProjectID + "', ");
            sb.Append("'" + grpProject.CreatedBy + "', ");
            sb.Append("'" + grpProject.UpdatedBy + "')");

            return sb.ToString();
        }

        public static string DeleteMNGroupProject(string MNPGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");
            sb.Append("FROM WOW.MNGRPPRJ ");
            sb.Append("WHERE MNPGID = " + MNPGID);

            return sb.ToString();

        }

        public static string DeleteMNGroupProjectByPGID(string PGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");
            sb.Append("FROM WOW.MNGRPPRJ ");
            sb.Append("WHERE PGID = '" + PGID + "' ");

            return sb.ToString();
        }

        #endregion

        #region MNGroupRequisitioner

        public static string GetRequisitioners(string Schema)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append("FROM " + Schema + ".REQUID r ");
            sb.Append("JOIN " + Schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("ORDER BY AU8USID ");

            return sb.ToString();
        }

        public static string GetRequisitionerByUserName(string Schema, string UserName)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append("FROM " + Schema + ".REQUID r ");
            sb.Append("JOIN " + Schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("WHERE UPPER(TRIM(r.AU8USID)) = '" + UserName.ToUpper().Trim() + "' ");
            return sb.ToString();
        }

        public static string GetRequisitionerInfoByEmpNum(string Schema, string EmpNum)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT r.AU8REQR, r.AU8USID, rq.JQDESC ");
            sb.Append("FROM " + Schema + ".REQUID r ");
            sb.Append("JOIN " + Schema + ".POREQR rq ");
            sb.Append("ON r.AU8REQR = rq.JQREQR ");
            sb.Append("WHERE UPPER(TRIM(r.AU8REQR)) = '" + EmpNum + "' ");

            return sb.ToString();
        }
        public static string GetMNGroupRequisitionerByPGID(string Schema, string PGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT pg.MNPRGID, pg.PGID, p.PGNAME, r2.AU8USID, r2.JQDESC,  pg.CRBY, pg.CRDT, pg.UPBY, pg.UPDT ");
            sb.Append("FROM WOW.MNRQPRJG pg ");
            sb.Append("JOIN (SELECT r.AU8USID, rq.JQDESC ");
            sb.Append("	FROM " + Schema + ".REQUID r ");
            sb.Append("	JOIN " + Schema + ".POREQR rq ");
            sb.Append("	ON r.AU8REQR = rq.JQREQR) r2 ");
            sb.Append("ON pg.AU8USID = r2.AU8USID ");
            sb.Append("JOIN WOW.PRJGRP p ");
            sb.Append("ON pg.PGID = p.PGID ");
            sb.Append("WHERE pg.PGID = '" + PGID + "' ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetMNGroupRequisitionerByMNPRGID(string Schema, string MNPRGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT pg.MNPRGID, pg.PGID, p.PGNAME, r2.AU8USID, r2.JQDESC,  pg.CRBY, pg.CRDT, pg.UPBY, pg.UPDT ");
            sb.Append("FROM WOW.MNRQPRJG pg ");
            sb.Append("JOIN (SELECT r.AU8USID, rq.JQDESC ");
            sb.Append("	FROM " + Schema + ".REQUID r ");
            sb.Append("	JOIN " + Schema + ".POREQR rq ");
            sb.Append("	ON r.AU8REQR = rq.JQREQR) r2 ");
            sb.Append("ON pg.AU8USID = r2.AU8USID ");
            sb.Append("JOIN WOW.PRJGRP p ");
            sb.Append("ON pg.PGID = p.PGID ");
            sb.Append("WHERE pg.MNPRGID = '" + MNPRGID + "' ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetMNGroupRequisitionerByPGIDAndUser(string Schema, string PGID, string UserID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT pg.MNPRGID, pg.PGID, p.PGNAME, r2.AU8USID, r2.JQDESC,  pg.CRBY, pg.CRDT, pg.UPBY, pg.UPDT ");
            sb.Append("FROM WOW.MNRQPRJG pg ");
            sb.Append("JOIN (SELECT r.AU8USID, rq.JQDESC ");
            sb.Append("	FROM " + Schema + ".REQUID r ");
            sb.Append("	JOIN " + Schema + ".POREQR rq ");
            sb.Append("	ON r.AU8REQR = rq.JQREQR) r2 ");
            sb.Append("ON pg.AU8USID = r2.AU8USID ");
            sb.Append("JOIN WOW.PRJGRP p ");
            sb.Append("ON pg.PGID = p.PGID ");
            sb.Append("WHERE pg.PGID = '" + PGID + "' ");
            sb.Append("AND TRIM(UPPER(r2.AU8USID)) = '" + UserID.ToUpper().Trim() + "' ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetMNGroupRequisitionerAndUserWithoutPGID(string Schema, string PGID, string UserID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT pg.MNPRGID, pg.PGID, p.PGNAME, r2.AU8USID, r2.JQDESC,  pg.CRBY, pg.CRDT, pg.UPBY, pg.UPDT ");
            sb.Append("FROM WOW.MNRQPRJG pg ");
            sb.Append("JOIN (SELECT r.AU8USID, rq.JQDESC ");
            sb.Append("	FROM " + Schema + ".REQUID r ");
            sb.Append("	JOIN " + Schema + ".POREQR rq ");
            sb.Append("	ON r.AU8REQR = rq.JQREQR) r2 ");
            sb.Append("ON pg.AU8USID = r2.AU8USID ");
            sb.Append("JOIN WOW.PRJGRP p ");
            sb.Append("ON pg.PGID = p.PGID ");
            sb.Append("WHERE pg.PGID <> '" + PGID + "' ");
            sb.Append("AND TRIM(UPPER(r2.AU8USID)) = '" + UserID.ToUpper().Trim() + "' ");
            sb.Append("");

            return sb.ToString();
        }

        public static string InsertMNGroupRequisitioner(MNGroupRequisitioner grReq)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO WOW.MNRQPRJG(PGID, AU8USID, CRBY, UPBY) ");
            sb.Append("VALUES(" + grReq.PGID + ", ");
            sb.Append("'" + grReq.UserID + "', ");
            sb.Append("'" + grReq.CreatedBy + "', ");
            sb.Append("'" + grReq.UpdatedBy + "') ");

            return sb.ToString();
        }

        public static string DeleteMNGroupRequisitioner(string MNPRGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");
            sb.Append("FROM WOW.MNRQPRJG ");
            sb.Append("WHERE MNPRGID = '" + MNPRGID + "' ");

            return sb.ToString();
        }

        public static string DeleteMNGroupRequisitionerByPGID(string PGID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");
            sb.Append("FROM WOW.MNRQPRJG ");
            sb.Append("WHERE PGID = '" + PGID + "' ");

            return sb.ToString();
        }

        #endregion

        #region CMSPermissions

        public static string GetCMSPermissions(string Schema, string ProjectID, string Requisitioner)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT pj.D70PROJ, pj.D70USER, pj.D70ALVL, pj.D70CUSR, pj.D70CDAT, pj.D70CTME, pj.D70UUSR, pj.D70UDAT, pj.D70UTME ");
            sb.Append("FROM " + Schema + ".PJUA pj ");
            sb.Append("WHERE TRIM(UPPER(pj.D70PROJ)) = '" + ProjectID.ToUpper().Trim() + "' ");
            sb.Append("AND TRIM(UPPER(pj.D70USER)) = '" + Requisitioner.ToUpper().Trim() + "' ");
            

            return sb.ToString();
        }

        public static string DeleteCMSPermissions(string Schema, string ProjectID, string Requisitioner)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");
            sb.Append("FROM " + Schema + ".PJUA pj ");
            sb.Append("WHERE TRIM(UPPER(pj.D70PROJ)) = '" + ProjectID.ToUpper().Trim() + "' ");
            sb.Append("AND TRIM(UPPER(pj.D70USER)) = '" + Requisitioner.ToUpper().Trim() + "' ");

            return sb.ToString();
        }

        public static string InsertCMSPermissions(CMSPermission permission)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO CMSDATEDI.PJUA(D70PROJ, D70USER, D70ALVL, D70CUSR, D70CDAT, D70CTME, D70UUSR, D70UDAT, D70UTME) ");
            sb.Append("VALUES(");
            sb.Append("'" + permission.ProjectID + "', ");
            sb.Append("'" + permission.RequisitionerID + "', ");
            sb.Append("" + permission.Level + ", ");
            sb.Append("'" + permission.CreatedUserID + "', ");
            sb.Append("'" + permission.CreatedDate + "', ");
            sb.Append("'" + permission.CreatedTime + "', ");
            sb.Append("'" + permission.UpdatedUserID + "', ");
            sb.Append("'" + permission.UpdatedDate + "', ");
            sb.Append("'" + permission.UpdatedTime + "')");

            return sb.ToString();

        }

        #endregion

        #region Routing

        public static string GetSupervisorByID(string EmployeeNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT e.EMP#, e.FNAME, e.LNAME, e.EPSUP#, e.JOBCLS, ");
            sb.Append("s.SVEMPN, s.SVFNAM, s.SVLNAM, s.SVDSC1, e2.JOBCLS, e2.TITLE, e.TITLE ");
            sb.Append("FROM OIPAYFILES.EMPLOYEE e ");
            sb.Append("JOIN OIPAYFILES.TSUPERVS s ON s.SVSUPN=e.EPSUP# ");
            sb.Append("JOIN OIPAYFILES.EMPLOYEE e2 ");
            sb.Append("ON TRIM(s.SVEMPN) = TRIM(e2.EMP#) ");
            sb.Append("WHERE TRIM(e.EMP#)='" + EmployeeNumber + "' ");

            return sb.ToString();

        }

        public static string GetManagerByDept(string DepartmentCode)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT EMP#, LNAME, FNAME, MNAME, EMORG3, JOBCLS, TITLE ");
            sb.Append("FROM WOW.EMPLOYEE ");
            sb.Append("WHERE EMDELT = 'A' ");
            sb.Append("AND EMORG3 IN (SELECT o.DOORG3 FROM WOW.PORDPORG o ");
            sb.Append("WHERE o.DONUM = '" + DepartmentCode + "') ");
            sb.Append("AND JOBCLS IN ( 'MANAGER', 'AGM') ");
            sb.Append("");

            return sb.ToString();
        }

        public static string GetManagerApprovalLimit()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT LTLIMIT ");
            sb.Append("FROM WOW.PORLIMIT ");
            sb.Append("WHERE LTJOBCLS = 'MANAGER' ");
            sb.Append("AND LTTITLE = 'MANAGER' ");

            return sb.ToString();
        }

        public static string GetApprovalLimit(string JobClass, string Title)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT LTLIMIT ");
            sb.Append("FROM WOW.PORLIMIT ");
            sb.Append("WHERE LTJOBCLS = '" + JobClass + "' ");
            sb.Append("AND LTTITLE = '" + Title + "' ");

            return sb.ToString();
        }

        public static string GetNextSupervisorApprovalLimit(string EmployeeNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT l.LTLIMIT ");
            sb.Append("FROM WOW.PORLIMIT l ");
            sb.Append("INNER JOIN (SELECT e2.JOBCLS, e2.TITLE ");
            sb.Append("    FROM OIPAYFILES.EMPLOYEE e ");
            sb.Append("J    OIN OIPAYFILES.TSUPERVS s ON s.SVSUPN=e.EPSUP# ");
            sb.Append("    JOIN OIPAYFILES.EMPLOYEE e2 ");
            sb.Append("    ON TRIM(s.SVEMPN) = TRIM(e2.EMP#) ");
            sb.Append("    WHERE TRIM(e.EMP#)='" + EmployeeNumber + "') AS emp ");
            sb.Append("ON UPPER(TRIM(l.LTJOBCLS)) = UPPER(TRIM(emp.JOBCLS)) ");
            sb.Append("AND UPPER(TRIM(l.LTTITLE)) = UPPER(TRIM(emp.TITLE)) ");

            return sb.ToString();

        }

        public static string GetSkipSupervisor(string DepartmentCode, string JobClass)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT SKID, SKDEPT, SKJOBCLS ");
            sb.Append("FROM WOW.PORSKIP ");
            sb.Append("WHERE TRIM(SKDEPT) = '" + DepartmentCode.Trim() + "' ");
            sb.Append("AND TRIM(SKJOBCLS) = '" + JobClass.Trim() + "' ");
            sb.Append("");
            sb.Append("");

            return sb.ToString();

        }

        public static string GetRouteByRequisitionNumber(string RequisitionNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ROID, ROREQ#, ROEMP#, ROSTS, ROLVL, ROPASS, RORJTO, RORJRS, ROSUBDT, ROSUBBY, ROUPDT, ROUPBY ");
            sb.Append("FROM WOW.PORROUTE ");
            sb.Append("WHERE ROREQ# = " + RequisitionNumber + " ");
            sb.Append("ORDER BY ROPASS, ROLVL ");

            return sb.ToString();
        }

        public static string GetRouteByRequisitionNumberAndStatus(string RequisitionNumber, string Status)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ROID, ROREQ#, ROEMP#, ROSTS, ROLVL, ROPASS, RORJTO, RORJRS, ROSUBDT, ROSUBBY, ROUPDT, ROUPBY ");
            sb.Append("FROM WOW.PORROUTE ");
            sb.Append("WHERE ROREQ# = " + RequisitionNumber + " ");
            sb.Append("AND ROSTS = '" + Status + "' ");
            sb.Append("ORDER BY ROPASS, ROLVL ");

            return sb.ToString();
        }

        public static string GetRouteMaxLevelByRequisitionNumber(string RequisitionNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT MAX(ROLVL) AS MaxLevel ");
            sb.Append("FROM WOW.PORROUTE ");
            sb.Append("WHERE ROREQ# = " + RequisitionNumber + " ");
            sb.Append("GROUP BY ROREQ# ");

            return sb.ToString();
        }

        public static string GetRouteMaxLevelByRequisitionNumberAndPass(string RequisitionNumber, Int32 Pass)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT MAX(ROLVL) AS MaxLevel ");
            sb.Append("FROM WOW.PORROUTE ");
            sb.Append("WHERE ROREQ# = " + RequisitionNumber + " ");
            sb.Append("AND ROPASS = " + Pass.ToString() + " ");
            sb.Append("GROUP BY ROREQ# ");

            return sb.ToString();
        }

        public static string GetRouteMaxPassByRequisitionNumber(string RequisitionNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT MAX(ROPASS) AS MaxPass ");
            sb.Append("FROM WOW.PORROUTE ");
            sb.Append("WHERE ROREQ# = " + RequisitionNumber + " ");
            sb.Append("GROUP BY ROREQ# ");

            return sb.ToString();
        }

        public static string InsertRoute(Approval apr)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO WOW.PORROUTE(ROREQ#, ROEMP#, ROSTS, ROLVL, ROPASS, ROSUBBY) ");
            sb.Append("VALUES(" + apr.RequisitionNumber.ToString() + ", ");
            sb.Append("'" + apr.SupervisorID + "', ");
            sb.Append("'" + apr.Status + "', ");
            sb.Append("" + apr.Level.ToString() + ", ");
            sb.Append("" + apr.Pass.ToString() + ", ");
            sb.Append("'" + apr.SubmittedUserName + "') ");

            return sb.ToString();
        }
        
        public static string UpdateRoute(Approval apr)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE WOW.PORROUTE ");
            sb.Append("SET ROSTS = '" + apr.Status + "' ");
            if(apr.RejectReason.Length > 0)
            {
                sb.Append(", RORJRS = '" + apr.RejectReason + "' ");
            }
            if(apr.RejectToNumber.Length > 0)
            {
                sb.Append(", RORJTO = '" + apr.RejectToNumber.ToString() + "' ");
            }
            sb.Append(", ROUPDT = CURRENT_TIMESTAMP ");
            sb.Append(", ROUPBY = '" + apr.UpdatedUserName + "' ");
            sb.Append("WHERE ROID = " + apr.PKID + " ");

            return sb.ToString();

        }
        public static string Get(string RequisitionNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("");
            sb.Append("");
            sb.Append("");
            sb.Append("");
            sb.Append("");

            return sb.ToString();
        }
        #endregion
    }
}