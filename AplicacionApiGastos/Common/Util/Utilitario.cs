using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Util
{
    public class Utilitario
    {
        public static XmlDocument ObjectToXml(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
            }
            return xmlDoc;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public static string ServerPath(string relative)
        {

            return HostingEnvironment.MapPath(relative);
        }

        public static void Logger()
        {
            string rutaDestinoLog = ServerPath("~/Log");
            if (!Directory.Exists(rutaDestinoLog))
            {
                Directory.CreateDirectory(rutaDestinoLog);
            }

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .WriteTo.File(ServerPath("~/Log/LogService.txt"))
               .CreateLogger();
        }


        private static string GetPathToWriteLog()
        {
            string path = string.Empty;

            try
            {
                string logName = "LogApiService_" + DateTime.Now.Date.ToString("ddMMyyyy");
                string rutaDestinoLog = ServerPath("~/Log");

                string fullFileNamePath = ServerPath("~/Log/LogService_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt"); //@"" + rutaDestinoLog + "/" + logName;


                if (!Directory.Exists(rutaDestinoLog))
                {
                    Directory.CreateDirectory(rutaDestinoLog);
                }

                if (!File.Exists(fullFileNamePath))
                {
                    var fc = File.Create(fullFileNamePath);
                    fc.Close();
                }

                if (File.Exists(fullFileNamePath))
                {
                    path = fullFileNamePath;
                }
            }
            catch (Exception ex)
            {
                path = string.Empty;
            }

            return path;
        }

        public static void WriteInfoLog(string errorMessage)
        {
            //string logName = "LogApiService_" + DateTime.Now.Date.ToString("ddMMyyyy");
            //string rutaDestinoLog = ServerPath("~/Log");

            //string fullFileNamePath = ServerPath("~/Log/LogService_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt"); //@"" + rutaDestinoLog + "/" + logName;


            //if (!Directory.Exists(rutaDestinoLog))
            //{
            //    Directory.CreateDirectory(rutaDestinoLog);
            //}

            //if (!File.Exists(fullFileNamePath))
            //{
            //    var fc = File.Create(fullFileNamePath);
            //    fc.Close();
            //}
            var fullFileNamePath = GetPathToWriteLog();
            if (!string.IsNullOrEmpty(fullFileNamePath))
            {
                //var delimiter = ("*").PadLeft(70, '*');
                var date = DateTime.Now.ToString();

                //var message = delimiter + Environment.NewLine +
                //              date + Environment.NewLine +
                //              errorMessage + Environment.NewLine;

                var message = date + "[INFO]" + errorMessage + Environment.NewLine;

                using (StreamWriter sw = new StreamWriter(fullFileNamePath, true))
                {
                    sw.Write(message);
                    sw.Close();
                }
            }

        }


        public static void WriteErrorLog(string errorMessage, string errorTittle)
        {
            //string logName = "LogApiService_" + DateTime.Now.Date.ToString("ddMMyyyy");
            //string rutaDestinoLog = ServerPath("~/Log");

            //string fullFileNamePath = ServerPath("~/Log/LogService_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".txt"); //@"" + rutaDestinoLog + "/" + logName;


            //if (!Directory.Exists(rutaDestinoLog))
            //{
            //    Directory.CreateDirectory(rutaDestinoLog);
            //}

            //if (!File.Exists(fullFileNamePath))
            //{
            //    var fc = File.Create(fullFileNamePath);
            //    fc.Close();
            //}
            var fullFileNamePath = GetPathToWriteLog();
            if (!string.IsNullOrEmpty(fullFileNamePath))
            {
                //var delimiter = ("*").PadLeft(70, '*');
                var date = DateTime.Now.ToString();

                //var message = delimiter + Environment.NewLine +
                //              errorTittle + Environment.NewLine +
                //              date + Environment.NewLine +
                //              errorMessage + Environment.NewLine;

                var message = date + "[" + errorTittle + "]" + errorMessage;

                using (StreamWriter sw = new StreamWriter(fullFileNamePath, true))
                {
                    sw.Write(message);
                    sw.Close();
                }
            }

        }

    }
}
