(function () {


    var validaciones = true;

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
        $('#dllEstados').prop('disabled', true);
        $('#dllSociedad').prop('disabled', true);
        $('#dllSucursal').prop('disabled', true);
        $('#txtAnioEjercicio').prop('disabled', true);
        $('#txtCodigoFiltro').prop('disabled', true);
        $('#txtTipo').prop('disabled', true);

        $('#chkDni').change(function () { checkDni(this) });
        $('#chkFechaDesde').change(function () { checkFechaDesde(this) });
        $('#chkFechaHasta').change(function () { checkFechaHasta(this) });
        $('#chkTipo').change(function () { checkTipo(this) });
        $('#chkCodigoFiltro').change(function () { checkCodigoFiltro(this) });

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

    $('body').on('click', "#btn-detalle-solicitud", function (e) {
        e.preventDefault();
        var IdSolicitud = $(this).attr('data-id');
        abrirModalDetalle(IdSolicitud);
    });
    $('body').on('click', "#btn-solicitud-cambioestado", function (e) {
        e.preventDefault();
        var id = $(this).attr('data-id');
        abrirModalCambioEstado(id);
    });

    //$('body').on('click', "#btn-solicitud-cambioestadoAP", function (e) {
    //    e.preventDefault();
    //    var id = $(this).attr('data-id');
    //    abrirModalCambioEstadoAP(id);

    //});



    $('body').on('click', '#btn-bitacora-solicitud', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        abrirSolicitudHistorial(pid);
    });

    $('body').on('click', '#btn-cancelar-solicitud', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-id');
        var cod = $(this).attr('data-cod');
        abrirConfirmaRechazo(pid, cod);
    });

    $('body').on('click', '#btn-detalle-rendir-gasto', function (e) {
        debugger;
        e.preventDefault();
        var pid = $(this).attr('data-id');
        Id_Solicitud = pid;
        abrirRendirGastos(pid);
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

    var checkCodigoFiltro = function (element) {
        if (element.checked) {
            $('#txtCodigoFiltro').prop('disabled', false);
        } else {
            $('#txtCodigoFiltro').prop('disabled', true);
            $('#txtCodigoFiltro').val('');
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
        var codigo = $('#txtCodigoFiltro').is(':disabled') ? '' : $('#txtCodigoFiltro').val();

        return url.GetListSolicitudAprobacion + '?idusuario=' + idusuario + '&idsituacionservicio=' + idestado + '&fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&idcentrocosto=' + idcentrocosto + '&idsociedad=' + idsociedad + '&idsucursal=' + idsucursal + '&anioejercicio=' + txtanioejercicio + '&idtipo=' + idtipo + '&codigo=' + codigo;
    }

    //EDIAZ 17.02.2020, se puso la opcion 'Rendir' seguido de 'Opciones'
    var paginado = function () {
        //debugger;

        var usuarioSociedad = "PE04";

        $("#jqGrid").jqGrid({
            url: url.GetListSolicitudAprobacion,
            datatype: 'JSON',
            mtype: 'GET',
            //colNames: ['Opciones', 'Soc.', 'Tipo', 'Codigo', 'DNI','Nombre', 'CECO', 'Fec.Des', 'Fec.Hasta', 'Aprobador', 'Mon', 'Importe', 'Gasto.T', 'Fec.Aprob', 'Fec.Pago', 'Motivo', 'Estado','Asiento.Cont','Cod.Liquidacion', 'Rendir'],
            colNames: ['Opciones', 'Rendir', 'Soc.', 'Tipo', 'Codigo', 'DNI', 'Nombre', 'CECO', 'Fec.Des', 'Fec.Hasta', 'Aprobador', 'Mon', 'Importe', 'Gasto.T', 'Fec.Aprob', 'Fec.Pago', 'Motivo', 'Estado', 'Asiento.Cont', 'Cod.Liquidacion'],
            colModel: [
                {
                    key: true,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '80px',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { Keys: false }
                },

                {
                    key: false,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '30px',
                    fixed: true,
                    formatter: formatoOpciones2,
                    formatoptions: { keys: true, }
                },
                //{ key: false, name: 'NombreCO', index: 'NombreCO', width: "68", align: 'center' },
                //{ key: false, name: 'NombreTipoServicio', index: 'NombreTipoServicio', width: "38", align: 'center' },
                //{ key: false, name: 'Codigo2', index: 'Codigo2', width: "68", align: 'center' },
                //{ key: false, name: 'DNI', index: 'DNI', width: "64", align: 'center' },
                //{ key: false, name: 'Beneficiario', index: 'Beneficiario', width: "100" },
                //{ key: false, name: 'CECO', index: 'CECO' },
                //{ key: false, name: 'FechaDesde', index: 'FechaDesde', width: "63" },
                //{ key: false, name: 'FechaHasta', index: 'FechaHasta', width: "63" },
                //{ key: false, name: 'Aprobador', index: 'Aprobador', width: "100" },
                //{ key: false, name: 'Moneda', index: 'Moneda', width: "30", align: 'center' },
                //{ key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "60", align: 'right' },
                //{ key: false, name: 'GastoRendido', index: 'GastoRendido', width: "60", align: 'right' },
                //{ key: false, name: 'FechaAprobacion', index: 'FechaAprobacion', width: "65" },
                //{ key: false, name: 'FechaCancelacion', index: 'FechaCancelacion', width: "65" },
                //{ key: false, name: 'Motivo', index: 'Motivo', width: "150" },


                { key: false, name: 'NombreCO', index: 'NombreCO', width: "40", align: 'center' },
                { key: false, name: 'NombreTipoServicio', index: 'NombreTipoServicio', width: "35", align: 'center' },
                { key: false, name: 'Codigo2', index: 'Codigo2', width: "68", align: 'center' },
                { key: false, name: 'DNI', index: 'DNI', width: "64", align: 'center' },
                { key: false, name: 'Beneficiario', index: 'Beneficiario', width: "160", align: 'center' },
                { key: false, name: 'CECO', index: 'CECO', width: "70", align: 'center' },
                { key: false, name: 'FechaDesde', index: 'FechaDesde', width: "70", align: 'center' },
                { key: false, name: 'FechaHasta', index: 'FechaHasta', width: "70", align: 'center' },
                { key: false, name: 'Aprobador', index: 'Aprobador', width: "150", align: 'center' },
                { key: false, name: 'Moneda', index: 'Moneda', width: "35", align: 'center' },
                { key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "60", align: 'right' },
                { key: false, name: 'GastoRendido', index: 'GastoRendido', width: "60", align: 'right' },
                { key: false, name: 'FechaAprobacion', index: 'FechaAprobacion', width: "70px", align: 'center' },
                { key: false, name: 'FechaCancelacion', index: 'FechaCancelacion', width: "70px", align: 'center' },
                { key: false, name: 'Motivo', index: 'Motivo', width: "75", align: 'center' },
                {
                    key: false, name: 'Estado', index: 'Estado', width: "85", align: 'center',
                    formatter: function (cellvalue, options, rowObject) {
                        if (rowObject.IdSituacionServicio == 1) {
                            return '<div style="background-color: #87CEEB; display: block; width: 100%; height: 100%;display: table; "><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 2) {
                            return '<div style="background-color: #87CEEB; display: block; width: 100%; height: 100%;display: table;><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 3) {
                            return '<div style="background-color: #FFA500; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 4) {
                            return '<div style="background-color: #98FB98; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 5) {
                            return '<div style="background-color: #32CD32; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 6) {
                            return '<div style="background-color: #FF0000; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 7) {
                            return '<div style="background-color: #FF0000; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 8) {
                            return '<div style="background-color: #87CEEB; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 9) {
                            return '<div style="background-color: #FF7F50; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 10) {
                            return '<div style="background-color: #FFA500; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        if (rowObject.IdSituacionServicio == 11) {
                            return '<div style="background-color: #98FB98; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        //NEY TANGOA 23/07/2020
                        if (rowObject.IdSituacionServicio == 12) {
                            return '<div style="background-color: #98FB98; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                        //FIN CAMBIOS
                        if (rowObject.IdSituacionServicio == 13) {
                            return '<div style="background-color: #FF7F50; display: block; width: 100%; height: 100%;display: table;"><div style="display: table-cell;vertical-align: middle;">' + cellvalue + '</div></div>';
                        }
                    }
                },
                { key: false, name: 'AsientoContable', index: 'AsientoContable', width: "79" },
                { key: false, name: 'CodigoLiquidacion', index: 'CodigoLiquidacion', width: "79" },


            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 30, 50, 100],
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
            },
            autowidth: true,
            multiselect: true,
            rownumbers: false,
            cmTemplate: { sortable: false },


            //height: 550,
            //sortable: true,
            //resizable: true,
            //pageable: true,


            loadBeforeSend: function () {
                fn_BloquearPantalla();
            },
            gridComplete: function () {
                //fn_DesbloquearPantalla();
                //$('[data-toggle="tooltip"]').tooltip({ placement: 'right' });
            },
            loadComplete: function (data) {
                $('#hdfIdDestinoAprobar').val(data.iddestino);

                if (data.rows.length > 0) {
                    for (var i = 0; i < data.rows.length; i++) {

                        //var rowData = data.rows[i];
                        //var idSociedad = rowData.NombreCO;

                        //var rowId = rowData.Codigo; // Utiliza el valor de Codigo2 como id de la fila

                        //// Verificar si UsuarioSociedad es igual a "PE04" y la fila tiene NombreCO diferente a "PE04"
                        //if (usuarioSociedad === "PE04" && idSociedad !== "PE04") {
                        //    // Si UsuarioSociedad es "PE04" y la fila no tiene NombreCO igual a "PE04", ocultar la fila
                        //    $('#' + rowId).hide(); // Oculta la fila por su id
                        //    $('#jqg_jqGrid_' + rowId).prop('checked', false); // Deselecciona el checkbox
                        //}

                        //var datosss=ExisteOrdenServicioInvalida(data.rows[i].IdSolicitud);
                        if (data.rows[i].IdTipo == 1) {
                            if (data.rows[i].IdSituacionServicio == 3 || data.rows[i].IdSituacionServicio == 9 || data.rows[i].IdSituacionServicio == 10) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                        else if (data.rows[i].IdTipo == 2) {
                            if (data.rows[i].IdSituacionServicio == 3 || data.rows[i].IdSituacionServicio == 4) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                        else {
                            if (data.rows[i].IdSituacionServicio == 3 || data.rows[i].IdSituacionServicio == 4) {
                            } else {
                                $('#jqg_jqGrid_' + data.rows[i].Codigo + '').css("display", "none");
                            }
                        }
                    }
                }
                //var grid = $("#jqGrid"),
                //ids = grid.getDataIDs();
                //for (var i = 0; i < ids.length; i++) {
                //    grid.setGridHeight('auto'); 
                //}
                fn_DesbloquearPantalla();
            },
        });
    }



    var formatoOpciones = function (cellvalue, options, rowObject) {
        var result = '';
        var IdPerfil = $("#IdPerfil").val();

        if (rowObject.IdTipo == 1) {
            if (IdPerfil != 24) {
                //if (rowObject.IdSituacionServicio < 4) {
                //result += '<a href="javascript(0);" id="btn-solicitud-cambioestadoAP" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-arrow-left" title = "Cambio Estado"></a>' + '&nbsp;';
                result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '" class="fa fa-times-circle" title = "Rechazar"></a>' + '&nbsp;&nbsp;';
                result += '<a href="javascript(0);" id="btn-solicitud-cambioestado" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-key" title = "Cambio Estado"></a>' + '&nbsp;';
                //}
            }
        }
        else {
            if (IdPerfil != 24) {
                //if (rowObject.IdSituacionServicio < 4) {
                //result += '<a href="javascript(0);" id="btn-solicitud-cambioestado" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-key" title = "Cambio Estado"></a>' + '&nbsp;';
                result += '<a href="javascript(0);" id="btn-cancelar-solicitud" data-id ="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"   class="fa fa-times-circle" title = "Rechazar"></a>' + '&nbsp;&nbsp;';
                result += '<a href="javascript(0);" id="btn-solicitud-cambioestado" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-key" title = "Cambio Estado"></a>' + '&nbsp;';
                //}
            }
        }

        result += '<a href="javascript(0);" id="btn-detalle-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-search btn-sol-det" title = "Detalle"></a>' + '&nbsp;';
        result += '<a href="javascript(0);" id="btn-bitacora-solicitud" data-id="' + cellvalue + '" data-cod ="' + rowObject.Codigo2 + '"    class="fa fa-bars" title = "Historial"></a>' + '&nbsp;';
        return '<div style="width:100%;display:table;"><div style="display:table-cell;vertical-align:middle;">' + result + '</div></div>';
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
    //function ExisteOrdenServicioInvalida(idsolicitud) {
    //    debugger;
    //    var retorno = false;
    //    if (ordenServicio != "-------") {
    //        $.ajax({
    //            url: url.SolicitudOrdenServicioInvalida,
    //            type: 'POST',
    //            data: { idsolicitud: idsolicitud },
    //            cache: false,
    //            async: false,
    //            beforeSend: function () {
    //                fn_BloquearPantalla();
    //            },
    //            success: function (data) {
    //                debugger;
    //                if (data.Success) {
    //                    fn_DesbloquearPantalla();
    //                    retorno = true;
    //                } else {

    //                    fn_DesbloquearPantalla();
    //                }
    //            },
    //            complete: function () {
    //                fn_DesbloquearPantalla();
    //            }
    //        });
    //    }
    //    return retorno;
    //}



    var abrirModal = function () {
        fn_BloquearPantalla();
        $('#modal-solicitud-aprobacion .modal-content').empty();
        $('#modal-solicitud-aprobacion .modal-content').load(url.ConfirmarAprobar, {}, function () {
            $("#modal-solicitud-aprobacion").modal({ show: true, backdrop: 'static', keyboard: true });
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
            $('#modal-solicitud-detalle').modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });

    }
    var abrirModalCambioEstado = function (id) {
        fn_BloquearPantalla();
        $('#modal-solicitud-cambio-estado .modal-content').empty();
        $('#modal-solicitud-cambio-estado .modal-content').load(url.ConfirmacionCambioEstado, { id: id }, function () {
            $('#modal-solicitud-cambio-estado').modal({ show: true, backdrop: 'static', keyboard: true });
            fn_DesbloquearPantalla();
        });

    }


    var abrirModalCambioEstadoAP = function (id) {
        debugger;
        fn_BloquearPantalla();
        $('#modal-solicitud-cambio-estado .modal-content').empty();
        $('#modal-solicitud-cambio-estado .modal-content').load(url.ConfirmacionCambioEstado, { id: id }, function () {
            $('#modal-solicitud-cambio-estado').modal({ show: true, backdrop: 'static', keyboard: true });

            for (var i = 1; i <= 12; i++) {
                if (i != 5) {
                    var n = "#dllEstados_CambioEstado option[value =" + i + "]";
                    $(n).remove();
                } else {
                    $('select#dllEstados_CambioEstado option:selected').val(5);

                }

            }

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

    var abrirRendirGastos = function (IdSolicitud) {
        debugger;
        fn_BloquearPantalla();
        $('#modal-solicitud-detalle-gasto .modal-content').empty();
        $('#modal-solicitud-detalle-gasto .modal-content').load(url.DetalleRendirGastos, { IdSolicitud: IdSolicitud }, function () {
            $("#modal-solicitud-detalle-gasto").modal({ show: true, backdrop: 'static', keyboard: true });
            //if (window.jsCharged) {
            //    $('#jqGrid3').jqGrid('clearGridData');
            //    $('#jqGrid3').jqGrid('setGridParam', {
            //        url: url.GetListDetalleRendirGasto + '?IdSolicitud=' + IdSolicitud
            //    }).trigger('reloadGrid');
            //}
            fn_DesbloquearPantalla();
        });
    }

})();