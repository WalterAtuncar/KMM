(function () {
    $(document).ready(function () {

        $('#btn-aprobar').click(function () {
            fn_BloquearPantalla();
            $('#btn-aprobar').prop('disabled', true);
            var id = $('#IdSolicitud').val();
            var idProveedor = getProveedortaxi();
            $.ajax({
                url: url.AprobarPost,
                data: { id: id, idProveedor: idProveedor },
                type: 'POST',
                async: true,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        window.location = '/Solicitud/Index?upd=true';
                    } else {
                        var MsjError = data.Message.substring(27).slice(0, -5);;
                        //toastr["error"]('Ocurrio un error en el servicio');
                        toastr["error"](MsjError);
                        fn_DesbloquearPantalla();
                    }
                },
                error: function (request, status, error) {
                    //console.log(request);
                    var MsjError = data.Message.substring(27).slice(0, -5);;
                    //toastr["error"]('Ocurrio un error en el servicio');
                    toastr["error"](MsjError);
                    fn_DesbloquearPantalla();
                }
            });
        });

        $('#btn-rechazar').click(function (e) {
            e.preventDefault();
            $('#txt-motivo-rechazo').val('');
            $('#modal-rechazar-confirm').modal('show');
        });

        $('body').on('click', '#btn-recorrido', function (e) {
            e.preventDefault();
            var pid = $(this).attr('data-id');
            abrirRecorrido(pid);
        });

        $('#btn-anular').click(function (e) {
            e.preventDefault();
            var idServicio = $(this).attr('data-pid');
            var id = $('#IdSolicitud').val();
            $.ajax({
                url: url.AnularServicioPost,
                data: { idServicio: idServicio, id: id },
                type: 'POST',
                async: false,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        window.location = '/Solicitud/Index?upd=true';
                    } else {
                        toastr["error"]('Ocurrio un error en el servicio');
                        fn_DesbloquearPantalla();
                    }
                },
                error: function (request, status, error) {
                    //console.log(request);
                    toastr["error"]('Ocurrio un error en el servicio');
                    fn_DesbloquearPantalla();
                }
            });
        });

        $('#btn-cancelar').click(function (e) {
            e.preventDefault();
            var idServicio = $(this).attr('data-pid');
            $.ajax({
                url: url.CancelarServicioPost,
                data: { idServicio: idServicio },
                type: 'POST',
                async: false,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        window.location = '/Solicitud/Index?upd=true';
                    } else {
                        toastr["error"]('Ocurrio un error en el servicio');
                        fn_DesbloquearPantalla();
                    }
                },
                error: function (request, status, error) {
                    //console.log(request);
                    toastr["error"]('Ocurrio un error en el servicio');
                    fn_DesbloquearPantalla();
                }
            });
        });

        $("#btn-cerrar-solicitud").click(function (e) {
            window.location = '/Solicitud/Index';
        });

        $('#btn-rechazar-confirm').click(function (e) {
            e.preventDefault();
            var $frm = $('#frm-eva');
            var formParsley = $frm.parsley(__defaultParsleyForm());
            var isValid = formParsley.validate();
            var IdSolicitud = document.getElementById('IdSolicitud').value;
            var motivoRechazo = document.getElementById('txt-motivo-rechazo').value;

            if (isValid) {
                var $btn = $(this);
                $.ajax({
                    url: url.RechazarPost,
                    data: { id: IdSolicitud, m: motivoRechazo },
                    dataType: 'JSON',
                    beforeSend: function () {
                        fn_BloquearPantalla();
                    },
                    type: 'POST',
                    async: true,
                    success: function (data) {
                        if (data.Success) {
                            window.location = '/Solicitud/Index?upd=true';
                        } else {
                            toastr["error"](data.Message);
                            fn_DesbloquearPantalla();
                        }
                    },
                    error: function (request, status, error) {
                        //console.log(request);
                        toastr["error"]('Ocurrio un error en el servicio');
                        fn_DesbloquearPantalla();
                    },
                    complete: function () {
                        fn_DesbloquearPantalla();
                    }
                });
            }
        });

        //$('[data-toggle="tooltip"]').tooltip();

        $('[data-toggle=confirmation]').click();

        addComboBeneficiado();

        $('[data-toggle=confirmation]').confirmation({
            rootSelector: '[data-toggle=confirmation]',
            onConfirm: function () {
                var pid = $(this).attr('data-pid');
                var selector = $(this);
                deleteBeneficiado(pid, selector);
            },
        });

    });

    $('body').on('click', '#btn-add-beneficiado', function (e) {
        e.preventDefault();
        if ($('#listBeneficiado option:selected').val() !== 0) {
            var beneficiado = {};
            beneficiado.SolicitudID = $("#IdSolicitud").val();
            beneficiado.IdTipoDocumento = 1;
            beneficiado.ID = $('#listBeneficiado option:selected').val();
            beneficiado.CodigoCentroCosto = $('#listBeneficiado option:selected').attr('data-centro');
            beneficiado.Nombre = $('#listBeneficiado option:selected').text().split(',')[1].trim();
            beneficiado.Apellidos = $('#listBeneficiado option:selected').text().split(',')[0].trim();
            beneficiado.ApellidoPaterno = beneficiado.Apellidos.split(' ')[0].trim();
            beneficiado.ApellidoMaterno = beneficiado.Apellidos.split(' ')[1].trim();
            beneficiado.Telefono = $('#listBeneficiado option:selected').attr('data-telefono');
            beneficiado.FlagPrincipal = false;
            beneficiado.NumeroDocumento = $('#listBeneficiado option:selected').attr('data-nrodocumento');
            ocultarBeneficiado();
            addBeneficiado(beneficiado);
        } else {
            toastr["info"]('Debe seleccionar un beneficiado');
        }

    });

    var ocultarBeneficiado = function () {
        var idBeneficiado = $('#listBeneficiado option:selected').prop("disabled", true);
        $('#listBeneficiado').select2();
        $('#listBeneficiado').val(0);
        $('#listBeneficiado').trigger("change");

    }

    var mostrarBeneficiado = function (pid) {
        $('#' + pid).prop("disabled", false);
        $('#listBeneficiado').select2();
        $('#listBeneficiado').val(0);
        $('#listBeneficiado').trigger("change");
    }

    var getProveedortaxi = function (e) {
        var idProveedor;
        $('#tblProveedorTaxi tbody tr').each(function (i) {
            if ($(this).find('.chkSeleccionado').is(":checked")) {
                idProveedor = $(this).find('#idProveedorSeleccionado').val();
            }
        });
        return idProveedor;
    }

    var abrirRecorrido = function (pid) {
        fn_BloquearPantalla();
        $('#modal-recorrido .modal-content').empty();
        $('#modal-recorrido .modal-content').load(url.RecorridoSolicitud, { id: pid }, function () {
            fn_DesbloquearPantalla();
        });
        $('#modal-recorrido').modal('show');
    }

    var addComboBeneficiado = function () {
        var listaBeneficiado = $('#InputBeneficiado').val();
        $('#div-beneficiado-add').html(listaBeneficiado + '<span class="input-group-addon" id="btn-add-beneficiado" title="Agregar"><i class="fa fa-user-plus"></i></span>');
        $('#tbl-beneficiado tbody tr').each(function (i) {
            var id = $($(this).children("td")[1]).text();
            $('#' + id).prop("disabled", true);
        });
        $('.select2').select2();
    }

    var deleteTableBeneficiado = function (selector) {
        selector.parent().parent().remove();
        enumerar();
    }

    var enumerar = function () {
        $("#tbl-beneficiado tbody tr").each(function (index) {
            $($(this).children()[0]).text(index + 1);
        });
    }

    var deleteBeneficiado = function (pid, selector) {
        fn_BloquearPantalla()
        $.ajax({
            url: url.EliminarBeneficiado,
            data: { id: pid, idSolicitud: $("#IdSolicitud").val() },
            type: 'POST',
            async: false,
            cache: false,
            success: function (data) {
                if (data.Success) {
                    //console.log(data);
                    mostrarBeneficiado(pid);
                    deleteTableBeneficiado(selector);
                    toastr["success"]('Se eliminó correctamente');
                    fn_DesbloquearPantalla();
                } else {
                    toastr["error"]('Ocurrio un error en el servicio');
                    fn_DesbloquearPantalla();
                }
            },
            error: function (request, status, error) {
                //console.log(request);
                toastr["error"]('Ocurrio un error en el servicio');
                fn_DesbloquearPantalla();
            }
        });
    }

    var addBeneficiado = function (beneficiado) {
        fn_BloquearPantalla();
        $.ajax({
            url: url.AgregarBeneficiado,
            data: { beneficiado: beneficiado },
            type: 'POST',
            async: false,
            cache: false,
            success: function (data) {
                if (data.Success) {
                    //console.log(data);
                    var countBeneficiado = $('#tbl-beneficiado tbody tr').length;
                    var row = '<tr><td class="text-center col-sm-1"></td ><td style="display: none;">' + beneficiado.ID + '</td><td>' + beneficiado.Apellidos + '</td><td>' + beneficiado.Nombre + '</td><td>' + beneficiado.CodigoCentroCosto + '</td><td class="text-center"><a data-toggle="confirmation" data-container="body" data-popout="true" data-title="¿Está seguro de eliminar a este beneficiario?" data-singleton="true" data-placement="left" data-btnOkLabel="Aceptar" data-btnCancelLabel="Cancelar" data-pid="' + beneficiado.ID + '" class="fa fa-trash-o btn-eliminar-beneficiario" style="text-decoration: unset; color: #333;"></a></td></tr>';
                    $('#tbl-beneficiado tbody').append(row);
                    $('[data-toggle=confirmation]').confirmation({
                        rootSelector: '[data-toggle=confirmation]',
                        onConfirm: function () {
                            var pid = $(this).attr('data-pid');
                            var selector = $(this);
                            deleteBeneficiado(pid, selector);
                        },
                    });
                    enumerar();
                    toastr["success"]('Se agregó correctamente');
                    fn_DesbloquearPantalla();
                } else {
                    toastr["error"]('Ocurrio un error en el servicio');
                    fn_DesbloquearPantalla();
                }
            },
            error: function (request, status, error) {
                //console.log(request);
                toastr["error"]('Ocurrio un error en el servicio');
                fn_DesbloquearPantalla();
            }
        });
    }
})()