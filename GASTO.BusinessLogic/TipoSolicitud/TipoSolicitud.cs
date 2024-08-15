﻿using System;
using System.Collections.Generic;
using System.Linq;

using BE_GASTO = GASTO.Domain;
using DA_GASTO = GASTO.DataAccess;

namespace GASTO.BusinessLogic.TipoSolicitud
{
    public class TipoSolicitud
    {
        public ICollection<BE_GASTO.TipoSolicitud> List()
        {
            List<BE_GASTO.TipoSolicitud> lisQuery = null;
            try
            {
                IEnumerable<BE_GASTO.TipoSolicitud> entQuery = new DA_GASTO.TipoSolicitud().List();
                if (entQuery != null)
                {
                    lisQuery = entQuery.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lisQuery;
        }
    }
}
