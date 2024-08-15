using System.ServiceProcess;

namespace WinServ_RFCGestionGastos
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            RFCGestionViajesService myService = new RFCGestionViajesService();
            myService.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);


            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new RFCGestionViajesService()
            //};
            //ServiceBase.Run(ServicesToRun);
        }

    }
}
