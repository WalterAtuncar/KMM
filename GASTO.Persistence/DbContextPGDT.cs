
using GASTO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace GASTO.Persistence
{
    public class DbContextPGDT : DbContext
    {
        public DbContextPGDT()
            : base("Entities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("Gastos");
            base.OnModelCreating(modelBuilder);
        }

        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public void Save()
        {
            this.SaveChanges();
        }

        #region Tables

        //public IDbSet<Solicitud> Solicitud { get; set; }

        //public IDbSet<AsociacionPersonalPeriodo> AsociacionPersonalPeriodo { get; set; }

        //public IDbSet<Etapa> Etapa { get; set; }

        //public IDbSet<Competencia> Competencia { get; set; }

        //public IDbSet<Nivel> Nivel { get; set; }

        //public IDbSet<Comportamiento> Comportamiento { get; set; }

        //public IDbSet<EtapaPeriodoInstruccion> EtapaPeriodoInstruccion { get; set; }

        //public IDbSet<CompetenciaPonderado> CompetenciaPonderado { get; set; }

        //public IDbSet<Ponderado> Ponderado { get; set; }

        //public IDbSet<PonderadoEtapaPeriodo> PonderadoEtapaPeriodo { get; set; }

        //public IDbSet<PreCalificacion> PreCalificacion { get; set; }

        //public IDbSet<TablaResultadoEtapa1> TablaResultadoEtapa1 { get; set; }

        //public IDbSet<CompetenciaEsperadoAlcanzado> CompetenciaEsperadoAlcanzado { get; set; }

        //public IDbSet<PreCalificacionEvaluacion> PreCalificacionEvaluacion { get; set; }

        //public IDbSet<ComportamientoEvaluacion> ComportamientoEvaluacion { get; set; }

        //public IDbSet<EstadoTecnicoEvaluacion> EstadoTecnicoEvaluacion { get; set; }

        //public IDbSet<CompetenciaTecnicoEvaluacionEtapa2> CompetenciaTecnicoEvaluacionEtapa2 { get; set; }

        //public IDbSet<FeedbackEtapa4> FeedbackEtapa4 { get; set; }

        //public IDbSet<AcuerdosActividadEtapa4> AcuerdosActividadEtapa4 { get; set; }

        //public IDbSet<MotivoCambioEvaluacion> MotivoCambioEvaluacion { get; set; }

        //public IDbSet<MaestroTipo> MaestroTipo { get; set; }

        //public IDbSet<MaestroTipoDetalle> MaestroTipoDetalle { get; set; }

        //public IDbSet<RevisionEvaluacionTecnico> RevisionEvaluacionTecnico { get; set; }

        //public IDbSet<ComentarioEvaluacionTecnico> ComentarioEvaluacionTecnico { get; set; }

        //public IDbSet<RankingPersonal> RankingPersonal { get; set; }

        #endregion

    }
}
