using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GASTO.Common
{
    public static class HelperPaginacion
    {
        public static void SetCounts<T>(
            this List<T> sourceList, 
            int? pageSize, 
            out int recordCount, 
            out int pagesCount)
        {
            recordCount = default(int);
            pagesCount = default(int);

            try
            {
                if (sourceList == null || !sourceList.Any()) return;

                T item = sourceList.FirstOrDefault();

                dynamic objDynamic = (dynamic)item;
                double allRows = Convert.ToDouble(objDynamic.TotalRows);
                double dPageSize = Convert.ToDouble(pageSize);

                pagesCount = Convert.ToInt32(Math.Ceiling(allRows / dPageSize));
                recordCount = Convert.ToInt32(allRows);
            }
            catch (Exception ex)
            {
            }

        }
    }
}
