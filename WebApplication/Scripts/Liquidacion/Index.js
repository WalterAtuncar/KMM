$(document).ready(function () {

    $('#ProveedorCheckBox').prop('checked', false);
    $('#EstadoCheckBox').prop('checked', false);
    $('#SociedadCheckbox').prop('checked', false);
    $('select[name="ProveedorDropDown"]').select2().prop('disabled', true);
    $('select[name="LiquidacionEstadoDropDown"]').prop('disabled', true);

    $('#ProveedorCheckbox').change(function () {
        if ($(this).is(':checked')) {
            $('select[name="ProveedorDropDown"]').prop('disabled', false);
        }
        else {
            $('select[name="ProveedorDropDown"]').prop('disabled', true);
        }
    });

    $('#EstadoCheckbox').change(function () {
        if (this.checked) {
            $('select[name="LiquidacionEstadoDropDown"]').prop('disabled', false);
        }
        else {
            $('select[name="LiquidacionEstadoDropDown"]').prop('disabled', true);
        }
    });

    $('#SociedadCheckbox').change(function () {
        if (this.checked) {
            $('select[name="LiquidacionEstadoDropDown"]').prop('disabled', false);
        }
        else {
            $('select[name="LiquidacionEstadoDropDown"]').prop('disabled', true);
        }
    });

    $('#BuscarLiqButton').click(function () {
        $('#jqGrid').jqGrid('setGridParam', {
            page: 1,
            url: getURLGet(),
            ajaxGridOptions: {
                beforeSend: function (xhr) {
                    fn_BloquearPantalla();
                },
                complete: function (xhr) {
                    fn_DesbloquearPantalla();
                },
            }
        }).trigger('reloadGrid');
    });

    paginado();
     
    var dataCentroCostoComboString = '';
    $.getJSON(url.GetListCentroCostoCombo, function (data, status) {
        data.forEach(function (currentValue, index, array) {
            dataCentroCostoComboString = dataCentroCostoComboString + data[index].Codigo + ':' + data[index].Denominacion + ';';
        });
        window.dataCentroCostoComboString = dataCentroCostoComboString;
    });
});

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

function paginado() {
    $('#jqGrid').jqGrid({
        url: getURLGet(),
        datatype: 'json',
        mtype: 'GET',
        colNames: ['Opciones', 'Código', 'Fecha', 'Proveedor', 'Total S/.', 'Estado', 'Asiento Contable', 'Ejercicio', 'Dia-Hora', 'Hora-Registro', 'FechaFactura','CuentaMayor'],
        colModel: [
            {
                key: false,
                name: 'ID',
                index: 'Options',
                align: 'center',
                width: '110px',
                fixed: false,
                formatter: formatoOpciones,
                formatoptions: { keys: true, }
            },
            { key: false, name: 'Codigo', index: 'Codigo', align: 'left', sortable: true },
            { key: false, name: 'FechaPaginadoStr', index: 'FechaPaginadoStr', align: 'center' },
            { key: false, name: 'RazonSocial', index: 'RazonSocial', align: 'left' },
            { key: false, name: 'Total', index: 'Total', align: 'center', formatter: formatoTotal },
            { key: false, name: 'EstadoNombre', index: 'EstadoNombre', align: 'center' },
            { key: false, name: 'AsientoContable', index: 'AsientoContable', align: 'center' },
            { key: false, name: 'Ejercicio', index: 'Ejercicio', align: 'center' },
            { key: false, name: 'DiaRegistro', index: 'DiaRegistro', align: 'center' },
            { key: false, name: 'HoraRegistro', index: 'HoraRegistro', align: 'center' },
            { key: false, name: 'FechaFacturaPaginadoStr', index: 'FechaFacturaPaginadoStr', align: 'center' },
            { key: false, name: 'CuentaMayor', index: 'CuentaMayor', align: 'center' },
            
        ],
        pager: $('#jqControls'),
        rowNum: 10,
        rowList: [10, 20, 30, 40, 50],
        height: '100%',
        viewrecords: true,
        //caption: 'Students Records',
        //emptyrecords: 'No Students Records are Available to Display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            //Id: "0"
        },
        autowidth: true,
        multiselect: false,
        rownumbers: true,
        cmTemplate: { sortable: false },
        loadBeforeSend: function () {
            fn_BloquearPantalla();
        },
        gridComplete: function () {
            $('[data-toggle=confirmation]').confirmation({
                rootSelector: '[data-toggle=confirmation]',
                onConfirm: function () {
                    var pid = $(this).attr('data-pid');
                    Cancelar(pid)
                },
            });
        },
        loadComplete: function (data) {
            fn_DesbloquearPantalla();
            //$('[data-toggle="tooltip"]').tooltip();
        },
    });
}

