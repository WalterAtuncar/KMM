(function () {


    var validaciones = true;

    $(document).ready(function () {
        $('.filtro').attr('disabled', true);
        $('select#txtDni').prop('disabled', true);
        $('#dllEstados').prop('disabled', true);
        $('#dllSociedad').prop('disabled', true);
        $('#dllSucursal').prop('disabled', true);
        $('#txtAnioEjercicio').prop('disabled', true);
        $('#txtTipo').prop('disabled', true);

        $('#chkDni').change(function () { checkDni(this) });
        $('#chkFechaDesde').change(function () { checkFechaDesde(this) });
        $('#chkFechaHasta').change(function () { checkFechaHasta(this) });
        $('#chkTipo').change(function () { checkTipo(this) });

        $('#chkEstado').change(function () { checkEstado(this) });
        $('#chkCentroCosto').change(function () { checkCentroCosto(this) });

        $('#chkSociedad').change(function () { checkSociedad(this) });
        $('#chkSucursal').change(function () { checkSucursal(this) });
        $('#chkAhioEjercicio').change(function () { checkAnioEjercicio(this) });

        $('#dllListaCentroCosto').prop('disabled', true);

        $('#chkEstadoPorTipo').change(function () { checkEstadosPorTipo(this) });

        paginado();

        $('#btnBuscarSolicitudAprobacion').click(function () {
            $('#jqGrid').jqGrid('setGridParam', {
                page: 1,
                url: getURLGet(),
                ajaxGridOptions: {
                    beforeSend: function (xhr) {
                        fn_BloquearPantalla();
                    },
                    complete: function (xhr) {
                        fn_DesbloquearPantalla();
                        $('#hdfIdDestinoAprobar').val(xhr.responseJSON.iddestino);
                    },
                }
            }).trigger('reloadGrid');
        });

        var anchoventana = parseInt($(window).width());
        var anchomodal = (anchoventana - 50);
        $("#jqGrid").setGridWidth(anchomodal);

    });



    $('#btnAprobarSolicitud').click(function (e) {
        e.preventDefault();
        abrirModal();
    });

    $('#btnDelegar').click(function (e) {

        e.preventDefault();
        abrirModalDelegar();
    });

    $('body').on('click', "#btn-detalle-solicitud", function (e) {
        e.preventDefault();
        var IdSolicitud = $(this).attr('data-id');
        abrirModalDetalle(IdSolicitud);
    });


    $('body').on('click', '#btn-cancelar-solicitud', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        var cod = $(this).attr('data-cod');
        abrirConfirmaRechazo(pid, cod);
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

    $('body').on('click', "#btn-solicitud-cambioestadoAP", function (e) {
        debugger;
        e.preventDefault();
        var id = $(this).attr('data-id');
        var tipo = $(this).attr('data-type');
        abrirModalCambioEstadoAP(id, tipo);

    });

    var checkSociedad = function (element) {
        if (element.checked) {
            $('#dllSociedad').prop('disabled', false);
        } else {
            $('#dllSociedad').prop('disabled', true);
            $('#dllSociedad').val('');
            $('#dllSociedad').trigger('change')
        }
    }
    var checkSucursal = function (element) {
        if (element.checked) {
            $('#dllSucursal').prop('disabled', false);
        } else {
            $('#dllSucursal').prop('disabled', true);
            $('#dllSucursal').val('0');
            $('#dllSucursal').trigger('change')
        }
    }

    var checkAnioEjercicio = function (element) {
        if (element.checked) {
            $('#txtAnioEjercicio').prop('disabled', false);
        } else {
            $('#txtAnioEjercicio').prop('disabled', true);
            $('#txtAnioEjercicio').val('');

        }
    }



    var checkEstado = function (element) {
        if (element.checked) {
            $('#dllEstados').prop('disabled', false);
        } else {
            $('#dllEstados').prop('disabled', true);
            $('#dllEstados').val('0');
            $('#dllEstados').trigger('change')
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

    var checkFechaHasta = function (element) {
        if (element.checked) {
            $('#fechaHasta').prop('disabled', false);
        } else {
            $('#fechaHasta').prop('disabled', true);
            $('#fechaHasta').val('');
        }
    }

    var checkEstadosPorTipo = function (element) {
        if (element.checked) {
            if ($("#chkTipo").is(':checked')) {
                var idtipo = parseInt($("#txtTipo").val());
                if (idtipo > 0) {
                    $('select#dllEstados').prop('disabled', false);
                    $('select#dllEstados').focus();
                } else {
                    $("#chkEstadoPorTipo").prop('checked', false);
                    alert('Debe selecciona primero un tipo');

                }
            } else {
                $("#chkEstadoPorTipo").prop('checked', false);
                alert('Debe selecciona primero un tipo');
            }
        } else {
            $('select#dllEstados').prop('disabled', true);
            $('select#dllEstados').val(0);
            $('select#dllEstados').trigger('change')
        }
    }

    var checkTipo = function (element) {
        if (element.checked) {
            $('#txtTipo').prop('disabled', false);

        } else {
            $("#chkEstadoPorTipo").prop('checked', false);
            $('#txtTipo').prop('disabled', true);
            $('#txtTipo').val('0');
            $('#txtTipo').trigger('change')
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
            $('#dllListaCentroCosto').prop('disabled', false);
            $('#dllListaCentroCosto').focus();
        } else {
            $('#dllListaCentroCosto').prop('disabled', true);
            $('#dllListaCentroCosto').val('');
            $('#dllListaCentroCosto').trigger('change')
        }
    }



    function getURLGet() {

        var idusuario = $('#txtDni').is(':disabled') ? '' : $('#txtDni').val();
        var fechaDesde = $('#fechaDesde').is(':disabled') ? '' : $('#fechaDesde').val();
        var idcentrocosto = $('#dllListaCentroCosto').is(':disabled') ? '' : $('#dllListaCentroCosto').val();
        var fechaHasta = $('#fechaHasta').is(':disabled') ? '' : $('#fechaHasta').val();
        var idestado = $('#dllEstados').is(':disabled') ? '' : $('#dllEstados').val();
        var idsociedad = $('#dllSociedad').is(':disabled') ? '' : $('#dllSociedad').val();
        var idsucursal = $('#dllSucursal').is(':disabled') ? '' : $('#dllSucursal').val();
        var txtanioejercicio = '';
        var idtipo = $('#txtTipo').is(':disabled') ? '' : $('#txtTipo').val();
        return url.GetListSolicitudAprobacion + '?idusuario=' + idusuario + '&idsituacionservicio=' + idestado + '&fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&idcentrocosto=' + idcentrocosto + '&idsociedad=' + idsociedad + '&idsucursal=' + idsucursal + '&anioejercicio=' + txtanioejercicio + '&idtipo=' + idtipo;
    }

    //EDIAZ 17.02.2020, se puso la opcion 'Rendir' seguido de 'Opciones'
    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListSolicitudAprobacion,
            datatype: 'JSON',
            mtype: 'GET',
            //colNames: ['Opciones', 'Sociedad', 'Tipo', 'Codigo', 'DNI', 'Nombre', 'CECO', 'Fec.Des', 'Fec.Hasta', 'Mon', 'Importe', 'Gasto.T', 'Fec.Registro', 'Motivo', 'Estado', 'Rendir'],
            colNames: ['Opciones', 'Rendir', 'Sociedad', 'Tipo', 'Codigo', 'DNI', 'Nombre', 'CECO', 'Fec.Des', 'Fec.Hasta', 'Mon', 'Importe', 'Gasto.T', 'Fec.Registro', 'Motivo', 'Estado'],
            colModel: [
                {
                    key: true,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '60px',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { Keys: false }
                },
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
                { key: false, name: 'NombreCO', index: 'NombreCO', width: "70", align: 'center' },
                { key: false, name: 'NombreTipoServicio', index: 'NombreTipoServicio', width: "20", align: 'center' },
                { key: false, name: 'Codigo2', index: 'Codigo2', width: "42", align: 'center' },
                { key: false, name: 'DNI', index: 'DNI', width: "54", align: 'center' },
                { key: false, name: 'Beneficiario', index: 'Beneficiario', width: "100" },
                //{ key: false, name: 'Aprobador', index: 'Aprobador', width: "130" },
                { key: false, name: 'CECO', index: 'CECO', width: "70" },


                //{ key: true, name: 'Codigo', index: 'Codigo', width: "30", align: 'center', Key: true },


                { key: false, name: 'FechaDesde', index: 'FechaDesde', width: "45", align: 'center' },
                { key: false, name: 'FechaHasta', index: 'FechaHasta', width: "45", align: 'center' },
                { key: false, name: 'Moneda', index: 'Moneda', width: "25", align: 'center' },
                { key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "60", align: 'right' },
                { key: false, name: 'GastoRendido', index: 'GastoRendido', width: "40", align: 'right' },
                {
                    key: false, name: 'FechaRegistroStr', index: 'FechaRegistroStr', width: "45", align: 'center'
                    //,
                    //formatter: function (cellvalue, options, rowObject) {
                    //    return '<span style="background-color: #FFFF00; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                    //}
                },
                { key: false, name: 'Motivo', index: 'Motivo', width: "130" },
                {
                    key: false, name: 'Estado', index: 'Estado', width: "85", align: 'center',
                    formatter: function (cellvalue, options, rowObject) {
                        if (rowObject.IdSituacionServicio == 1) {
                            return '<span style="background-color: #87CEEB; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 2) {
                            return '<span style="background-color: #87CEEB; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 3) {
                            return '<span style="background-color: #FFA500; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 4) {
                            return '<span style="background-color: #98FB98; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }

                        if (rowObject.IdSituacionServicio == 5) {
                            return '<span style="background-color: #32CD32; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 6) {
                            return '<span style="background-color: #FF0000; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }

                        if (rowObject.IdSituacionServicio == 7) {
                            return '<span style="background-color: #FF0000; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }

                        if (rowObject.IdSituacionServicio == 8) {
                            return '<span style="background-color: #87CEEB; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 9) {
                            return '<span style="background-color: #FF7F50; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 10) {
                            return '<span style="background-color: #FFA500; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 11) {
                            return '<span style="background-color: #98FB98; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span>';
                        }
                        if (rowObject.IdSituacionServicio == 13) {
                            return '<span style="background-color: #FF7F50; display: block; width: 100%; height: 100%; ">' + cellvalue + '</span >';
                        }

                    }
                },


            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 30, 50, 100],
            height: '100%',
            //postData: JSON.stringify(getParameterFilter()),
            //loadonce: true,
            //multiSort: true,

            viewrecords: true,
            //caption: 'Students Records',
            //emptyrecords: 'No Students Records are Available to Display',
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
            },
            autowidth: true,
            multiselect: true,
            rownumbers: false,

            //cmTemplate: { sortable: true },
            loadBeforeSend: function () {
                fn_BloquearPantalla();
            },
            gridComplete: function () {
                //fn_DesbloquearPantalla();
                //$('[data-toggle="tooltip"]').tooltip({ placement: 'right' });
            },
            loadComplete: function (data) {
                document.getElementById("hdfIdDestinoAprobar").value = data.iddestino;

                if (data.rows.length > 0) {
                    for (var i = 0; i < data.rows.length; i++) {
                        if (data.rows[i].IdTipo == 1) {
                            if (data.rows[i].IdSituacionServicio == 2 || data.rows[i].IdSituacionServicio == 8 || data.rows[i].IdSituacionServicio == 13) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                        else if (data.rows[i].IdTipo == 2) {
                            if (data.rows[i].IdSituacionServicio == 2 || data.rows[i].IdSituacionServicio == 9 ) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                        else {

                            if (data.rows[i].IdSituacionServicio == 2 || data.rows[i].IdSituacionServicio == 8) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                    }
                };
                fn_DesbloquearPantalla();
            },
        });

    }



    var formatoOpciones = function (cellvalue, options, rowObject) {
        var result = '';
        //if (rowObject.IdSituacionServicio2 = 1) {
        //    result += '<a href="javascript(0);" id="btn-edit-solicitud_aprobacion_exterior" data-id ="' + cellvalue + '" class="glyphicon glyphicon-pencil" title = "Aprobar Cotizacion"></a>' + '&nbsp;&nbsp;';
        //}
        result += '<a href="javascript(0);" id="btn-solicitud-cambioestadoAP" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"data-type ="' + rowObject.IdTipo + '"  class="fa fa-arrow-left" title = "Cambio Estado"></a>' + '&nbsp;';
        debugger;
        if (rowObject.IdTipo == 1) {

            if (rowObject.IdSituacionServicio < 3) {
                result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-times-circle" title = "Rechazar"></a>' + '&nbsp;&nbsp;';
            }
        } else {
            if (rowObject.IdSituacionServicio <= 2) {
                /*result += '<a href="javascript(0);" id="btn-solicitud-cambioestadoAP" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"data-type ="' + rowObject.IdTipo + '"  class="fa fa-arrow-left" title = "Cambio Estado"></a>' + '&nbsp;';*/
                result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-times-circle" title = "Rechazar"></a>' + '&nbsp;&nbsp;';
            }
        }
        
        result += '<a href="javascript(0);" id="btn-detalle-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-search btn-sol-det" title = "Detalle"></a>' + '&nbsp;';
        result += '<a href="javascript(0);" id="btn-bitacora-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-bars" title = "Historial"></a>' + '&nbsp;';
        return result;
    }

    var formatoOpciones2 = function (cellvalue, options, rowObject) {
        var result = '';
        if (rowObject.IdTipo == 2 || rowObject.IdTipo == 3 || rowObject.IdTipo == 4) {
            result += '<a href="javascript(0);" id="btn-detalle-rendir-gasto" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-check-square-o fa-lg text-danger btn-sol-evaluar" title = "Rendir"></a>' + '&nbsp;';

        } else {
            if (rowObject.IdTipo == 1) {
                //if (rowObject.IdSituacionServicio == 4 || rowObject.IdSituacionServicio == 5) {
                //if (rowObject.IdSituacionServicio == 5) {
                result += '<a href="javascript(0);" id="btn-detalle-rendir-gasto" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-check-square-o fa-lg text-danger btn-sol-evaluar" title = "Rendir"></a>' + '&nbsp;';
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



    var abrirModal = function () {
        fn_BloquearPantalla();
        $('#modal-solicitud-aprobacion .modal-content').empty();
        IdDestinoAprobador = $('#hdfIdDestinoAprobar').val();
        $('#modal-solicitud-aprobacion .modal-content').load(url.ConfirmarAprobar, { IdDestinoAprobador: IdDestinoAprobador }, function () {
            $("#modal-solicitud-aprobacion").modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });
    }

    var abrirModalDelegar = function () {
        fn_BloquearPantalla();
        $('#modal-delegar .modal-content').empty();
        $('#modal-delegar .modal-content').load(url.Delegar, { IdUsuario: 0 }, function () {
            $("#modal-delegar").modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });
    }


    var abrirSolicitudHistorial = function (pid) {
        fn_BloquearPantalla();
        $('#modal-sol-detalle .modal-content').empty();
        $('#modal-sol-detalle .modal-content').load(url.GetHistorialSolicitud, { IdSolicitud: pid }, function () {
            $("#modal-sol-detalle").modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });

    }

    var abrirModalDetalle = function (IdSolicitud) {
        fn_BloquearPantalla();
        $('#modal-solicitud-detalle .modal-content').empty();
        $('#modal-solicitud-detalle .modal-content').load(url.DetalleSolicitud, { IdSolicitud: IdSolicitud }, function () {
            $("#modal-solicitud-detalle").modal({ show: true, backdrop: 'static', keyboard: true });
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

    var abrirConfirmaRechazo = function (IdSolicitud, cod) {
        fn_BloquearPantalla();
        $('#modal-solicitud-rechazo .modal-content').empty();
        $('#modal-solicitud-rechazo .modal-content').load(url.ConfirmaRechazo, { IdSolicitud: IdSolicitud, cod: cod }, function () {
            $('#modal-solicitud-rechazo').modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });

    }

    var abrirModalCambioEstadoAP = function (id, tipo) {
        debugger;
        fn_BloquearPantalla();
        $('#modal-solicitud-cambio-estado .modal-content').empty();
        $('#modal-solicitud-cambio-estado .modal-content').load(url.ConfirmacionCambioEstado, { id: id }, function () {
            $('#modal-solicitud-cambio-estado').modal({ show: true, backdrop: 'static', keyboard: true });
            if (tipo == 1) {

                for (var i = 1; i <= 12; i++) {

                    if (i != 5) {
                        var n = "#dllEstados_CambioEstado option[value =" + i + "]";
                        $(n).remove();
                    } else {
                        $('select#dllEstados_CambioEstado option:selected').val(5);
                    }

                }
            } else {
                var cntoption = $("#dllEstados_CambioEstado option").length;
                for (var i = 1; i <= 12; i++) {
                    if (i != 1) {
                        var n = "#dllEstados_CambioEstado option[value =" + i + "]";
                        $(n).remove();

                    } else {
                        $('select#dllEstados_CambioEstado option:selected').val(1);
                    }
                }

                if (cntoption > 1) {
                    $("#dllEstados_CambioEstado option:nth-child(2)").remove();
                }
            }

                fn_DesbloquearPantalla();
            });

    }

    cargadogrilla = 1;

})();