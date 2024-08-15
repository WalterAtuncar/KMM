using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Komatsu.Sistema.Taxi.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            double tiempoDeEspera = Convert.ToDouble(ConfigurationManager.AppSettings["timeMinutes"]);

            string cadenaFecha = "20180115";

            var anio = cadenaFecha.Substring(0, 4);
            var mes = cadenaFecha.Substring(4, 2);
            var dia = cadenaFecha.Substring(6, 2);

            string fecha = $"{dia}/{mes}/{anio}";

            var fehcaPrueba = Convert.ToDateTime(fecha);


            var fechaServicio = Convert.ToDateTime("18/01/2018 02:28:00 pm");

            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var FechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);

            TimeSpan difference = FechaServidor - fechaServicio;
            int hours = (int)difference.TotalHours;
            var minutes = difference.TotalMinutes;

        }
    }
}
