using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logic
{
    public class TraceHelper
    {
        public static void Write(string errorMessage)
        {
            try
            {
                string folderPath = @"" + ConfigurationManager.AppSettings["Log:FolderPath"];
                string sufixFileName = ConfigurationManager.AppSettings["Log:SufixName"];
                string fileName =  string.Concat(sufixFileName, DateTime.Now.Date.ToString("ddMMyyyy"));


                

                string fullFileNamePath = @"" + string.Concat(folderPath, fileName, ".txt");

                if (!File.Exists(fullFileNamePath))
                {
                    var fc = File.Create(fullFileNamePath);
                    fc.Close();
                }

                if (File.Exists(fullFileNamePath))
                {
                    var delimiter = ("*").PadLeft(70, '*');
                    var date = "Fecha:" + DateTime.Now.ToString();

                    var message = delimiter + Environment.NewLine +
                                  date + Environment.NewLine +
                                  errorMessage + Environment.NewLine;

                    using (StreamWriter sw = new StreamWriter(fullFileNamePath, true))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
               // Console.Write(ex.ToString());
            }
        }
    }
}
