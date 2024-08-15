using ServicioLibreria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppWinServ_RFCGestionViajes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsServicioRFC_Invoice.ProcesoValidaPago(); // aduana
            timer1.Start();
            label1.Text = "Iniciando Servicio.";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 120000;
            ClsServicioRFC_Invoice.ProcesoValidaPago();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label1.Text = "Deteniendo Servicio";
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