var formatoOpciones = function (cellvalue, options, rowObject) {
    var rutaFile = rowObject.NombreFile == null ? '' : '<a href="/Liquidacion/DowloadFile?nombreFile='+ rowObject.NombreFile +'" class="fa fa-download" title = "Descargar Documentos"></a>'

    if (rowObject.Estado == 2 || rowObject.Estado == 3 ) {
        return '<a href="#" id="idEnviarDocumentos" data-pid ="' + cellvalue + '"  class="fa fa-envelope" title = "Enviar Documentos"></a>' + '&nbsp;&nbsp;&nbsp;' + rutaFile;
    } else {
        return '<a href="#" id="LiquidarButton" data-pid ="' + cellvalue + '"  class="fa fa-pencil" title = "Liquidar"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a style="cursor: pointer;" id="CancelarButton" data-toggle="confirmation" data-container="body" data-popout="true" data-title="¿Está seguro de cancelar la liquidación?" data-singleton="true" data-placement="right" data-btnOkLabel="Sí" data-btnCancelLabel="Cancelar" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-remove-sign"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="#" id="idEnviarDocumentos" data-pid ="' + cellvalue + '"  class="fa fa-envelope" title = "Enviar Documentos"></a>' + '&nbsp;&nbsp;&nbsp;' +rutaFile;
    }
    
}

var formatoTotal = function (cellValue, options, rowObject) {
    return parseFloat(cellValue).toFixed(2).toString();
}

$('body').on('click', '#LiquidarButton', function (e) {
    e.preventDefault();
    var pid = $(this).attr('data-pid');
    $('#frm-liquidar').trigger('reset');
    fn_BloquearPantalla();
    $('#modal-liquidar .modal-content').empty();
    $('#modal-liquidar .modal-content').load(url.GetLiquidarModal + '?id=' + pid, function () {
        fn_DesbloquearPantalla();
    });
    $('#modal-liquidar').modal('show');
});

$('body').on('click', '#idEnviarDocumentos', function (e) {
    e.preventDefault();
    var pid = $(this).attr('data-pid');
    fn_BloquearPantalla();
    $('#modal-liquidar .modal-content').empty();
    $('#modal-liquidar .modal-content').load(url.GetPartialCorreo + '?idLiquidacion=' + pid, function () {
        fn_DesbloquearPantalla();
    });
    $('#modal-liquidar').modal('show');
});

$('body').on('click', '#btn-enviar-correo-documentos', function (e) {
    e.preventDefault();
    fn_BloquearPantalla();
    $.ajax({
        url: $('#frm-descargar-documento').attr('action'),
        data: $('#frm-descargar-documento').serialize(),
        type: 'POST',
        async: false,
        cache: false,
        success: function (data) {
            toastr['success'](data);
            $('#modal-liquidar').modal('hide');
            fn_DesbloquearPantalla();
        },
        error: function (request, status, error) {
            toastr["error"]('Ocurrió un error en el servicio');
            fn_DesbloquearPantalla();
        }
    });
});

var enviarExcel = function (pid) {
    fn_BloquearPantalla();
    $.ajax({
        url:'/Liquidacion/GenerarReporte',
        data: { idLiquidacion : pid},
        type: 'POST',
        async: false,
        cache: false,
        success: function (data) {
            toastr['success'](data);
            fn_DesbloquearPantalla();
        }
    });
}

function getURLModalLiquidar(LiquidacionID) {
    return url.GetSolicitudByLiquidacionID + '?LiquidacionID=' + LiquidacionID
}

function Cancelar(id) {
    fn_BloquearPantalla();
    $.ajax({
        url: url.PostCancelar,
        data: { idLiquidacion : id},
        type: 'POST',
        async: false,
        cache: false,
        success: function (data) {
            if (data.Success) {
                toastr['success']('Cancelado correctamente');
                $('#jqGrid').jqGrid('clearGridData');
                $('#jqGrid').trigger('reloadGrid');
            } else {
                    toastr["error"]('Ocurrio un error inesperado');
            }
            fn_DesbloquearPantalla();
        }
    });
}