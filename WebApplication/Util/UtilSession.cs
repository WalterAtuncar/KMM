using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Util
{
    public class UtilSession
    {

        public static UserModel Usuario
        {
            get
            {
                var usuario = new UserModel();
                if (HttpContext.Current.Session["UserModel"] != null)
                    usuario = (UserModel)HttpContext.Current.Session["UserModel"];
                return usuario;
            }
        }

    }

    public class RenderRazor
    {
        public static String RenderRazorViewToString(ControllerContext controllerContext, String viewName, Object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }

    public class Export
    {
        private byte[] ByteFile(string reportPath, string[] dataSet, object[] dataSource, string format, out string mimeType, out string extension, Dictionary<string, string> parameters = null)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
            LocalReport report = GetLocalReport();
            report.ReportPath = reportPath;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    report.SetParameters(new ReportParameter(param.Key, param.Value));
                }
            }

            for (int i = 0; i < dataSet.Count(); i++)
            {
                report.DataSources.Add(new ReportDataSource(dataSet[i], dataSource[i]));
            }

            Warning[] warnings;
            string[] streamids;
            string encoding;
            byte[] bytes = report.Render(format, null, out mimeType, out encoding, out extension, out streamids, out warnings);
            return bytes;
        }

       
        public byte[] CreateExcel(string reportPath, string[] dataSet, object[] dataSource, Dictionary<string, string> parameters = null)
        {
            string mimeType, extension;
            return ByteFile(reportPath, dataSet, dataSource, "EXCEL", out mimeType, out extension, parameters);
        }

        public byte[] CreatePdf(string reportPath, string[] dataSet, object[] dataSource, Dictionary<string, string> parameters = null)
        {
            string mimeType, extension;
            return ByteFile(reportPath, dataSet, dataSource, "PDF", out mimeType, out extension, parameters);
        }

        public void CreateExcelResponse(string reportPath, string[] dataSet, object[] dataSource, string nameFile, HttpResponseBase response)
        {
            string mimeType, extension;
            byte[] bytes = ByteFile(reportPath, dataSet, dataSource, "EXCEL", out mimeType, out extension);

            response.Buffer = true;
            response.Clear();
            response.ContentType = mimeType;
            response.AddHeader("content-disposition", "attachment; filename=" + nameFile + "." + extension);
            response.BinaryWrite(bytes);
            response.Flush();
        }

        public void CreateFileSave(string keyAppSetting, string nameFile, string reportPath, string[] dataSet, object[] dataSource, string typeFile)
        {
            string mimeType, extension;
            string absolutePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings[keyAppSetting]);

            if (!Directory.Exists(absolutePath))
                Directory.CreateDirectory(absolutePath);

            byte[] bytes = ByteFile(reportPath, dataSet, dataSource, typeFile, out mimeType, out extension);

            string finalPath = Path.Combine(absolutePath, nameFile + "." + extension);
            File.WriteAllBytes(finalPath, bytes);
        }


        private static LocalReport GetLocalReport()
        {
            LocalReport localReport = new LocalReport();
            if (localReport.DataSources.Count > 0)
                localReport.DataSources.RemoveAt(0);
            return localReport;
        }
    }
}