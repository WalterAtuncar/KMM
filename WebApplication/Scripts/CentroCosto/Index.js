(function () {


    var validaciones = true;

    $(document).ready(function () {
        $('.filtro').attr('disabled', true);

        $('#chkCodigo').change(function () {checkCodigo(this)});
        $('#chkFechaDesde').change(function () { checkFechaDesde(this) });
        $('#chkTipo').change(function () { checkTipo(this) });

        $('#chkSoc').change(function () { checkSoc(this) });
        $('#chkFechaHasta').change(function () { checkFechaHasta(this)});

        paginado();

        $('#btnBuscarCentroCosto').click(function () {
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


    $('body').on('click', "#btn-edit-centro", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirModal(pid);
    });


    $('body').on('click', "#btn-save", function (e) {
        e.preventDefault();
        saveCentro();
    });

    var checkTipo = function(element){
        if (element.checked) {
            $('#txtTipo').prop('disabled', false);
        } else {
            $('#txtTipo').prop('disabled', true); 
            $('#txtTipo').val('');
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

    var checkCodigo = function (element) {
        if (element.checked) {
            $('#txtCodigo').prop('disabled', false);
            $('#txtCodigo').focus();
        } else {
            $('#txtCodigo').prop('disabled', true);
            $('#txtCodigo').val('');
        }
    }

    var checkSoc = function (element) {
        if (element.checked) {
            $('#txtSoc').prop('disabled', false);
            $('#txtSoc').focus();
        } else {
            $('#txtSoc').prop('disabled', true);
            $('#txtSoc').val('');
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
       
        var codigo = $('#txtCodigo').is(':disabled') ? '' : $('#txtCodigo').val();
        var fechaDesde = $('#fechaDesde').is(':disabled') ? '' : $('#fechaDesde').val();
        var soc = $('#txtSoc').is(':disabled') ? '' : $('#txtSoc').val();
        var fechaHasta = $('#fechaHasta').is(':disabled') ? '' : $('#fechaHasta').val();
        var tipo = $('#txtTipo').is(':disabled') ? '' : $('#txtTipo').val();
        return url.GetListCentroCosto + '?codigo=' + codigo + '&soc=' + soc + '&fechaDesde=' + fechaDesde + '&fechaHasta=' + fechaHasta + '&tipo=' + tipo;
    }

    var paginado = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListCentroCosto,
            datatype: 'JSON',
            //ajaxGridOptions: { contentType: "application/json; charset=utf-8" },
            mtype: 'GET',
            colNames: ['Opciones','Codigo', 'Tipo', 'Valido Desde', 'Valido Hasta', 'Sociedad','Descripcion'],
            colModel: [
                {
                    key: false,
                    name: 'Codigo',
                    index: 'Codigo',
                    align: 'center',
                    width: '95px',
                    fixed: true,
                    formatter: formatoOpciones,
                    formatoptions: { keys: true, }
                },
                { key: false, name: 'Codigo', index: 'Codigo', align: 'center'},
                { key: false, name: 'Tipo', index: 'Tipo', align: 'center'},
                { key: false, name: 'FechaInicioStr', index: 'FechaFinStr' },
                { key: false, name: 'FechaFinStr', index: 'FechaInicioStr' },
                { key: false, name: 'Soc', index: 'Soc' },
                { key: false, name: 'Descripcion', index: 'Descripcion' },
            ],
            pager: $('#jqControls'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
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
        return '<a href="javascript(0);" id="btn-edit-centro" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-pencil" title="Editar"></a>';
    }

    var getParameterFilter = function () {
        var parametro = [
            {
                ParamName: "Prueba1",
                ParamValue: "3"
            },
            {
                ParamName: "Prueba2",
                ParamValue: "4"
            }
        ];

        var Pagination = new Object();
        Pagination.Page = $('#jqGrid').getGridParam("page") == undefined ? 1 : $('#jqGrid').getGridParam("page") ;
        Pagination.Rows = $('#jqGrid').getGridParam("rowNum") == undefined ? 10 : $('#jqGrid').getGridParam("rowNum");
        Pagination.ListaParameterFiler = parametro;

        return Pagination;
       

    }

    var saveCentro = function () {
        var formulario = $('#frm-add-centro');
        validar();
        fn_BloquearPantalla();
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
                        $('#modal-centro').modal('hide');
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
    };

    var abrirModal = function (id) {
        fn_BloquearPantalla();
        $('#modal-centro .modal-content').empty();
        $('#modal-centro .modal-content').load(url.Registrar, { codigo: id }, function () {
            setFlag();
            fn_DesbloquearPantalla();
        });
        $('#modal-centro').modal('show');
    }

    $('body').on('change', '#ddlUsuario', function () {
        $("#select2-ddlUsuario-container").css('border', '0px solid red');
    });

    $('body').on('change', '.checkFlag', function () {
        changeFlag();
    });

    var changeFlag = function () {
        var chkGasto = $('#checkGasto').is(':checked');
        var chkCosto = $('#checkCosto').is(':checked');

        if (chkGasto) {
            $('#IdGasto').val(true);
            $('#IdCosto').val(false);
        } else {
            $('#IdCosto').val(true);
            $('#IdGasto').val(false);

        }
    }

    var validar = function () {
        validaciones = true;
        if ($('#ddlUsuario').val() === "") {
            $("#select2-ddlUsuario-container").css('border', '1px solid red');
            validaciones = false;
        }
    }

    var setFlag = function () {
        var flagCosto = $('#IdCosto').val() == 'True' ? true : false;
        var flagGasto = $('#IdGasto').val() == 'True' ? true : false;

        if (flagCosto) {
            $("#checkCosto").prop('checked', true);
            $('#IdCosto').val(true);
            $('#IdGasto').val(false);
        } else {
            $("#checkGasto").prop('checked', true);
            $('#IdGasto').val(true);
            $('#IdCosto').val(false);
        }
    }

})();