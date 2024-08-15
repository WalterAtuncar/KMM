using SAP.Middleware.Connector;
using System.Configuration;

namespace WebApiAplications.Util
{
    public class ECCDestinationConfig : IDestinationConfiguration
    {
        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {

            RfcConfigParameters parms = new RfcConfigParameters();
            if (destinationName.Equals("mySAPdestination"))
            {
                parms.Add(RfcConfigParameters.Client, ConfigurationManager.AppSettings["CLIENT"]);
                parms.Add(RfcConfigParameters.User, ConfigurationManager.AppSettings["SAP-RFC-USER"]);
                parms.Add(RfcConfigParameters.Password, ConfigurationManager.AppSettings["SAP-RFC-PASSWORD"]);
                parms.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["APPSERVERHOST"]);
                parms.Add(RfcConfigParameters.Language, ConfigurationManager.AppSettings["LANGUAGE"]);
                parms.Add(RfcConfigParameters.SystemNumber, ConfigurationManager.AppSettings["SYSTEMNUMBER"]);
            }
            return parms;
        }
    }
}