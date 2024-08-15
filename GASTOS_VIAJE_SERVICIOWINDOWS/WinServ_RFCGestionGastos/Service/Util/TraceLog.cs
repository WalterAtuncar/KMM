using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Util
{
    public class TraceLog
    {
        public static void Write(string msg, string ftpPath = "", string fileName = "")
        {
            try
            {
                var path = @"" + ConfigurationManager.AppSettings["pathTraceCarga"];
                var _fName = ConfigurationManager.AppSettings["fileNameTrace"];
                var newFileName = string.Concat(_fName, DateTime.Now.Date.ToString("ddMMyyyy"));

                var fullName = string.Concat(path + newFileName + ".txt");
                if (!File.Exists(fullName))
                {
                    var x = File.Create(fullName);
                    x.Close();
                }
                
                if (File.Exists(fullName))
                {
                   
                    var fecha = DateTime.Now.ToString() + "[INFO] ";
                    var message = fecha + ftpPath + " [MSG] " + msg+Environment.NewLine; 
                    using (StreamWriter sw = new StreamWriter(fullName, true))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
              string m=ex.Message.ToString();
            }
        }
    }

    //public struct MessageText
    //{
    //    public const string Conectando = "Conectando con el servidor FTP.";
    //    public const string ConexionCorrecta = "Se conectó correctamente al servidor FTP.";
    //    public const string ErrorDeConexion = "Ocurrio un error al intentar acceder al servidor FTP.";
    //    public const string InicioDeDescarga = "Se está inciando el proceso de descarga del archivo.";
    //    public const string ArchivoDescargadoCorrectamente = "El archivo fue descargador de manera correcta.";
    //    public const string OcurrioErrorEnLaDescarga = "Ocurrio un error en la descarga del archivo";

    //    public const string LeyendoTxt = "Leyendo archivo de texto de carpeta compartida.";
    //    public const string InsertandoTxt = "Insertando contenido de archivo de texto en Base de Datos.";
    //    public const string ProcesoInsertTxtCompletado = "Se inserto correctamente el contenido del archivo de texto en la Base de Datos.";

    //}
}
