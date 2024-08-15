(function () {
    var validaciones = true;
    var Id_Solicitud = 0;
    $(document).ready(function () {

        //NEY TANGOA 14/01/21

        //if (window.localStorage) {
        //    if (!localStorage.getItem('firstLoad')) {
        //        localStorage['firstLoad'] = true;
        //        window.location.reload();
        //    } else
        //        localStorage.removeItem('firstLoad');
        //}

        //FIN CAMBIOS 


        $('.filtro').attr('disabled', true);
        $('select#txtDni').prop('disabled', true);
        $('#txtTipo').prop('disabled', true);
        $('select#dllCentroCosto').prop('disabled', true);
        $('select#dll_EstadosPorTipo').prop('disabled', true);
        $('#chkDni').change(function () {checkDni(this)});
        $('#chkFechaDesde').change(function () { checkFechaDesde(this) });
        $('#chkTipo').change(function () { checkTipo(this) });
        $('#chkCentroCosto').change(function () { checkCentroCosto(this) });
        $('#chkFechaHasta').change(function () { checkFechaHasta(this) });
        $('#chkEstadoPorTipo').change(function () { checkEstadosPorTipo(this) });
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
        var anchoventana = parseInt($(window).width());
        var anchomodal = (anchoventana - 50);
        $("#jqGrid").setGridWidth(anchomodal);
    });

    $('#btnSolicitudesDelegadas').click(function (e) {
        e.preventDefault();
        abrirModalSolicitudesDelegadas();
    });

    $('#btnRegistrarSolicitud').click(function (e) {
        debugger;
        $('#hdfIdSolicitud_Formulario').val('');
        $('#hdfCtaBancaria_Formulario').val('0');
        $('#hdfIdUsuario_Formulario').val('0')
       
        cargado = 0;
        idUserCuentaBancaria = 0;
        e.preventDefault();
        abrirModal(0);
    });
    $('body').on('click', "#btn-edit-solicitud", function (e) {
      
        $('#hdfIdSolicitud_Formulario').val('');
        $('#hdfCtaBancaria_Formulario').val('0');
        $('#hdfIdUsuario_Formulario').val('0');
        cargado = 0;
        idUserCuentaBancaria = 0;
        e.preventDefault();
        var IdSolicitud = $(this).attr('data-id');
        abrirModal(IdSolicitud);
    });

    $('body').on('click', "#btn-detalle-solicitud", function (e) {
        e.preventDefault();
        var IdSolicitud = $(this).attr('data-id');
        abrirModalDetalle(IdSolicitud);
    });

    $('body').on('click', '#btn-bitacora-solicitud', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirSolicitudHistorial(pid);
    });
    

    $('body').on('click', '#btn-detalle-rendir-gasto', function (e) {
       
        e.preventDefault();
        var pid = $(this).attr('data-id');
        Id_Solicitud = pid;
        abrirRendirGastos(pid);
    });

    $('body').on('click', "#btn-cancelar-solicitud", function (e) {
        
        e.preventDefault();
        var id = $(this).attr('data-id');
        var cod = $(this).attr('data-cod');
        abrirModalAnularSolicitud(id,cod);
    });
    

    var checkTipo = function(element){
        if (element.checked) {
            $('#txtTipo').prop('disabled', false);

        } else {
            $("#chkEstadoPorTipo").prop('checked', false); 
            $('#txtTipo').prop('disabled', true); 
            $('#txtTipo').val('0');
            $('#txtTipo').trigger('change')
        }
    }
    
    var checkFechaDesde = function (element) {
        if (element.checked) {
            $('#fechaDesde').prop('disabled', false);
        } else {
            $('#fechaDesde').prop('disabled', true);  
            $('#fechaDesde').val('');
        }
    }

    var checkDni = function (element) {
        if (element.checked) {
            $('select#txtDni').prop('disabled', false);
            $('select#txtDni').focus();
        } else {
            $('select#txtDni').prop('disabled', true);
            $('select#txtDni').val('');
            $('select#txtDni').trigger('change')
        }
    }

    var checkCentroCosto = function (element) {
        if (element.checked) {
            $('#dllCentroCosto').prop('disabled', false);
            $('#dllCentroCosto').focus();
        } else {
            $('#dllCentroCosto').prop('disabled', true);
            $('#dllCentroCosto').val('');
            $('#dllCentroCosto').trigger('change')
        }
    }

    var checkEstadosPorTipo = function (element) {
        if (element.checked) {
            if ($("#chkTipo").is(':checked')) {
                var idtipo = parseInt($("#txtTipo").val());
                if (idtipo > 0) {
                    $('select#dll_EstadosPorTipo').prop('disabled', false);
                    $('select#dll_EstadosPorTipo').focus();
                } else {
                    $("#chkEstadoPorTipo").prop('checked', false); 
                    alert('Debe selecciona primero un tipo');
                   
                }
            } else {
                $("#chkEstadoPorTipo").prop('checked', false); 
                alert('Debe selecciona primero un tipo');
            }
        } else {
            $('select#dll_EstadosPorTipo').prop('disabled', true);
            $('select#dll_EstadosPorTipo').val(0);
            $('select#dll_EstadosPorTipo').trigger('change')
        }
    }

    

    var checkFechaHasta = function (element) {
        if (element.checked) {
            $('#fechaHasta').prop('disabled', false);
        } else {
            $('#fechaHasta').prop('disabled', true);
            $('#fechaHasta').val('');
        }
    }

    function getURLGet() {
       
        var idusuario = $('#txtDni').is(':disabled') ? '' : $('#txtDni').val();
        var fechaDesde = $('#fechaDesde').is(':disabled') ? '' : $('#fechaDesde').val();
        var idcentrocosto = $('#dllCentroCosto').is(':disabled') ? '' : $('#dllCentroCosto').val();
        var fechaHasta = $('#fechaHasta').is(':disabled') ? '' : $('#fechaHasta').val();
        var idtipo = $('#txtTipo').is(':disabled') ? '' : $('#txtTipo').val();
        var idestado = $('#dll_EstadosPorTipo').is(':disabled') ? 0 : $('#dll_EstadosPorTipo').val();

        return url.GetListSolicitud + '?idusuario=' + idusuario + '&idtipo=' + idtipo + '&fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&idcentrocosto=' + idcentrocosto + '&idestado=' + idestado;
    }

    //EDIAZ 17.02.2020, se puso la opcion 'Rendir' seguido de 'Opciones'
    var paginado = function () {
        $("#jqGrid").jqGrid({
            //url: getURLGet(),
            url: url.GetListSolicitud,
            datatype: 'JSON',
            //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
            mtype: 'GET',
            //colNames: ['Opciones', 'DNI', 'ID', 'Codigo', 'Fec.Des', 'Fec.Has', 'CECO', 'Aprobador', 'Tipo', 'Mon', 'Importe', 'Fec.Sol', 'Motivo','Fec.Camb.Estado', 'Estado', 'Rendir'],
            colNames: ['Opciones', 'Rendir', 'Sociedad', 'Tipo', 'Codigo', 'DNI', 'Empleado', 'Aprobador', 'CECO', 'Mon', 'Importe', 'Gasto.T','Fec.Sol', 'Motivo', 'Estado'],
            colModel: [
                {
                    key: false,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '64px',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { keys: true, }
                },
                //{ key: false, name: 'NombreCO', index: 'NombreCO', width: "150", align: 'center' },
                //{ key: false, name: 'NombreTipoServicio', index: 'NombreTipoServicio', width: "40", align: 'center' },
                ////{ key: false, name: 'Codigo', index: 'Codigo', width: "30", align: 'center' },
                //{ key: false, name: 'Codigo2', index: 'Codigo2', width: "62", align: 'center' },
                ////{ key: false, name: 'FechaDesde', index: 'FechaDesde', width: "60" },
                ////{ key: false, name: 'FechaHasta', index: 'FechaHasta', width: "60" },
                //{ key: false, name: 'DNI', index: 'DNI', width: "64", align: 'center' },
                //{ key: false, name: 'Beneficiario', index: 'Beneficiario', align: 'center' },
                //{ key: false, name: 'Aprobador', index: 'Aprobador', width: "130"  },
                //{ key: false, name: 'CECO', index: 'CECO' },
                //{ key: false, name: 'Moneda', index: 'Moneda', width: "25", align: 'center' },
                //{ key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "60", align: 'right' },
                //{ key: false, name: 'GastoRendido', index: 'GastoRendido', width: "60", align: 'right' },
                //{ key: false, name: 'FechaRegistroStr', index: 'FechaRegistroStr', width: "65" },
                //{ key: false, name: 'Motivo', index: 'Motivo', width: "150" },
                ////{ key: false, name: 'UsuarioActualizo', index: 'UsuarioActualizo', width: "80" },
                ////{ key: false, name: 'FechaActualizacion', index: 'FechaActualizacion', width: "61" },
                //{ key: false, name: 'Estado', index: 'Estado', width: "61"},
                ////{ key: false, name: 'AsientoContable', index: 'AsientoContable', width: "61"},
                ////{ key: false, name: 'Motivo', index: 'Motivo', width: "100" },
                {
                    key: false,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '32px',
                    fixed: true,
                    formatter: formatoOpciones2,
                    formatoptions: { keys: true, }
                },

                { key: false, name: 'NombreCO', index: 'NombreCO', width: "68", align: 'center' },
                { key: false, name: 'NombreTipoServicio', index: 'NombreTipoServicio', width: "20", align: 'center' },
                { key: false, name: 'Codigo2', index: 'Codigo2', width: "40", align: 'center' },
                { key: false, name: 'DNI', index: 'DNI', width: "50", align: 'center' },
                { key: false, name: 'Beneficiario', index: 'Beneficiario', width: "80",align: 'center' },
                { key: false, name: 'Aprobador', index: 'Aprobador', width: "80", align: 'center' },
                { key: false, name: 'CECO', index: 'CECO', width: "70", align: 'center' },
                { key: false, name: 'Moneda', index: 'Moneda', width: "25", align: 'center' },
                { key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "40", align: 'right' },
                { key: false, name: 'GastoRendido', index: 'GastoRendido', width: "40", align: 'right' },
                { key: false, name: 'FechaRegistroStr', index: 'FechaRegistroStr', width: "45", align: 'center' },
                { key: false, name: 'Motivo', index: 'Motivo', width: "75", align: 'center' },
                { key: false, name: 'Estado', index: 'Estado', width: "85",align: 'center' },
         
                

            ],

            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10,30, 50, 100],
            height: '100%',
            //postData: JSON.stringify(getParameterFilter()),
          
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
                //fn_DesbloquearPantalla();
                //$('[data-toggle="tooltip"]').tooltip({ placement: 'right' });
            },
            loadComplete: function (data) {
                fn_DesbloquearPantalla();
                //$('[data-toggle="tooltip"]').tooltip();
            },
           
        });
    }

    var formatoOpciones = function (cellvalue, options, rowObject) {
        var result = ''
        if ((rowObject.IdSituacionServicio == 1 || rowObject.IdSituacionServicio == 2) && rowObject.IdTipo == 1 ) {
            result += '<a href="javascript(0);" id="btn-edit-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"  class="fa fa-pencil" title="Editar"></a>' + '&nbsp;&nbsp;';
            result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"   class="fa fa-times-circle" title = "Eliminar"></a>' + '&nbsp;&nbsp;';
        }
        if (rowObject.IdSituacionServicio == 1 && rowObject.IdTipo != 1) {
                result += '<a href="javascript(0);" id="btn-edit-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"  class="fa fa-pencil" title="Editar"></a>' + '&nbsp;&nbsp;';
                result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"   class="fa fa-times-circle" title = "Eliminar"></a>' + '&nbsp;&nbsp;';
        }
        result += '<a href="javascript(0);" id="btn-detalle-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-search btn-sol-det" title = "Detalle"></a>' + '&nbsp;';
        result += '<a href="javascript(0);" id="btn-bitacora-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-bars" title = "Historial"></a>' + '&nbsp;';
        return result;
    }

    var formatoOpciones2 = function (cellvalue, options, rowObject) {
        var result = '';
        if (rowObject.IdTipo == 2 || rowObject.IdTipo == 3 || rowObject.IdTipo == 4) {
            result += '<a href="javascript(0);" id="btn-detalle-rendir-gasto" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-check-square-o fa-lg text-danger btn-sol-evaluar" title = "Rendir"></a>' + '&nbsp;';
        } else {
            if (rowObject.IdTipo == 1) {
                //if (rowObject.IdSituacionServicio == 4 || rowObject.IdSituacionServicio == 5) {
                //if (rowObject.IdSituacionServicio == 5) {
                result += '<a href="javascript(0);" id="btn-detalle-rendir-gasto" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-check-square-o fa-lg text-danger btn-sol-evaluar" title = "Rendir"></a>' + '&nbsp;';
                //}
            }
        }
        return result;
    }


    

    //function convertDateFormat(string) {
    //    var info = string.split('-').reverse().join('/');
    //    return info;
    //}

    //var getParameterFilter = function () {
    //    var parametro = [
    //        {
    //            ParamName: "Prueba1",
    //            ParamValue: "3"
    //        },
    //        {
    //            ParamName: "Prueba2",
    //            ParamValue: "4"
    //        }
    //    ];

    //    var Pagination = new Object();
    //    Pagination.Page = $('#jqGrid').getGridParam("page") == undefined ? 1 : $('#jqGrid').getGridParam("page") ;
    //    Pagination.Rows = $('#jqGrid').getGridParam("rowNum") == undefined ? 10 : $('#jqGrid').getGridParam("rowNum");
    //    Pagination.ListaParameterFiler = parametro;
    //    return Pagination;
    //}

   

    function abrirModal(IdSolicitud) {
        debugger;
        fn_BloquearPantalla();
        $('#modal-solicitud .modal-content').empty();
        $('#modal-solicitud .modal-content').load(url.Registrar, { IdSolicitud: IdSolicitud }, function () {
            //setFlag();
            $("#modal-solicitud").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });

    }

    var abrirModalSolicitudesDelegadas = function () {
        fn_BloquearPantalla();
        $('#modal-solicitudes-delegadas .modal-content').empty();
        $('#modal-solicitudes-delegadas .modal-content').load(url.SolicitudesDelegadas, { IdUsuario: 0 }, function () {
            $("#modal-solicitudes-delegadas").modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });
    }

    var abrirModalDetalle = function (IdSolicitud) {
        fn_BloquearPantalla();
        $('#modal-solicitud-detalle .modal-content').empty();
        $('#modal-solicitud-detalle .modal-content').load(url.DetalleSolicitud, { IdSolicitud: IdSolicitud }, function () {
            $("#modal-solicitud-detalle").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });
      
    }

    var abrirSolicitudHistorial = function (pid) {
        fn_BloquearPantalla();
        $('#modal-sol-detalle .modal-content').empty();
        $('#modal-sol-detalle .modal-content').load(url.GetHistorialSolicitud, { IdSolicitud: pid }, function () {
            $("#modal-sol-detalle").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });
    }

    var abrirRendirGastos = function (IdSolicitud) {
        fn_BloquearPantalla();
      
        $("#modal-solicitud-detalle-gasto").modal({ show: true, backdrop: 'static', keyboard: true });
        $('#modal-solicitud-detalle-gasto .modal-content').empty();
        $('#modal-solicitud-detalle-gasto .modal-content').load(url.DetalleRendirGastos, { IdSolicitud: IdSolicitud }, function () {
            //if (window.jsCharged) {
            //    $('#tbl-solicitudes').jqGrid('clearGridData');
            //    $('#tbl-solicitudes').jqGrid('setGridParam', {
            //        url: getURLGet(),
            //    }).trigger('reloadGrid');
            //}
            fn_DesbloquearPantalla();
        });
    }
    function abrirModalAnularSolicitud(id,cod) {
        fn_BloquearPantalla();
        $('#modal-solicitud-eliminar-anular .modal-content').empty();
        $('#modal-solicitud-eliminar-anular .modal-content').load(url.ConfirmarAnularSolicitud, { id: id, cod: cod }, function () {
            $("#modal-solicitud-eliminar-anular").modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });
    }

    

})();