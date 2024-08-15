(function () {

    $(document).ready(function () {
        paginado();
        $('.filtro').attr('disabled', true);
        $('#chkRuc').change(function () { checkRuc(this) });
        $('#chkCodigoSap').change(function () { checkCodigoSap(this) });
        $('#chkRazonSocial').change(function () { checkRazonSocial(this) });

        $('#btnBuscar_Proveedor').click(function () {
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

    function getURLGet() {
        var Ruc = $('#txtRuc').is(':disabled') ? '' : $('#txtRuc').val();
        var CodigoSap = $('#txtCodigoSap').is(':disabled') ? '' : $('#txtCodigoSap').val();
        var RazonSocial = $('#txtRazonSocial').is(':disabled') ? '' : $('#txtRazonSocial').val();
        
        return url.GetListProveedor + '?Ruc=' + Ruc + '&RazonSocial=' + RazonSocial + '&CodigoSap=' + CodigoSap;
    }

    var checkRuc = function (element) {

        if (element.checked) {
            $('#txtRuc').prop('disabled', false);
            $('#txtRuc').focus();
        } else {
            $('#txtRuc').prop('disabled', true);
            $('#txtRuc').val("");
        }
    }
    
    var checkCodigoSap = function (element) {
        if (element.checked) {
            $('#txtCodigoSap').prop('disabled', false);
            $('#txtCodigoSap').focus();
        } else {
            $('#txtCodigoSap').prop('disabled', true);
            $('#txtCodigoSap').val("");
        }
    }

    var checkRazonSocial = function (element) {
        if (element.checked) {
            $('#txtRazonSocial').prop('disabled', false);
            $('#txtRazonSocial').focus();
        } else {
            $('#txtRazonSocial').prop('disabled', true);
            $('#txtRazonSocial').val("");
        }
    }

    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListProveedor,
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Opciones', 'RUC', 'Razón Social', 'Contacto', 'Teléfono', 'Dirección',
                'Código SAP', 'Estado'],
            colModel: [
                {
                    key: false,
                    name: 'ID',
                    index: 'ID',
                    align: 'center',
                    fixed: true,
                    width: '95px',
                    formatter: formatoOpciones,
                    formatoptions: { keys: true }
                },
                { key: false, name: 'RUC', index: 'RUC', align: 'center' },
                { key: false, name: 'RazonSocial', index: 'RazonSocial' },
                { key: false, name: 'Contacto', index: 'Contacto' },
                { key: false, name: 'Telefono', index: 'Telefono', align: 'center' },
                { key: false, name: 'Direccion', index: 'Direccion' },
                { key: false, name: 'CodigoSAP', index: 'CodigoSAP', align: 'center' },
                { key: false, name: 'Estado', index: 'Estado' }
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
                repeatitems: false
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
                //$('[data-toggle="tooltip"]').tooltip();
                fn_DesbloquearPantalla();
            }
        });
    };

    $('#btn-create').click(function (e) {
        e.preventDefault();
        abrirModal();
    });


    $('body').on('click', "#btn-save-credencial", function (e){
        e.preventDefault();
        saveCredencial();
    });

    $('body').on('click', "#btn-edit-proveedor", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirModal(pid);
    });

    $('body').on('click', '#btn-servicio-proveedor', function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        loadListMethod(pid);
    });

    $('body').on('click', '#btn-credenciales-proveedor', function(e){
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirModalCredenciales(pid);
    });

    $('body').on('click', '#btn-save-method', function (e) {
        e.preventDefault();
        saveListMethod();
    });

    var formatoOpciones = function (cellvalue, options, rowObject) {
        return '<a href="javascript(0);" id="btn-edit-proveedor" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-pencil" title="Editar"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="javascript(0);" id="btn-servicio-proveedor" data-pid ="' + cellvalue + '"  class="fa fa-check-square-o" title = "Servicios"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="javascript(0);" id="btn-credenciales-proveedor" data-pid ="' + cellvalue + '"  class="fa fa-key" title = "Credenciales"></a>';
    };

    var saveCredencial = function(){
        var formulario = $('#frm-add-credencial');
        $.validator.unobtrusive.parse("#frm-add-credencial");
        fn_BloquearPantalla();
        if (formulario.valid()) {
            $.ajax({
                url: url.RegistrarCredenciales,
                data: formulario.serialize(),
                type: 'POST',
                async: false,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        toastr["success"]('Registro Correcto');
                        $('#modal-credenciales').modal('hide');
                        $('#jqGrid').jqGrid('clearGridData');
                        $('#jqGrid').trigger('reloadGrid');
                        fn_DesbloquearPantalla();
                    } else {
                        toastr["error"]('Ocurrio un error inesperado');
                        fn_DesbloquearPantalla();
                    }
                }
            });
        } else{
            fn_DesbloquearPantalla();
            toastr["error"]('Complete los campos obligatorios');
        }
    };

    var saveProveedor = function () {
        var formulario = $('#frm-add-proveedor');
        $.validator.unobtrusive.parse("#frm-add-proveedor");
       
        if (formulario.valid()) {
            $.ajax({
                url: url.RegistrarPost,
                data: formulario.serialize(),
                type: 'POST',
                dataType: 'JSON',
                beforeSend: function () {
                    fn_BloquearPantalla();
                },
                async: true,
                cache: false,
                success: function (data) {
                    if (data.Success) {
                        toastr["success"]('Registro Correcto');
                        $('#modal-proveedor').modal('hide');
                        $('#jqGrid').jqGrid('clearGridData');
                        $('#jqGrid').trigger('reloadGrid');
                        fn_DesbloquearPantalla();
                    } else {
                        toastr["error"](data.Message);
                        fn_DesbloquearPantalla();
                    }
                },
                error: function (data) {
                    //console.log(data)
                },
                complete: function () {
                    fn_DesbloquearPantalla();
                }

            });
        } else {
            fn_DesbloquearPantalla();
            toastr["error"]('Complete los campos obligatorios');
        }
    };

    var abrirModal = function (id) {
        fn_BloquearPantalla();
        $('#modal-proveedor .modal-content').empty();
        $('#modal-proveedor .modal-content').load(url.Registrar, { id: id }, function () {
            var btnSaveProveedor = document.getElementById('btn-save');
            btnSaveProveedor.addEventListener('click', saveProveedor, false);
            fn_DesbloquearPantalla();
        });

        $('#modal-proveedor').modal('show');
    };

        var abrirModalCredenciales = function(id){
            fn_BloquearPantalla();
            $('#modal-credenciales .modal-content').empty();
            $('#modal-credenciales .modal-content').load(url.Credenciales, { id: id }, function () {
                fn_DesbloquearPantalla();
            });
            $('#modal-credenciales').modal('show');
        };

            var loadListMethod = function (id) {
                fn_BloquearPantalla();
                $('#modal-proveedor .modal-content').empty();
                $('#modal-proveedor .modal-content').load(url.GetListMethod, { id: id }, function () {
                    $('#IdProveedor').val(id);
                    fn_DesbloquearPantalla();
                });

                $('#modal-proveedor').modal('show');
            };

                var saveListMethod = function () {
                    var listMethod = getListMethot();
                    var ProveedorTaxiModel = new Object();
                    ProveedorTaxiModel.Proveedor = new Object();
                    ProveedorTaxiModel.Proveedor.ID = $('#IdProveedor').val();
                    ProveedorTaxiModel.ListaMetodos = listMethod;

                    $.ajax({
                        url: url.RegistrarMethodPost,
                        contentType: "application/json",
                        data: JSON.stringify({ modelo: ProveedorTaxiModel }),
                        type: 'POST',
                        async: false,
                        cache: false,
                        success: function (data) {
                            if (data.Success) {
                                toastr["success"]('Registro Correcto');
                                $('#modal-proveedor').modal('hide');
                            } else {
                                //console.log(data.Error),
                                    toastr["error"]('Ocurrio un error inesperado');
                            }
                        }
                    });
                }

                var getListMethot = function () {
                    var usp_ServicioProveedorTaxi_List_Result = function () { };
                    var arrayDetalle = new Array();
                    $('#tblMethod tbody tr').each(function (i) {
                        var methodProveedor = new usp_ServicioProveedorTaxi_List_Result();
                        methodProveedor.Nombre = $(this).find('.txtNombre').text();
                        methodProveedor.IdMetodoProveedorTaxi = $(this).find('.txtIdMetodoProveedorTaxi').val();
                        methodProveedor.URL = $(this).find(".txtUrl").val();
                        methodProveedor.Metodo = $(this).find(".txtMetodo").val();
                        methodProveedor.IdProveedor = $('#IdProveedor').val();
                        arrayDetalle.push(methodProveedor);
                    });

                    return arrayDetalle;
                }
   

            })();
