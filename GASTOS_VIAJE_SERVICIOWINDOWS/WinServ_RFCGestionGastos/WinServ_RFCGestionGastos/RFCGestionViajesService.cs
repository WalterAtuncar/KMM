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
        private Timer timer = null;
        private bool ejecutando = false;
        public RFCGestionViajesService()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Enabled = true;
            timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["timeInterval"]);
            //timer.Elapsed += Timer_Elapsed;
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            //timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            //timer.Enabled = true;
            GC.KeepAlive(timer);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (this.ejecutando == false)
            //{
                TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
                StartProcess();
                TraceLog.Write("FINALIZO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
            //}
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
            TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
            TraceLog.Write("--------------------------------------------------");
        }

       
        //public void OnTimer(object sender, ElapsedEventArgs args)
        //{
        //    if (this.ejecutando == false)
        //    {
        //        TraceLog.Write("INICIANDO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        //        StartProcess();
        //        TraceLog.Write("FINALIZO SERVICIO RFC PAGO ANTICIPO !!!! ....... ");
        //    }
        //}

        protected override void OnStop()
        {
            TraceLog.Write("SE DETUVO SERVICIO RFC PAGO ANTICIPO !!!! ....... " +  DateTime.Now);
            TraceLog.Write("--------------------------------------------------");
            //timer.Enabled = false;
            //timer.Stop();
        }



        private void StartProcess()
        {
            this.ejecutando = true;
            ClsServicioRFC_Invoice.ProcesoValidaPago();
        }

        internal void OnDebug()
        {
            OnStart(null);
        }
    }
}
