(function () {

    $(document).ready(function () {
        debugger;
        var idLiquidacion = $('#LiquidacionID').val();
        var codigoSociedad = $('#CodigoSociedad').val();

        $('#GuardarLiquidacionButton').click(function (e) {
            e.preventDefault();
            Liquidar();
        });

        //
        //Permite mostrar el detalle de la liquidacion
        var $nav_link_tab = $('.nav-link-tab').click(function (e) {
            var index = $(this).data('index');
            e.preventDefault();
            $('#GuardarLiquidacionButton').prop('disabled', true);
            if (index == 1) {
                $('#GuardarLiquidacionButton').prop('disabled', false);
                $('#tblDetalleLiquidacion tbody').empty();
                fn_BloquearPantalla();
                var html = "";
                $.getJSON(url.GetLiquidacionDetalle + '?idLiquidacion=' + idLiquidacion
                ).done(function (data) {
                    var lista = $('#inputList').val();
                    var input = lista;
                    $.each(data, function (key, val) {
                        html += '<tr id="tr_' + val.ID + '" role="row" data-id="' + val.ID + '" data-ordenservicio="' + val.OrdenServicio + '" data-centrocostoafecto="' + val.CentroCostoAfecto + '" style="min-height:15px; max-height:18px;">';
                        html += '<td scope="row">' + parseInt(key + 1) + '</td>';
                        html += '<td>' + val.CodigoSolicitud + '</td>';
                        html += '<td>' + val.FechaServicio + '</td>';
                        html += '<td>' + val.HoraServicio + '</td>';
                        html += '<td>' + val.Beneficiado + '</td>';
                        html += '<td>' + val.Documento + '</td>';
                        if (val.OrdenServicio != "") {
                            html += '<td><input id="txtOS-' + val.ID + '" data-id="' + val.ID + '" type="text" class="input-sm texto-os" value="' + val.OrdenServicio + '" style="width:95px;" /></td>';
                            html += '<td id="td_cc_nombre_' + val.ID + '">' + val.CentroCostoAfecto + ' - ' + val.NombreCentroCostoAfecto + '</td>';
                        } else {
                            html += '<td></td>';
                            html += '<td>' + input.replace('[DLLCECO]', 'dllCeco-' + val.ID) + '</td>';
                        }
                        html += '<td>' + val.Aprobador + '</td>';
                        html += '<td class="text-right">' + val.PrecioTarifa + '</td>';
                        html += '<td class="text-right">' + val.PrecioDesvioRuta + '</td>';
                        html += '<td class="text-right">' + val.PrecioPeaje + '</td>';
                        html += '<td class="text-right">' + val.PrecioEstacionamiento + '</td>';
                        html += '<td class="text-right">' + val.PrecioEstacionamiento + '</td>';
                        html += '<td class="text-right">' + val.PrecioDesplazamiento + '</td>';
                        html += '<td class="text-right">' + val.TotalGeneral + '</td>';
                        html += '</tr>';
                    })
                }).fail(function (jqXHR, textStatus, error) {
                    fn_DesbloquearPantalla();
                }).always(function () {
                    $('#tblDetalleLiquidacion tbody').append(html);
                    cargarMetodos();
                    fn_DesbloquearPantalla();
                });
            }
        });

        //fn_BloquearPantalla();
        //$('#div-tbl-solicitudes').load(url.GetLiquidacionDetalle, { idLiquidacion: idLiquidacion, codigoSociedad: codigoSociedad }, function (e) {
        //    cargarMetodos();
        //    fn_DesbloquearPantalla();
        //});

        var $input = $("#archivo-adjunto");

        $input.fileinput({
            language: "es",
            maxFileCount: 1,
            showPreview: false,
            maxFileSize: '91324',
            msgSizeTooLarge: 'Tamaño máximo de archivo: 90 MB',
            uploadUrl: '/Liquidacion/UploadFile',
            uploadExtraData: { idLiquidacion: $('#LiquidacionID').val()},
            elErrorContainer: "#errorBlock-producto",
            showCancel: false,
            showUpload: false,
            showRemove: false,
            showCaption: false,
            browseClass: "btn btn-primary btn-sm",
            removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
            uploadAsync: false,
            allowedFileExtensions: ["jpg", "png","pdf","doc","xls"]
        }).on('filebatchselected', function (event, files) {
            $input.fileinput('upload');
        }).on('filebatchuploadsuccess', function (event, data) {
            if (data.response.code == 1) {
                $("#content-attachment").html(data.response.partial).promise().done(function () {
                    $(".remove-item-adjunto").click(removeFile);
                });
            }
            else
                notifyError('Ocurrio un error inesperado');
        });
    });


    var removeFile = function () {
        $.ajax('/Liquidacion/RemoveFile', {
            cache: false,
            type: "GET",
            beforeSend: function () {
                fn_BloquearPantalla();
            },
            success: function (form) {
                $("#content-attachment").html(form).promise().done(function () {
                    $(".remove-item-adjunto").click(removeFile);
                });
            },
            complete: function () {
                fn_DesbloquearPantalla();
            }
        })
    }

    $('.remove-item-adjunto').click(function () {
        removeFile();
    });


    var cargarMetodos = function () {
        $('#tblDetalleLiquidacion tbody tr').each(function () {
            var Id = $(this).data("id");
            var OrdenServicio = $(this).data('ordenservicio');
            var CentroCosto = $(this).data('centrocostoafecto');
            if (OrdenServicio == "") {
                $('#dllCeco-' + Id).val(CentroCosto).select2();
            }
            //validacionRow($(this));
        });
    }

    $('#tblDetalleLiquidacion').on('blur', '.texto-os', function (e) {
        e.preventDefault();
        if ($(this).val() !== "") {
            validarOrdenServicio($(this));
        }
    });

    $('#tblDetalleLiquidacion').on('keyup', '.texto-os', function (e) {
        e.preventDefault();
        if (e.which === 13) {
            if ($(this).val() !== "") {
                validarOrdenServicio($(this));
            }
        }
    });

    var validarOrdenServicio = function (elemento) {
        debugger;
        var ordenServicio = elemento.val();
        var id = elemento.data('id');
        if (elemento.val().trim() !== "") {
          
            $.ajax({
                url: '/Solicitud/ValidarOrdenServicioLiquidacion',
                type: 'POST',
                data: { ordenServicio: ordenServicio },
                cache: false,
                async: true,
                beforeSend: function () {
                    fn_BloquearPantalla();
                },
                success: function (data) {
                    if (data.Success) {
                        var codigoSociedad = $('#CodigoSociedad').val();
                        if (codigoSociedad != data.ObjectResult.BUKRS) {
                            toastr["error"]('OS no pertenece a la misma Sociedad');
                            elemento.css('border', '1px solid red');
                            elemento.val('')
                        } else {
                            $('#tr_' + id).data('centrocostoafecto', data.ObjectResult.KOSTV);
                            $('#td_cc_nombre_' + id).html(data.ObjectResult.KTEXT);
                            elemento.css('border', '0px solid red')
                        }
                        fn_DesbloquearPantalla();
                    } else {
                        toastr["error"](data.Message);
                        elemento.css('border', '1px solid red')
                        fn_DesbloquearPantalla();
                    }
                },
                complete: function () {
                    fn_DesbloquearPantalla();
                }
            });
        }
       
    }

    //var paginadoDetalle = function () {
    //    $("#tbl-solicitudes").jqGrid({
    //        url: getURLModalLiquidar($('#LiquidacionID').val()),
    //        datatype: 'json',
    //        mtype: 'GET',
    //        colNames: ['Opciones', 'Código Servicio', 'FechaServicio', 'Hora', 'Beneficiado', 'Documento', 'O/S', 'Centro Costo', 'Aprobador', 'Tarifa', 'T.Espera', 'T.DesvRuta', 'T.Peaje', 'T.Estacionamiento', 'T.General'],
    //        colModel: [
    //            { key: true, name: 'ID', index: 'ID', hidden: true },
    //            { key: false, name: 'CodigoSolicitud', index: 'CodigoSolicitud', align: 'center', width: '94px' },
    //            { key: false, name: 'FechaServicioStr', index: 'FechaServicioStr', align: 'center', width: '94px' },
    //            { key: false, name: 'HoraServicio', index: 'HoraServicio', align: 'center', width: '72px' },
    //            { key: false, name: 'Beneficiado', index: 'Beneficiado', align: 'left', width: '284px' },
    //            { key: false, name: 'Documento', index: 'Documento', align: 'center', width: '75px' },
    //            { key: false, name: 'OrdenServicio', index: 'OrdenServicio', align: 'center', width: '98px', editable: true },
    //            {
    //                key: false, name: 'CentroCostoAfecto', index: 'CentroCostoAfecto', align: 'center', width: '250px', editable: true, resizable: false, fixed: true, formatoptions: { keys: true, }, edittype: "select",
    //                editoptions: {
    //                    dataUrl: url.GetListCentroCostoCombo,
    //                    buildSelect: function (data) {
    //                        fn_BloquearPantalla();
    //                        var list = $.parseJSON(data);
    //                        var s = '<select class="select2">';
    //                        list.forEach(function (e) {
    //                            s += '<option value="' + e.Codigo + '">' + e.Codigo + ' - ' + e.Descripcion + '</option>';
    //                        });

    //                        var lista = s + "</select>";

    //                        fn_DesbloquearPantalla();
    //                        return lista
    //                    },
    //                    class: 'select2',
    //                    completeProcessing: function (a) {
    //                        $('.select2').select2();
    //                    },
    //                    error: function (a, b, e) {
    //                        fn_DesbloquearPantalla()
    //                    }
    //                }
    //            },
    //            { key: false, name: 'Aprobador', index: 'Aprobador', align: 'center', width: '155px' },
    //            { key: false, name: 'PrecioTarifalStr', index: 'PrecioTarifalStr', align: 'center', width: '67px' },
    //            { key: false, name: 'PrecioEsperaStr', index: 'PrecioEsperaStr', align: 'center', width: '67px' },
    //            { key: false, name: 'PrecioDesvioRutaStr', index: 'PrecioDesvioRutaStr', align: 'center', width: '70px' },
    //            { key: false, name: 'PrecioPeajeStr', index: 'PrecioPeajeStr', align: 'center', width: '67px' },
    //            { key: false, name: 'PrecioEstacionamientoStr', index: 'PrecioEstacionamientoStr', align: 'center', width: '105px' },
    //            { key: false, name: 'TotalGeneralStr', index: 'TotalGeneralStr', align: 'center', width: '100px' }
    //        ],
    //        pager: $('#pager-tbl-solicitudes'),
    //        rowNum: 10,
    //        rowList: [10, 20, 30, 40, 50],
    //        height: '100%',
    //        viewrecords: true,
    //        //caption: 'Students Records',
    //        //emptyrecords: 'No Students Records are Available to Display',
    //        jsonReader: {
    //            root: "rows",
    //            page: "page",
    //            total: "total",
    //            records: "records",
    //            repeatitems: false,
    //            //Id: "0"
    //        },
    //        autowidth: true,
    //        multiselect: false,
    //        rownumbers: true,
    //        footerrow: true,
    //        cmTemplate: { sortable: false },
    //        beforeProcessing: function (id) {
    //            $('select[name="CentroCostoAfecto"]').select2().trigger('change');
    //        },
    //        loadComplete: function () {
    //            var totalSum = $('#tbl-solicitudes').jqGrid('getCol', 'TotalGeneralStr', false, 'sum');
    //            var totalSumStr = totalSum.toFixed(2);
    //            $('#tbl-solicitudes').jqGrid('footerData', 'set', { ClientName: 'Total:', TotalGeneral: totalSumStr });
    //        },
    //        onSelectRow: function (id) {
    //            $('#tbl-solicitudes').jqGrid('editRow', id, true);
    //            $('#' + id + '_CentroCostoAfecto').trigger('change');
    //        },
    //        //editurl: url.PostSaveRowSolicitud
    //    });
    //}

    var getModelo = function () {
        var modelo = $('#frm-liquidar').serializeObject();
        modelo['Liquidacion.ListaSolicitud'] = getDetalleSerialize();
        return modelo;
    }

    var getDetalleSerialize = function () {
        var SolicitudLiquidacion = function () { };
        var arrayDetalle = new Array();
        $('#tblDetalleLiquidacion tbody tr').each(function (i) {
            var Id = $(this).data('id');
            var CentroCostoAfecto = $(this).data('centrocostoafecto');
            var solicitudLiquidacionDetalle = new SolicitudLiquidacion();
            solicitudLiquidacionDetalle.ID = $(this).data('id');
            if ($(this).data('ordenservicio') != "") {
                solicitudLiquidacionDetalle.OrdenServicio = $('#txtOS-' + Id).val();
                solicitudLiquidacionDetalle.CentroCostoAfecto = CentroCostoAfecto;
            } else {
                solicitudLiquidacionDetalle.CentroCostoAfecto = $($(this).find('select')).val();
            }
            arrayDetalle.push(solicitudLiquidacionDetalle);
        });

        return arrayDetalle;
    }

    function Liquidar() {
        debugger;
        fn_BloquearPantalla();
        $('#GuardarLiquidacionButton').prop('disabled', true);
        var formulario = $('#frm-liquidar');
        $.validator.unobtrusive.parse("#frm-liquidar");
        if (formulario.valid()) {
            fn_BloquearPantalla();
            var modelo = getModelo();
            $.ajax({
                url: url.PostLiquidar,
                data: JSON.stringify({ modelo: modelo }),
                type: 'POST',
                beforeSend: function () { fn_BloquearPantalla();},
                async: true,
                cache: false,
                contentType: "application/json",
                success: function (data) {
                    if (data.Success) {
                        toastr["success"]('Liquidado correctamente');
                        $('#modal-liquidar').modal('hide');
                        $('#jqGrid').jqGrid('clearGridData');
                        $('#jqGrid').trigger('reloadGrid');
                    } else {
                   //     if (data.ObjectResult == null) {
                   //         toastr["error"](data.Message);
			                //$('#GuardarLiquidacionButton').prop('disabled', false);
                   //     } else {
                   //         pintarOS(data);
                   //         toastr["error"]("Verificar los OS");
			                //$('#GuardarLiquidacionButton').prop('disabled', false);
                   //     }
                        if (data.ObjectResult == null) {
                            toastr.error(data.Message);
                            $('#GuardarLiquidacionButton').prop('disabled', false);
                        } else {
                            //pintarOS(data);
                            if (data.ObjectResult !== undefined && data.ObjectResult !== null) {
                                var rows = [];

                                data.ObjectResult.ObjectResult.forEach(function (obj) {
                                    if (obj.mensaje !== null) {
                                        // Agregar una fila a la tabla con los valores de OS y Mensaje
                                        rows.push('<tr><td>' + obj.AUFNR + '</td><td>' + obj.mensaje + '</td></tr>');
                                    }
                                });

                                // Construir la tabla HTML con cabeceras OS y Mensaje
                                if (rows.length > 0) {
                                    var table = '<table><thead><tr><th>OS</th><th>Mensaje</th></tr></thead><tbody>' + rows.join('') + '</tbody></table>';
                                    toastr.error(table);
                                } else {
                                    toastr.error("Verificar los OS");
                                }
                            } else {
                                toastr.error("Verificar los OS");
                            }

                            // Habilitar el botón GuardarLiquidacionButton
                            $('#GuardarLiquidacionButton').prop('disabled', false);
                        }
                    }
                    fn_DesbloquearPantalla();
                },
                complete: function () {
                    fn_DesbloquearPantalla();
                }
            });

        } else {
            toastr["error"]('Complete los campos obligatorios');
	        $('#GuardarLiquidacionButton').prop('disabled', false);
        }
    }

    var pintarOS = function (data) {
        data.ObjectResult.forEach(function (i, value) {
            $('#txtOS-' + i).css({ "border-color": "red" })
        });

    }

})();