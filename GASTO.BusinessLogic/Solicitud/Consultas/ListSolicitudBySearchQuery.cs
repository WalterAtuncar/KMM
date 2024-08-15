using GASTO.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GASTO.BusinessLogic.Solicitud.Consultas
{
    public class ListPeriodoBySearchQuery 
    {
        private IDatabaseService<GASTO.Domain.Solicitud> _dbSolicitud;


        public ListPeriodoBySearchQuery()
        {
            DbContextPGDT _context = new DbContextPGDT();
            _dbSolicitud = new DatabaseService<GASTO.Domain.Solicitud>(_context);
           
        }

        public List<ListSolicitudItem> Execute(
            int page,
            int pageSize,
            out int total)
        {
            if (page < 0) page = 0;
            if (page > 0) page -= 1;

            DateTime _fechaInicio = DateTime.Now, _fechaFin = DateTime.Now;
            var solicitudesDb =
                _dbSolicitud
                .GetListWhere(x =>
                    (x.Activo == true)
                )
                .OrderByDescending(x => x.IdSolicitud)
                .Skip(page * pageSize)
                .Take(pageSize);

            List<ListSolicitudItem> solicitudes = new List<ListSolicitudItem>();
            ListSolicitudItem item = null;

            foreach (var solicitud in solicitudesDb)
            {
                item = new ListSolicitudItem();
                item.IdSolicitud = solicitud.IdSolicitud;
                item.IdUsuario = solicitud.IdUsuario;
                item.IdCentroCosto = solicitud.IdCentroCosto == null ? 0 : solicitud.IdCentroCosto;
                item.IdAprobador = solicitud.IdAprobador == null ? 0 : solicitud.IdAprobador;
                item.IdSucursal = solicitud.IdSucursal == null ? 0 : solicitud.IdSucursal;
                item.IdDestino = solicitud.IdDestino == null ? 0 : solicitud.IdDestino;
                item.IdTipo = solicitud.IdTipo == null ? 0 : solicitud.IdTipo;
                item.Motivo = solicitud.Motivo == null ? "" : solicitud.Motivo;
                item.FechaSalida = solicitud.FechaSalida == null ? DateTime.Now : solicitud.FechaSalida;
                item.FechaRetorno = solicitud.FechaRetorno == null ? DateTime.Now : solicitud.FechaRetorno;
                item.AsientoContable = solicitud.AsientoContable == null ? "" : solicitud.AsientoContable;

                item.Competencia = solicitud.Competencia == null ? "" : solicitud.Competencia;
                item.CtaBancaria = solicitud.CtaBancaria == null ? "" : solicitud.CtaBancaria;
                item.IdMoneda = solicitud.IdMoneda == null ? 0 : solicitud.IdMoneda;
                item.ImporteSolicitud = solicitud.ImporteSolicitud == null ? 0 : solicitud.ImporteSolicitud;
                item.Comentario = solicitud.Comentario == null ? "" : solicitud.Comentario;
                item.IdUsuarioAprobador = solicitud.IdUsuarioAprobador == null ? 0 : solicitud.IdUsuarioAprobador;
                item.FechaRegistro = solicitud.FechaRegistro == null ? DateTime.Now : solicitud.FechaRegistro;
                item.IdUsuarioM = solicitud.IdUsuarioM == null ? 0 : solicitud.IdUsuarioM;
                item.FechaAccion = solicitud.FechaAccion == null ? DateTime.Now : solicitud.FechaAccion;
                item.IdSociedad = solicitud.IdSociedad == null ? "" : solicitud.IdSociedad;
                item.Activo = solicitud.Activo;
               
                solicitudes.Add(item);
            }
            total = _dbSolicitud.GetListWhere(x =>
                (x.Activo == true)
              )
            .Count;
            return solicitudes;
        }

    }
}
