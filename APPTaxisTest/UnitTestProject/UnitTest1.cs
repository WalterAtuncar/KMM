using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Servicio.Util;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var formatoFecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            string cadena = null;


            if (string.IsNullOrEmpty(cadena)) formatoFecha = "Paso";
            else formatoFecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


            Console.WriteLine(formatoFecha);
        }
    }
}
