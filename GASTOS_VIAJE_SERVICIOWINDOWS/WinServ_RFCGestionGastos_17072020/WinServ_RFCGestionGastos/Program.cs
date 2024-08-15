using System;
using System.ServiceProcess;
using System.Threading;

namespace WinServ_RFCGestionGastos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new RFCGestionViajesService()
            //};
            //ServiceBase.Run(ServicesToRun);

#if DEBUG
            // Inicia el servicio en modo depuración directamente desde Visual Studio
            RFCGestionViajesService service = new RFCGestionViajesService();
            service.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            // Inicia el servicio normalmente
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new RFCGestionViajesService()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
