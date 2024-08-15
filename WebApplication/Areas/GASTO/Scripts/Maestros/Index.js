(function () {
    $(document).ready(function () {
        paginado();
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
        if (IdTabla == 22) {
            abrirUsuarioAprobador(0);
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

    $('body').on('click', '#btn-edit-tabla22', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-22');
        abrirUsuarioAprobador(id);
    });

    $('body').on('click', '#btn-del-tabla22', function (e) {
        e.preventDefault();
        var id = $(this).attr('data-pid-22');
        EliminarUsuarioAprobador(id);
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
        return url.GetListMaestros;
    }

    function getURLGetDetalleMaestroLista() {
        return url.GetDetalleMaestroLista;
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
                    width: '60px',
                    formatter: formatoOpciones,
                    formatoptions: { keys: true }
                },
                { key: false, name: 'ID', index: 'ID', align: 'center', width: "20"  },
                { key: true, name: 'IdTabla', index: 'IdTabla', width: "20"  },
                { key: false, name: 'Tabla', index: 'Tabla', width: "200"  }
            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            height: '100%',
            viewrecords: true,
            subGrid: true,
            subGridRowExpanded: showChildGrid,
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false
            },
            autowidth: true,
            multiselect: false,
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
            }
        });
    };

    function showChildGrid(parentRowID, parentRowKey) {
        var childGridID = parentRowID + "_table";
        var childGridPagerID = parentRowID + "_pager";
        var childGridURL = getURLGetDetalleMaestroLista() + '?IdTabla=' + parentRowKey;
        $('#' + parentRowID).append('<table id=' + childGridID + '></table><div id=' + childGridPagerID + '></div>');
        if (parentRowKey == 10) {
            $("#" + childGridID).jqGrid({
                url: childGridURL,
                mtype: "GET",
                datatype: "json",
                colNames: ['Opciones', 'Sociedad', 'Cod.Local', 'Sucursal', 'Administrador'],
                colModel: [
                    {
                        key: false,
                        name: 'ID',
                        index: 'ID',
                        align: 'center',
                        fixed: true,
                        width: '70',
                        formatter: formatoOpciones10,
                        formatoptions: { keys: true }
                    },
                    { key: false, name: 'Sociedad', index: 'Sociedad', align: 'center', width: "160" },
                    { key: true, name: 'CodigoLocal', index: 'CodigoLocal', width: "70" },
                    { key: false, name: 'Sucursal', index: 'Sucursal', width: "160" },
                    { key: false, name: 'Administrador', index: 'Administrador', width: "350" }
                ],
                //loadonce: true,
                //width: 500,
                height: '100%',
                pager: $('#' + childGridPagerID),
                rowNum: 10,
                rowList: [10, 20, 30, 40, 50],
                loadBeforeSend: function () {
                    fn_BloquearPantalla();
                },
                loadComplete: function (data) {
                    fn_DesbloquearPantalla();
                }
            });
        }
        if (parentRowKey == 20) {
            $("#" + childGridID).jqGrid({
                url: childGridURL,
                mtype: "GET",
                datatype: "json",
                colNames: ['Opciones', 'Clave', 'Valor1', 'Valor2'],
                colModel: [
                    {
                        key: false,
                        name: 'Id',
                        index: 'Id',
                        align: 'center',
                        fixed: true,
                        width: '70',
                        formatter: formatoOpciones20,
                        formatoptions: { keys: true }
                    },
                    { key: false, name: 'Clave', index: 'Clave', align: 'center', width: "250" },
                    { key: false, name: 'Valor1', index: 'Valor1', align: 'right', width: "120" },
                    { key: false, name: 'Valor2', index: 'Valor2', align: 'right', width: "120" },
                    
                ],
                //loadonce: true,
                //width: 500,
                height: '100%',
                pager: $('#' + childGridPagerID),
                rowNum: 10,
                rowList: [10, 20, 30, 40, 50],
                loadBeforeSend: function () {
                    fn_BloquearPantalla();
                },
                loadComplete: function (data) {
                    fn_DesbloquearPantalla();
                }
            });
        }
        if (parentRowKey == 21) {
            $("#" + childGridID).jqGrid({
                url: childGridURL,
                mtype: "GET",
                datatype: "json",
                colNames: ['Opciones', 'Sociedad', 'Cod.Local', 'Sucursal'],
                colModel: [
                    {
                        key: false,
                        name: 'ID',
                        index: 'ID',
                        align: 'center',
                        fixed: true,
                        width: '70',
                        formatter: formatoOpciones21,
                        formatoptions: { keys: true }
                    },
                    { key: false, name: 'Sociedad', index: 'Sociedad', align: 'center', width: "160" },
                    { key: false, name: 'CodigoLocal', index: 'CodigoLocal', width: "70" },
                    { key: false, name: 'Sucursal', index: 'Sucursal', width: "200" }
                   
                ],
                //loadonce: true,
                //width: 500,
                height: '100%',
                pager: $('#' + childGridPagerID),
                rowNum: 10,
                rowList: [10, 20, 30, 40, 50],
                loadBeforeSend: function () {
                    fn_BloquearPantalla();
                },
                loadComplete: function (data) {
                    fn_DesbloquearPantalla();
                }
            });
        }
        if (parentRowKey == 22) {
            $("#" + childGridID).jqGrid({
                url: childGridURL,
                mtype: "GET",
                datatype: "json",
                colNames: ['Opciones', 'Sociedad', 'Aprobador', 'Centros Costos'],
                colModel: [
                    {
                        key: false,
                        name: 'ID',
                        index: 'ID',
                        align: 'center',
                        fixed: true,
                        width: '70',
                        formatter: formatoOpciones22,
                        formatoptions: { keys: true }
                    },
                    { key: false, name: 'Sociedad', index: 'Sociedad', align: 'center', width: "160" },
                    { key: true, name: 'Administrador', index: 'Administrador', width: "250" },
                    { key: false, name: 'CentrosCostos', index: 'CentrosCostos', width: "350" }
                ],
                //loadonce: true,
                //width: 500,
                height: '100%',
                pager: $('#' + childGridPagerID),
                rowNum: 10,
                rowList: [10, 20, 30, 40, 50],
                loadBeforeSend: function () {
                    fn_BloquearPantalla();
                },
                loadComplete: function (data) {
                    fn_DesbloquearPantalla();
                }
            });
        }
    }

    var formatoOpciones10 = function (cellvalue, options, rowObject) {
        var result = '';
        result += '<a href="javascript(0);" id="btn-edit-tabla10" data-pid-10="' + cellvalue + '" class="glyphicon glyphicon-pencil" title="Editar"></a>&nbsp;&nbsp;&nbsp;';
        result += '<a href="javascript(0);" id="btn-del-tabla10" data-pid-10="' + cellvalue + '" class="glyphicon glyphicon-trash" title="Eliminar"></a>';
        return result;
    };
    var formatoOpciones20 = function (cellvalue, options, rowObject) {
        var result = '';
        result += '<a href="javascript(0);" id="btn-edit-tabla20" data-pid-20="' + cellvalue + '" class="glyphicon glyphicon-pencil" title="Editar"></a>&nbsp;&nbsp;&nbsp;';
        result += '<a href="javascript(0);" id="btn-del-tabla20" data-pid-20="' + cellvalue + '" class="glyphicon glyphicon-trash" title="Eliminar"></a>';
        return result;
    };

    var formatoOpciones21 = function (cellvalue, options, rowObject) {
        var result = '';
        result += '<a href="javascript(0);" id="btn-edit-tabla21" data-pid-21="' + cellvalue + '" class="glyphicon glyphicon-pencil" title="Editar"></a>&nbsp;&nbsp;&nbsp;';
        result += '<a href="javascript(0);" id="btn-del-tabla21" data-pid-21="' + cellvalue + '" class="glyphicon glyphicon-trash" title="Eliminar"></a>';
        return result;
    };
    var formatoOpciones22 = function (cellvalue, options, rowObject) {
        var result = '';
        result += '<a href="javascript(0);" id="btn-edit-tabla22" data-pid-22="' + cellvalue + '" class="glyphicon glyphicon-pencil" title="Editar"></a>&nbsp;&nbsp;&nbsp;';
        result += '<a href="javascript(0);" id="btn-del-tabla22" data-pid-22="' + cellvalue + '" class="glyphicon glyphicon-trash" title="Eliminar"></a>';
        return result;
    };

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
    var abrirUsuarioAprobador = function (id) {
        fn_BloquearPantalla();
        $('#modal-UsuarioAprobador .modal-content').empty();
        $('#modal-UsuarioAprobador .modal-content').load(url.RegistrarUsuarioAprobador, { Id: id }, function () {
            $("#modal-UsuarioAprobador").modal({ backdrop: 'static', keyboard: true, show: true });
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
