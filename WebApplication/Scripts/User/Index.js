(function () {

    var validaciones = true;

    $(document).ready(function () {
        $('#ddlPerfilFiltro').select2();
        $('.filtro').attr('disabled', true);

        $('#chkNombre').change(function () { checkNombre(this) });
        $('#chkEmail').change(function () { checkEmail(this) });
        $('#chkDocumento').change(function () { checkDocumento(this) });

        $('#chkApellido').change(function () { checkApellido(this) });
        $('#chkCentroCosto').change(function () { checkCentroCosto(this) });
        $('#chkPerfil').change(function () { checkPerfil(this) });

        paginado();

        $('#btnBuscarUsers').click(function () {
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


    var checkNombre = function (element) {
        if (element.checked) {
            $('#txtNombre').prop('disabled', false);
            $('#txtNombre').focus();
        } else {
            $('#txtNombre').prop('disabled', true);
            $('#txtNombre').val("");
        }
    }

    var checkEmail = function (element) {
        if (element.checked) {
            $('#txtEmail').prop('disabled', false);
            $('#txtEmail').focus();
        } else {
            $('#txtEmail').prop('disabled', true);
            $('#txtEmail').val("");
        }
    }

    var checkDocumento = function (element) {
        if (element.checked) {
            $('#txtDocumento').prop('disabled', false);
            $('#txtDocumento').focus();
        } else {
            $('#txtDocumento').prop('disabled', true);
            $('#txtDocumento').val("");
        }
    }

    var checkApellido = function (element) {
        if (element.checked) {
            $('#txtApellido').prop('disabled', false);
            $('#txtApellido').focus();
        } else {
            $('#txtApellido').prop('disabled', true);
            $('#txtApellido').val("");
        }
    }

    var checkCentroCosto = function (element) {
        if (element.checked) {
            $('#txtCentroCosto').prop('disabled', false);
            $('#txtCentroCosto').focus();
        } else {
            $('#txtCentroCosto').prop('disabled', true);
            $('#txtCentroCosto').val("");
            $('#txtCentroCosto').trigger("change")
        }
    }

    var checkPerfil = function (element) {
        if (element.checked) {
            $('#ddlPerfilFiltro').prop('disabled', false);
            $('#ddlPerfilFiltro').focus();
        } else {
            $('#ddlPerfilFiltro').prop('disabled', true);
            $('#ddlPerfilFiltro').val("");
            $('#ddlPerfilFiltro').trigger("change")
        }
    }

    function getURLGet() {
       
        var nombres = $('#txtNombre').val();
        var apellidos = $('#txtApellido').val();
        var email = $('#txtEmail').val();
        var codigocentrocosto = $('#txtCentroCosto').val();
        var numerodocumento = $('#txtDocumento').val();
        var idperfil = $('#ddlPerfilFiltro').val();

        return url.GetListUsers + '?nombres=' + nombres + '&apellidos=' + apellidos + '&email=' + email + '&codigocentrocosto=' + codigocentrocosto + '&numerodocumento=' + numerodocumento + '&idperfil=' + idperfil;
    }

    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListUsers,
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Opciones', 'Nombres', 'Apellidos', 'Email', 'Centro Costo','Telefono', 'Numero Documento',
                'Perfil', 'Estado'],
            colModel: [
                {
                    key: false,
                    name: 'ID',
                    index: 'Options',
                    align: 'center',
                    width: '95px',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { keys: true, }
                },
                { key: false, name: 'Nombres', index: 'Nombres', align: 'center' },
                { key: false, name: 'Apellidos', index: 'Apellidos' },
                { key: false, name: 'Email', index: 'Email' },
                { key: false, name: 'CentroCosto', index: 'CentroCosto' },
                { key: false, name: 'Telefono', index: 'Telefono' },
                { key: false, name: 'NumeroDocumento', index: 'NumeroDocumento' },
                { key: false, name: 'Perfil', index: 'Perfil' },
                { key: false, name: 'Estado', index: 'Estado' },
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
        return '<a href="javascript(0);" id="btn-edit-user" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-pencil"></a>';
    }

    $('body').on('click', "#btn-edit-user", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirModal(pid)
    });

    $('body').on('change', "#ddlCentroCosto", function (e) {
        e.preventDefault();
        $("#select2-ddlCentroCosto-container").css('border', '0px solid red');
    });

    $('body').on('click', '#btn-save', function (e) {
        e.preventDefault();
        saveUser();
    });

    $('body').on('change', '#ddlPerfil', function () {
        changePerfil();
    });

    var changePerfil = function () {
        var idPerfil = $('#ddlPerfil').val();
        if (idPerfil !== "2" && idPerfil !== "28") {
            $('.div-multiselect').hide();
            $('#ddlCentroCostoMultiSelect').val('');
        } else {
            $('.div-multiselect').show();
        }
    }

    var abrirModal = function (id) {
        fn_BloquearPantalla();
        $('#modal-usuario .modal-content').empty();
        $('#modal-usuario .modal-content').load(url.Registrar, { id: id }, function () {
            fn_DesbloquearPantalla();
            changePerfil();
        });
        $('#modal-usuario').modal('show');
    }

    var saveUser = function () {
        var formulario = $('#frm-add-user');
        $.validator.unobtrusive.parse("#frm-add-user");
        validar();
        fn_BloquearPantalla();
        var CentroCostoMultiSelect = $('#ddlCentroCostoMultiSelect').val() === null ? [] : $('#ddlCentroCostoMultiSelect').val()
        $('#IdListaCentoCosto').val(CentroCostoMultiSelect.join(','));
        var CentroCosto = $('#ddlCentroCosto').val();
        $('#IdCentroCosto').val(CentroCosto);
        if (formulario.valid() && validaciones) {
            $.ajax({
                url: url.RegistrarPost,
                data: formulario.serialize(),
                type: 'POST',
                async: false,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        toastr["success"]('Registro Correcto');
                        $('#modal-usuario').modal('hide');
                        $('#jqGrid').jqGrid('clearGridData');
                        $('#jqGrid').trigger('reloadGrid');
                        fn_DesbloquearPantalla();
                    } else {
                        //console.log(data.Error),
                            toastr["error"]('Ocurrio un error inesperado');
                        fn_DesbloquearPantalla();
                    }
                }

            });
        } else {
            toastr["error"]('Complete los campos obligatorios');
            fn_DesbloquearPantalla();
        }
    }

    var validar = function () {
        validaciones = true;
        if ($('#ddlPerfil').val() === 2 && $('#ddlCentroCosto').val() === "") {
            $("#select2-ddlCentroCosto-container").css('border', '1px solid red');
            validaciones = false;
        }
    }

})();