using GASTO.Persistence;

namespace GASTO.BusinessLogic.Solicitud.Create
{
    public class CreateSolicitud : ICreateSolicitud
    {
        DbContextPGDT _context;
        private IDatabaseService<GASTO.Domain.Solicitud> _dbSolicitud;
        public CreateSolicitud()
        {
            _context = new DbContextPGDT();
            _dbSolicitud = new DatabaseService<GASTO.Domain.Solicitud>(_context);
        }

        public void Execute(GASTO.Domain.Solicitud solicitud)
        {
            //GASTO.Domain.Solicitud _solicitud = new GASTO.Domain.Solicitud();
            //_solicitud.EtapaId = competenciaModel.EtapaId;
            //_solicitud.PeriodoId = competenciaModel.PeriodoId;
            //_solicitud.Denominacion = competenciaModel.Denominacion.ToUpper();
            //competencia.Activo = true;
            //competencia.UsuarioCreacion = auditData.User;
            //competencia.IpCreacion = auditData.Ip;
            //competencia.FechaCreacion = auditData.Date;
            //competencia.UsuarioModificacion = auditData.User;
            //competencia.IpModificacion = auditData.Ip;
            //competencia.FechaModificacion = auditData.Date;
            _dbSolicitud.Add(solicitud);
            _context.Save();
        }
    }
}
