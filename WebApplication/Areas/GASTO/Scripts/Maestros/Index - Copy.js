(function () {

    $(document).ready(function () {
        
        paginado();
        //$('.filtro').attr('disabled', true);
        //$('#chkRuc').change(function () { checkRuc(this) });
        //$('#chkCodigoSap').change(function () { checkCodigoSap(this) });
        //$('#chkRazonSocial').change(function () { checkRazonSocial(this) });

        $('#btnBuscar_Maestros').click(function () {
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

    $('body').on('click', '#btn-new-maestro-tabla', function (e) {
        e.preventDefault();
        var IdTabla = $(this).attr('data-pid');
        if (IdTabla == 10) {
            abrirUsuarioSucursal(0);
        }
        if (IdTabla == 20) {
            abrirConfiguraciones(0);
        }
        if (IdTabla == 21) {
            abrirSociedadSucursal(0);
        }
    });
    $('body').on('click', '#btn-edit-tabla10', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-10');
        abrirUsuarioSucursal(id);
    });

    $('body').on('click', '#btn-del-tabla10', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-10');
        EliminarUsuarioSucursal(id);
    });

    $('body').on('click', '#btn-edit-tabla21', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-21');
        abrirSociedadSucursal(id);
    });
    $('body').on('click', '#btn-del-tabla21', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-21');
        EliminarSociedadSucursal(id);
    });

    $('body').on('click', '#btn-edit-tabla20', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-20');
        abrirConfiguraciones(id);
    });
    $('body').on('click', '#btn-del-tabla20', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-20');
        EliminarConfiguraciones(id);
    });


    function getURLGet() {
        //var Ruc = $('#txtRuc').is(':disabled') ? '' : $('#txtRuc').val();
        //var CodigoSap = $('#txtCodigoSap').is(':disabled') ? '' : $('#txtCodigoSap').val();
        //var RazonSocial = $('#txtRazonSocial').is(':disabled') ? '' : $('#txtRazonSocial').val();
        
        //return url.GetListMaestros + '?Ruc=' + Ruc + '&RazonSocial=' + RazonSocial + '&CodigoSap=' + CodigoSap;
        return url.GetListMaestros;
    }

    function getURLGetDetalleMaestroLista() {
        //var Ruc = $('#txtRuc').is(':disabled') ? '' : $('#txtRuc').val();
        //var CodigoSap = $('#txtCodigoSap').is(':disabled') ? '' : $('#txtCodigoSap').val();
        //var RazonSocial = $('#txtRazonSocial').is(':disabled') ? '' : $('#txtRazonSocial').val();

        //return url.GetListMaestros + '?Ruc=' + Ruc + '&RazonSocial=' + RazonSocial + '&CodigoSap=' + CodigoSap;
        return url.GetDetalleMaestroLista+'?page=1&rows=100';
    }
    

    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListMaestros,
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Opciones', 'ID', 'ID Tabla','Tabla'],
            colModel: [
                {
                    key: false,
                    name: 'IdTabla',
                    index: 'IdTabla',
                    align: 'center',
                    fixed: true,
                    width: '95px',
                    formatter: formatoOpciones,
                    formatoptions: { keys: true }
                },
                { key: false, name: 'ID', index: 'ID', align: 'center' },
                { key: true, name: 'IdTabla', index: 'IdTabla' },
                { key: false, name: 'Tabla', index: 'Tabla' }
            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            height: '100%',
            viewrecords: true,
            subGrid: true,
            subGridRowExpanded: showChildGrid,
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
            //rownumbers: true,
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

    function showChildGrid(parentRowID, parentRowKey) {
        //debugger;
        var urlAccion = '';
        //if (parentRowKey == 10) {
        urlAccion = getURLGetDetalleMaestroLista() + '&IdTabla=' + parentRowKey;
        //}
        //if (parentRowKey == 20) {
        //    urlAccion = getURLGetUsuarioSucursal() + '&IdTabla=' + parentRowKey;
        //}
        //if (parentRowKey == 21) {
        //    urlAccion = getURLGetSociedadSucursal() + '&IdTabla=' + parentRowKey;
        //}

        fn_BloquearPantalla();
        $.ajax({
            url: urlAccion ,
            type: "GET",
            success: function (html) {
                $("#" + parentRowID).append('');
                $("#" + parentRowID).append(html.partial);
                fn_DesbloquearPantalla();
            }
        });
    }
    

    var formatoOpciones = function (cellvalue, options, rowObject) {
        return '<a href="javascript(0);" id="btn-new-maestro-tabla" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-file" title="Agregar"></a>' + '&nbsp;&nbsp;&nbsp;' 
    };

    var abrirUsuarioSucursal = function (id) {
        fn_BloquearPantalla();
        $('#modal-UsuarioSucursal .modal-content').empty();
        $('#modal-UsuarioSucursal .modal-content').load(url.RegistrarUsuarioSucursal, { Id: id }, function () {
            $("#modal-UsuarioSucursal").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });
    }
    var abrirSociedadSucursal = function (id) {
        fn_BloquearPantalla();
        $('#modal-SociedadSucursal .modal-content').empty();
        $('#modal-SociedadSucursal .modal-content').load(url.RegistrarSociedadSucursal, { Id: id }, function () {
            $("#modal-SociedadSucursal").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });
    }
    var abrirConfiguraciones = function (id) {
        fn_BloquearPantalla();
        $('#modal-Configuraciones .modal-content').empty();
        $('#modal-Configuraciones .modal-content').load(url.RegistrarConfiguraciones, { Id: id }, function () {
            $("#modal-Configuraciones").modal({ backdrop: 'static', keyboard: true, show: true });
            fn_DesbloquearPantalla();
        });
    }

})();
