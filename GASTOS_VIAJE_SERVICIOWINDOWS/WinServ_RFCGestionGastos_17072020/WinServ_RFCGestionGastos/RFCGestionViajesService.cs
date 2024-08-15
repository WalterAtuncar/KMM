using Service.Util;
using ServicioLibreria;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace WinServ_RFCGestionGastos
{
    public partial class RFCGestionViajesService : ServiceBase
    {
        //private Timer timer = null;
        //private bool ejecutando = false;
        //public RFCGestionViajesService()
        //{
        //    InitializeComponent();
        //}

        //protected override void OnStart(string[] args)
        //{
        //    timer = new Timer();
        //    timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timeInterval"]);
        //    //timer.Elapsed += Timer_Elapsed;
        //    timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
        //    timer.Enabled = true;
        //}

        ////private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        ////{
        ////    if (this.ejecutando == false)
        ////    {
        ////        TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        ////        StartProcess();
        ////        TraceLog.Write("FINALIZO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        ////    }
        ////}
        //public void OnTimer(object sender, ElapsedEventArgs args)
        //{
        //    if (this.ejecutando == false)
        //    {
        //        TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        //        StartProcess();
        //        TraceLog.Write("FINALIZO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        //    }
        //}

        //protected override void OnStop()
        //{
        //    timer.Enabled = false;
        //    timer.Stop();
        //}



        //private void StartProcess()
        //{
        //    this.ejecutando = true;
        //    ClsServicioRFC_Invoice.ProcesoValidaPago();
        //}

        private Timer timer = null;
        private bool ejecutando = false;

        public RFCGestionViajesService()
        {
            InitializeComponent();
        }

#if DEBUG
        public void OnDebug()
        {
            // Aquí puedes colocar cualquier código de inicialización o depuración adicional

            // Llama al método que normalmente se ejecuta al iniciar el servicio
            StartService();
        }
#else
        protected override void OnStart(string[] args)
        {
            StartService();
        }
#endif

        protected override void OnStop()
        {
            timer.Enabled = false;
            timer.Stop();
        }

        private void StartService()
        {
            timer = new Timer();
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timeInterval"]);
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Enabled = true;
        }

        private void OnTimer(object sender, ElapsedEventArgs args)
        {
            if (this.ejecutando == false)
            {
                TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
                StartProcess();
                TraceLog.Write("FINALIZO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
            }
        }

        private void StartProcess()
        {
            this.ejecutando = true;
            ClsServicioRFC_Invoice.ProcesoValidaPago();
        }
    }


}

