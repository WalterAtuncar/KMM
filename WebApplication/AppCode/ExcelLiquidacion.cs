using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.AppCode;
using Data;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using System.IO;

namespace WebApplication.AppCode
{
    public class ExcelLiquidacion
    {
        public string ExcelRoute { get; set; }
        private List<usp_Solicitud_GetByLiquidacionIDForExcel_Result> _Solicitud;
        public ExcelLiquidacion(List<usp_Solicitud_GetByLiquidacionIDForExcel_Result> Solicitud)
        {
            _Solicitud = Solicitud;
        }
        public bool GenerateExcel()
        {
            try
            {
                HSSFWorkbook workbook = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Servicios");

                var rowHeader = sheet.CreateRow(0);
                rowHeader.CreateCell(0).SetCellValue("Tipo Servicio");
                rowHeader.CreateCell(1).SetCellValue("Tipo Pago");
                rowHeader.CreateCell(2).SetCellValue("Proveedor");
                rowHeader.CreateCell(3).SetCellValue("Fecha Servicio");
                rowHeader.CreateCell(4).SetCellValue("Cod. Centro de Costo");
                rowHeader.CreateCell(5).SetCellValue("Nom. Centro de Costo");
                rowHeader.CreateCell(6).SetCellValue("O/S");
                rowHeader.CreateCell(7).SetCellValue("Total Servicio");
                rowHeader.CreateCell(8).SetCellValue("Total Gasto");
                rowHeader.CreateCell(9).SetCellValue("Total General");
                rowHeader.CreateCell(10).SetCellValue("Sociedad");


                //Generate data on sheet

                for (int i = 0; i < _Solicitud.Count; i++)
                {
                    var rowDetail = sheet.CreateRow(i + 1);
                    rowDetail.CreateCell(0).SetCellValue(_Solicitud[i].TipoServicioNombre);
                    rowDetail.CreateCell(1).SetCellValue(_Solicitud[i].TipoPagoNombre);
                    rowDetail.CreateCell(2).SetCellValue(_Solicitud[i].RazonSocial);
                    rowDetail.CreateCell(3).SetCellValue(_Solicitud[i].FechaServicio.ToString());
                    rowDetail.CreateCell(4).SetCellValue(_Solicitud[i].CentroCostoAfectoCodigoSap);
                    rowDetail.CreateCell(5).SetCellValue(_Solicitud[i].CentroCostoAfectoDescripcion);
                    rowDetail.CreateCell(6).SetCellValue(_Solicitud[i].NroOrdenServicio);
                    rowDetail.CreateCell(7).SetCellValue(_Solicitud[i].TotalServicio.ToString());
                    rowDetail.CreateCell(8).SetCellValue(_Solicitud[i].TotalGasto.ToString());
                    rowDetail.CreateCell(9).SetCellValue(_Solicitud[i].TotalGeneral.ToString());
                    rowDetail.CreateCell(10).SetCellValue(_Solicitud[i].Sociedad);
                }

                string Path = Util.ServerPath("ExcelTEMP");
                string FileName = _Solicitud[0].Codigo.ToString() + ".xls";
                ExcelRoute = Path + @"\" + FileName;
                using (var fs = new FileStream(ExcelRoute, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}