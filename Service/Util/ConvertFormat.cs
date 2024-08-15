using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Service.Util
{
    public class ConvertFormat
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

        public static DateTime GetCurrentDateTime()
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            var FechaServidor = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);

            return FechaServidor;
        }
    }
}
