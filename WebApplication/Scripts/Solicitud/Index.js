(function () {

    $(document).ready(function () {
        $('.filtro').attr('disabled', true);
        $('.select2').select2();
        $('#chkCompania').change(function () { checkCompania(this) });
        $('#chkCentroCosto').change(function () { checkCentroCosto(this) });

        $('#chkSituacion').change(function () { checkSituacion(this) });
        $('#chkSolicitud').change(function () { checkSolicitud(this) });
        $('#chkFechaHasta').change(function () { checkFechaHasta(this) });
        $('#chkFechaDesde').change(function () { checkFechaDesde(this) });
        paginado();

        $('#btnBuscarSolicitud').click(function () {
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
    });

    var abrirTracking = function (pid) {
        fn_BloquearPantalla();
        $('#modal-tracking .modal-content').empty();
        $('#modal-tracking .modal-content').load(url.Tracking, { idSolicitud: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-tracking').modal('show');
    }

    var abrirUbicacion = function (pid) {
        fn_BloquearPantalla();
        $('#modal-ubicacion .modal-content').empty();
        $('#modal-ubicacion .modal-content').load(url.Ubicacion, { idSolicitud: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-ubicacion').modal('show');
    }

    var abrirSolicitudDetalle = function (pid) {
        fn_BloquearPantalla();
        $('#modal-sol-detalle .modal-content').empty();
        $('#modal-sol-detalle .modal-content').load(url.GetSolicitudDetalle, { idSolicitud: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-sol-detalle').modal('show');
    }

    var abrirGastoAdicional = function (pid) {
        fn_BloquearPantalla();
        $('#modal-ubicacion .modal-content').empty();
        $('#modal-ubicacion .modal-content').load(url.GetGastoAdicional, { idSolicitud: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-ubicacion').modal('show');
    }

    var abrirCancelar = function (id) {
        fn_BloquearPantalla();
        $('#modal-cancelar .modal-content').empty();
        $('#modal-cancelar .modal-content').load(url.CancelarSolicitud, { idSolicitud: id }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-cancelar').modal('show');
    }

    $('body').on('click', '#btn-estado', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirTracking(pid);
    });

    $('body').on('click', '#btn-bitacora', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirSolicitudDetalle(pid);
    });

    $('body').on('click', '#btn-gastoadicional', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirGastoAdicional(pid);
    });

    $('body').on('click', '#btn-puntoactual', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirUbicacion(pid);
    });

    $('body').on('click', '#btn-cancelar-servicio', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirCancelar(pid);
    });

    $('body').on('click', '#btn-sol-det', function (e) {
        e.preventDefault();
        fn_BloquearPantalla();
        var __$modal_sol_detalle = $('#modal-sol-detalle');
        var $btn = $(this);
        __$modal_sol_detalle.find('.modal-content').empty();
        __$modal_sol_detalle.find('.modal-content').load(
            url.VisualizarDetalle,
            { id: $btn.attr('data-id') },
            function () {
                __$modal_sol_detalle.modal('show');
                fn_DesbloquearPantalla();
            });
    });

    $('body').on('click', '#btn-editar-servicio', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        window.location = "/Solicitud/Editar?id=" + pid;
    });

    var checkCompania = function (element) {
        if (element.checked) {
            $('#ddlCompania').prop('disabled', false);
            $('#ddlCompania').focus();
        } else {
            $('#ddlCompania').prop('disabled', true);
            $('#ddlCompania').val("");
            $('#ddlCompania').trigger("change")
        }
    }

    var checkCentroCosto = function (element) {
        if (element.checked) {
            $('#ddlCentroCosto').prop('disabled', false);
            $('#ddlCentroCosto').focus();
        } else {
            $('#ddlCentroCosto').prop('disabled', true);
            $('#ddlCentroCosto').val("");
            $('#ddlCentroCosto').trigger("change")
        }
    }

    var checkSituacion = function (element) {
        if (element.checked) {
            $('#ddlSituacion').prop('disabled', false);
            $('#ddlSituacion').focus();
        } else {
            $('#ddlSituacion').prop('disabled', true);
            $('#ddlSituacion').val('');
            $('#ddlSituacion').trigger("change")
        }
    }

    var checkSolicitud = function (element) {
        if (element.checked) {
            $('#txtSolicitud').prop('disabled', false);
            $('#txtSolicitud').focus();
        } else {
            $('#txtSolicitud').prop('disabled', true);
            $('#txtSolicitud').val('');
        }
    }

    var checkFechaHasta = function (element) {
        if (element.checked) {
            $('#txtFechaHasta').prop('disabled', false);
            //$('#txtFechaHasta').focus();
        } else {
            $('#txtFechaHasta').prop('disabled', true);
            $('#txtFechaHasta').val('');
        }
    }

    var checkFechaDesde = function (element) {
        if (element.checked) {
            $('#txtFechaDesde').prop('disabled', false);
            //$('#txtFechaDesde').focus();
        } else {
            $('#txtFechaDesde').prop('disabled', true);
            $('#txtFechaDesde').val('');
        }
    }

    function getURLGet() {

        var sociedad = $('#ddlCompania').val();
        var fechaDesde = $('#txtFechaDesde').val();
        var fechaHasta = $('#txtFechaHasta').val();
        var situacion = $('#ddlSituacion').val();
        var nroSolicitud = $('#txtSolicitud').val();
        var centroCosto = $('#ddlCentroCosto').val();


        return url.GetListSolicitud + '?sociedad=' + sociedad + '&fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&nroSolicitud=' + nroSolicitud + '&situacion=' + situacion + '&centroCosto=' + centroCosto;
    }

    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListSolicitud,
            datatype: 'JSON',
            mtype: 'GET',
            colNames: ['Opciones', 'Código', 'Situación P.', 'Fecha Registro', 'Fecha Servicio', 'Hora Servicio', 'Proveedor', 'Beneficiado Principal', 'Usuario Registro', 'Origen', 'Destino', 'Calificación', 'Situación S.', 'Acciones'],
            colModel: [
                {
                    key: false,
                    name: 'ID',
                    index: 'ID',
                    align: 'center',
                    width: '150',
                    fixed: false,
                    formatter: formatoOpciones,
                    formatoptions: { keys: true, }
                },
                {
                    key: false,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    fixed: false
                },
                { key: false, name: 'NombreEstadoServicioProveedor', index: 'NombreSituacionServicio', align: 'center' },
                { key: false, name: 'FechaRegistroStr', index: 'FechaRegistroStr', width: "115", align: 'center' },
                { key: false, name: 'FechaServicioStr', index: 'FechaServicioStr', width: "115", align: 'center' },
                { key: false, name: 'HoraServicio', index: 'HoraServicio', width: "115", align: 'center' },
                { key: false, name: 'RazonSocial', index: 'RazonSocial', width: "94", align: 'center' },
                { key: false, name: 'BeneficiadoNombreCompleto', index: 'BeneficiadoNombreCompleto' },
                { key: false, name: 'UserNameRegistro', index: 'UserNameRegistro', align: 'center' },
                { key: false, name: 'DireccionOrigen', index: 'DireccionOrigen', width: "304" },
                { key: false, name: 'DireccionDestino', index: 'DireccionDestino', width: "304" },
                {
                    key: false,
                    name: 'Calificacion',
                    index: 'Calificacion',
                    align: 'center',
                    fixed: false,
                    formatter: formatoCalificacion
                },
                { key: false, name: 'NombreSituacionServicio', index: 'NombreSituacionServicio', width: "109", align: 'center' },
                {
                    key: false,
                    name: 'ID',
                    index: 'ID',
                    align: 'center',
                    fixed: true,
                    formatter: formatoAcciones,
                    formatoptions: { keys: true, },
                    width: '50'
                }
            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            height: '100%',
            autowidth: true,
            //postData: getParameterFilter(),

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
                        var id = $(this).attr('data-id');
                        Anular(pid, id)
                    },
                });
            },
            loadComplete: function (data) {
                fn_DesbloquearPantalla();
            },
        });
    }

    var formatoOpciones = function (cellvalue, options, rowObject) {
        var result = '<a href="javascript(0);" id="btn-sol-det" data-id ="' + cellvalue + '"  class="fa fa-search btn-sol-det" title = "Detalle"></a>' + '&nbsp;&nbsp;&nbsp;';
        if (rowObject.IdSituacionServicio > 1) {
            result += '<a href="javascript(0);" id="btn-estado" data-id="' + cellvalue + '"  class="fa fa-map-marker" title = "Tracking"></a>' + '&nbsp;&nbsp;&nbsp;';
        }
        result += '<a href="javascript(0);" id="btn-bitacora" data-id="' + cellvalue + '"  class="fa fa-bars" title = "Historial"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="javascript(0);" id="btn-gastoadicional" data-id="' + cellvalue + '"  class="fa fa-usd" title = "Gasto Adicional"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="javascript(0);" id="btn-puntoactual" data-id="' + cellvalue + '"  class="fa fa-car" title = "Punto Actual Automóvil"></a>';
        return result;
    }

    var formatoAcciones = function (cellvalue, options, rowObject) {
        var result = '';
        //console.log(rowObject);
        if (rowObject.IdSituacionServicio == 1) {
            result += '<a href="javascript(0);" id="btn-editar-servicio" data-id ="' + cellvalue + '" class="fa fa-edit" title = "Editar"></a>' + '&nbsp;&nbsp;&nbsp;';
        } else if (rowObject.IdSituacionServicio == 2) {
            result += '<a style="cursor: pointer;" id="btn-editar-solicitud" data-id="' + cellvalue + '" data-toggle="confirmation" data-container="body" data-popout="true" data-title="Editar Servicio" data-singleton="true" data-placement="left" data-btnOkLabel="Sí" data-btnCancelLabel="Cancelar" data-pid ="' + rowObject.IdServicioProveedor + '" class="fa fa-edit" data-content="Para editar este servicio, primero se anulará y es posible que se le cobre un gasto adicional. ¿Desea continuar?"></a>' + '&nbsp;&nbsp;&nbsp;';
        }
        if (rowObject.IdSituacionServicio == 1 || rowObject.IdSituacionServicio == 2) {
            result += '<a href="javascript(0);" id="btn-cancelar-servicio" data-id ="' + cellvalue + '" class="fa fa-times-circle" title = "Cancelar"></a>';
        }
        //console.log(result);
        return result;
    }

    var formatoCalificacion = function (cellvalue, options, rowObject) {
        var calificacion;
        if (cellvalue > 0) {
            calificacion = '<div style="color:#fff205; font-size: 14px;">';
            for (var i = 0; i < cellvalue; i++) {
                calificacion += '<span class="glyphicon glyphicon-star"></span>';
            }
            for (var j = 0; j < 5 - cellvalue; j++) {
                calificacion += '<span class="glyphicon glyphicon-star-empty"></span>';
            }
            calificacion += '</div>';
            return calificacion;
        } else {
            calificacion = 'Sin Calificación';
        }
        return calificacion;
    }

    function Anular(pid, id) {
        fn_BloquearPantalla();
        $.ajax({
            url: url.AnularPost,
            data: { idServicio: pid, id: id },
            type: 'POST',
            dataType:'JSON',
            beforeSend: function () { fn_BloquearPantalla(); },
            async: true,
            cache: true,
            success: function (data) {
                if (data.Success) {
                    window.location = "/Solicitud/Editar?id=" + id;
                } else {
                    toastr["error"](JSON.parse(data.ObjectResult).Message);
                    fn_DesbloquearPantalla();
                }
            },
            complete: function () {
                fn_DesbloquearPantalla();
            }
        });
    }
})();
