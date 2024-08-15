(function () {
    $(document).ready(function () {
        $('#tbl-solicitudes-registrar').jqGrid({
            datatype: 'local',
            data: [],
            colNames: ['Opciones', 'FechaServicio', 'Codigo Solicitud','Situacion Servicio', 'Hora Servicio','Hora Inicio', 'Centro Costo', 'Beneficiado', 'Documento', 'O/S', 'Centro Costo', 'Aprobador', 'Tarifa', 'T.Espera', 'T.DesvRuta', 'T.Peaje', 'T.Estacionamiento','T.Desplazamiento', 'Total General', 'Indicador'],
            colModel: [
                {
                    key: true,
                    name: 'ID',
                    index: 'Options',
                    align: 'center',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { keys: true, },
                    width: '110px'
                },

                { key: false, name: 'FechaServicioStr', index: 'FechaServicioStr', align: 'center', width: '100px' },
                { key: false, name: 'CodigoSolicitud', index: 'CodigoSolicitud', align: 'center', width: '120px' },
                { key: false, name: 'SituacionServicioNombre', index: 'SituacionServicio', align: 'center', width: '120px' },
                { key: false, name: 'HoraServicio', index: 'HoraServicio', align: 'center', width: '85px' },
                { key: false, name: 'HoraInicio', index: 'HoraInicio', align: 'center', width: '85px' },
                { key: false, name: 'CentroCostoBeneficiado', index: 'CentroCostoBeneficiado', align: 'center', width: '83px' },
                { key: false, name: 'Beneficiado', index: 'Beneficiado', align: 'left', width: '284px' },
                { key: false, name: 'Documento', index: 'Documento', align: 'center', width: '75px' },
                { key: false, name: 'OrdenServicio', index: 'OrdenServicio', align: 'center', width: '98px' },
                { key: false, name: 'CentroCostoAfecto', index: 'CentroCostoAfecto', align: 'center', width: '100px' },
                { key: false, name: 'Aprobador', index: 'Aprobador', align: 'center', width: '155px' },
                { key: false, name: 'PrecioTarifaStr', index: 'PrecioTarifaStr', align: 'center', width: '67px', class: 'PrecioTarifal' },
                { key: false, name: 'PrecioEsperaStr', index: 'PrecioEsperaStr', align: 'center', width: '67px' },
                { key: false, name: 'PrecioDesvioRutaStr', index: 'PrecioDesvioRutaStr', align: 'center', width: '70px' },
                { key: false, name: 'PrecioPeajeStr', index: 'PrecioPeajeStr', align: 'center', width: '67px' },
                { key: false, name: 'PrecioEstacionamientoStr', index: 'PrecioEstacionamientoStr', align: 'center', width: '105px' },
                { key: false, name: 'PrecioDesplazamientoStr', index: 'PrecioDesplazamientoStr', align: 'center', width: '105px' },
                { key: false, name: 'TotalGeneralStr', index: 'TotalGeneralStr', align: 'center', width: '100px' },
                { key: false, name: 'IndicadorNegociado', index: 'IndicadorNegociado', hidden: true }
            ],
            rowNum: 20,
            rowList: [10, 20, 30, 40, 50],
            height: '100%',
            pager: '#jqControls',
            autowidth: true,
            viewrecords: true,
            rownumbers: true,
            cmTemplate: { sortable: false }
        });
    });

    var formatoOpciones = function (cellvalue, options, rowObject) {

        var btnTarifa = '';
        if (rowObject.IndicadorNegociado == 1) {
            btnTarifa = '<a id="btn-edit-tarifa" data-pid ="' + cellvalue + '"  class="fa fa-pencil" title = "Negociar Tarifa"></a>' + '&nbsp;&nbsp;&nbsp;' +
                '<a id="btn-flag" class="fa fa-flag" style ="color:red;" title = "Con Observaciones"></a>' + '&nbsp;&nbsp;&nbsp';
        } else {
            btnTarifa = '<a class="fa fa-flag-o" id="btn-flag" title = "Sin Observaciones"></a>' + '&nbsp;&nbsp;&nbsp';
        }


        return '<a id="QuitarSolicitudButton" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-trash" title = "Quitar Solicitud"></a>' + '&nbsp;&nbsp;&nbsp;' +
            btnTarifa + '&nbsp;&nbsp;' + '<a href="#" id="btn-edit-gasto" data-pid="' + cellvalue + '"  data-object="' + rowObject + '" class="fa fa-usd" title = "Editar Gastos"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="#" id="btn-bitacora" data-pid="' + cellvalue + '"  class="fa fa-bars" title = "Historial"></a>';
    }

    $('body').on('click', '#btn-bitacora', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirSolicitudDetalle(pid);
    });
    //
    //Permite editar los gastos
    $('body').on('click', '#btn-edit-gasto', function (e) {
        e.preventDefault();
        fn_BloquearPantalla();
        var pid = $(this).attr('data-pid');
        var valueRow = $('#tbl-solicitudes-registrar').jqGrid('getRowData', pid);

        var solicitudLiquidacionDetalle = new Object();
        solicitudLiquidacionDetalle.ID = pid;
        solicitudLiquidacionDetalle.CodigoSolicitud = valueRow.CodigoSolicitud;
        solicitudLiquidacionDetalle.OrdenServicio = valueRow.OrdenServicio;
        solicitudLiquidacionDetalle.CentroCostoAfecto = valueRow.CentroCostoAfecto;
        solicitudLiquidacionDetalle.PrecioTarifa = parseFloat(valueRow.PrecioTarifaStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.PrecioEspera = parseFloat(valueRow.PrecioEsperaStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.PrecioDesvioRuta = parseFloat(valueRow.PrecioDesvioRutaStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.PrecioPeaje = parseFloat(valueRow.PrecioPeajeStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.PrecioEstacionamiento = parseFloat(valueRow.PrecioEstacionamientoStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.PrecioDesplazamiento = parseFloat(valueRow.PrecioDesplazamientoStr).toFixed(2).replace(',', '');
        solicitudLiquidacionDetalle.TotalGeneral = parseFloat(valueRow.TotalGeneralStr).toFixed(2).replace(',', '');

        var modelo = solicitudLiquidacionDetalle;

        $('#modal-solicitud .modal-content').empty();
        $('#modal-solicitud .modal-content').load('/Liquidacion/GetGastosAdicionales', { idSolicitud: pid, model: modelo}, function () {
            $('#modal-solicitud').modal('show');
            fn_DesbloquearPantalla();
        });
    });

    $('body').on('click', "#QuitarSolicitudButton", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');

        $('#tbl-solicitudes-registrar').jqGrid('delRowData', pid);
        $("#tbl-solicitudes-registrar").trigger('reloadGrid');

        var totalGeneralGrid = 0;
        var data = $("#tbl-solicitudes-registrar").jqGrid('getGridParam', 'data');
        data.forEach(function (Row) {
            totalGeneralGrid += parseFloat(Row.TotalGeneralStr);
        });
        $('#TotalGeneralLabel').html(totalGeneralGrid.toFixed(2).toString());
        $('#CantidadLabel').html(data.length);
    });
    //
    //Permite editar la tarifa
    $('body').on('click', '#btn-edit-tarifa', function (e) {
        e.preventDefault();
        fn_BloquearPantalla();
        var pid = $(this).attr('data-pid');

        $('#modal-solicitud .modal-content').empty();
        $('#modal-solicitud .modal-content').load('/Liquidacion/GetNegociablePartial', { idSolicitud: pid}, function () {
            $('#modal-solicitud').modal('show');
            fn_DesbloquearPantalla();
        });
    });

    $('#AgregarServiciosButton').click(function (e) {
        e.preventDefault();
        abrirBuscarModal($('select[name="ProveedorDropDown"]').val());
    });

    $('#ddlProveedorTaxi').change(function () {
        $('#tbl-solicitudes-registrar').jqGrid('clearGridData');
        $('#tbl-solicitudes-registrar').jqGrid('setGridParam', {
            datatype: 'local',
            data: []
        }).trigger('reloadGrid');
        $('#TotalGeneralLabel').html('0');
        $('#CantidadLabel').html('0');
    });

    $('#ddlSociedad').change(function () {
        $('#tbl-solicitudes-registrar').jqGrid('clearGridData');
        $('#tbl-solicitudes-registrar').jqGrid('setGridParam', {
            datatype: 'local',
            data: []
        }).trigger('reloadGrid');
        $('#TotalGeneralLabel').html('0');
        $('#CantidadLabel').html('0');
    });

    var abrirBuscarModal = function (id) {
        fn_BloquearPantalla();
        $('#modal-solicitud .modal-content').empty();
        $('#modal-solicitud .modal-content').load('/Liquidacion/BuscarSolicitud', { id: id }, function () { fn_DesbloquearPantalla();});
        $('#modal-solicitud').modal('show');
        if (window.jsCharged) {
            $('#tbl-solicitudes').jqGrid('clearGridData');
            $('#tbl-solicitudes').jqGrid('setGridParam', {
                url: getURLGet(),
            }).trigger('reloadGrid');
        }
    };

    function getURLGet() {
        var ProveedorTaxiID = 0;
        var Estado = 0;
        if ($('#ProveedorCheckbox').is(':checked')) {
            ProveedorTaxiID = $('select[name="ProveedorDropDown"]').val();
        }
        else {
            ProveedorTaxiID = 0;
        }

        if ($('#EstadoCheckbox').is(':checked')) {
            Estado = $('select[name="LiquidacionEstadoDropDown"]').val();
        }
        else {
            Estado = 0;
        }
        var FechaInicioTxt = $('#FechaInicioTextBox').val();
        var FechaFinTxt = $('#FechaFinTextBox').val();
        return url.GetListLiquidacionPaginado + '?Estado=' + Estado + '&ProveedorTaxiID=' + ProveedorTaxiID + '&FechaInicio=' + FechaInicioTxt + '&FechaFin=' + FechaFinTxt;
    }

    var abrirSolicitudDetalle = function (pid) {
        fn_BloquearPantalla();
        $('#modal-sol-detalle .modal-content').empty();
        $('#modal-sol-detalle .modal-content').load(url.GetSolicitudDetalle, { idSolicitud: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-sol-detalle').modal('show');
    }
    //
    //permite grabar los datos
    $('#GuardarButton').click(function (e) {
        e.preventDefault();
        fn_BloquearPantalla();
        var countNegociableTarifa = parseFloat(0);
        var rows = null;
        var LiquidacionDetalleLis = []
        rows = $("#tbl-solicitudes-registrar").jqGrid('getGridParam', 'data');
        rows.forEach(function (valueRow) {
            if (valueRow.IndicadorNegociado === 1) {
                countNegociableTarifa = + countNegociableTarifa + 1;
            };
        })
        if (countNegociableTarifa === 0) {
            rows = $("#tbl-solicitudes-registrar").jqGrid('getGridParam', 'data');
            rows.forEach(function (valueRow) {
                var SolicitudID = valueRow.ID;
                var OrdenServicio = valueRow.OrdenServicio;
                var CentroCostoAfecto = valueRow.CentroCostoAfecto;
                var PrecioTarifa = valueRow.PrecioTarifaStr;
                var PrecioEspera = valueRow.PrecioEsperaStr;
                var PrecioDesvioRuta = valueRow.PrecioDesvioRutaStr;
                var PrecioPeaje = valueRow.PrecioPeajeStr;
                var PrecioEstacionamiento = valueRow.PrecioEstacionamientoStr;
                var PrecioDesplazamiento = valueRow.PrecioDesplazamientoStr;
                var TotalGeneral = valueRow.TotalGeneralStr;
                LiquidacionDetalleLis.push({
                    'SolicitudID': SolicitudID,
                    'OrdenServicio': OrdenServicio,
                    'CentroCostoAfecto': CentroCostoAfecto,
                    'PrecioTarifa': PrecioTarifa,
                    'PrecioEspera': PrecioEspera,
                    'PrecioDesvioRuta': PrecioDesvioRuta,
                    'PrecioPeaje': PrecioPeaje,
                    'PrecioEstacionamiento': PrecioEstacionamiento,
                    'PrecioDesplazamiento': PrecioDesplazamiento,
                    'TotalGeneral': TotalGeneral
                })
            });
            var objeto = {
                Fecha: $('#txtFecha').val(),
                ProveedorTaxiID: $('#ddlProveedorTaxi').val(),
                Total: $('#TotalGeneralLabel').text(),
                SociedadID: $('#ddlSociedad').val(),
                Observacion: $('#txtObservacion').text(),
                LiquidacionDetalleLis: LiquidacionDetalleLis
            };
            $.ajax({
                method: 'POST',
                url: '/Liquidacion/Grabar',
                contentType: 'application/json',
                dataType: 'json',
                data: JSON.stringify(objeto),
                beforeSend: function () { fn_BloquearPantalla(); },
            })
            .done(function (data) {
                window.location.href = '/Liquidacion/Index';
            })
            .fail(function (jqXHR, textStatus, error) {
                //console.log(request);
                toastr["error"]('Ocurrio un error en el servicio');
                fn_DesbloquearPantalla();
            })
            .always(function () {
                fn_DesbloquearPantalla();
            });
        } else {
            toastr["warning"]('Existen Solicitudes con Observaciones');
            fn_DesbloquearPantalla();
        }
    });
})();