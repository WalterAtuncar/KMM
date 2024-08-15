(function () {

    $(document).ready(function () {
        paginate();
    });

    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };

    $('#btn-create').click(function (e) {
        e.preventDefault();
        abrirModal();
    });

    $('body').on('click', "#btn-save", function (e) {
        e.preventDefault();
        savePerfil();
    });

    $('body').on('click', "#btn-edit-perfil", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        abrirModal(pid);
    });

    $('body').on('click', "#btn-asignar-pagina", function (e) {
        e.preventDefault();
        var pid = $(this).attr('data-pid');
        modalPagina(pid);
    });

    $('body').on('click', "#btn-add-pagina", function (e) {
        e.preventDefault();
        var pid = $('#ddlPagina').val();
        addPaginaPerfil(pid);
    });

    $('body').on('click', "#btn-delete-pagina", function (e) {
        e.preventDefault();
        var selector = $(this);
        deletePaginaPerfil(selector);
    });

    var savePerfil = function () {
        var $frm = $('#frm-add-perfil');
        var formParsley = $frm.parsley(__defaultParsleyForm());
        var isValid = formParsley.validate();
        if (isValid) {
            $.ajax({
                url: url.RegistrarPost,
                data: $frm.serialize(),
                type: 'POST',
                async: true,
                cache: false,
                beforeSend: function () {
                    fn_BloquearPantalla();
                },
                success: function (data) {
                    if (data.Success) {
                        var validar = data.ObjectResult;
                        if(validar){
                            toastr["warning"]('Ya existe el nombre de perfil ingresado');
                        } else{
                            toastr["success"]('Registro Correcto');
                            $('#modal-perfil').modal('hide');
                            $('#jqGrid').jqGrid('clearGridData');
                            $('#jqGrid').jqGrid('setGridParam', {
                                page: 1
                            }).trigger('reloadGrid');
                        }
                        fn_DesbloquearPantalla();
                    } else {
                        //console.log(data.Error),
                        toastr["error"]('Ocurrio un error inesperado');
                        fn_DesbloquearPantalla();
                    }
                },
                complete: function () {
                    fn_DesbloquearPantalla();
                }
            });
        } else {
            toastr["error"]('Complete los campos obligatorios');
            fn_DesbloquearPantalla();
        }
    };

    var paginate = function () {
        $("#jqGrid").jqGrid({
            url: url.GetListPerfil,
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Opciones', 'Nombre', 'Descripción', 'Estado'],
            colModel: [
                {
                    key: false,
                    name: 'ID',
                    index: 'Options',
                    align: 'center',
                    fixed: true,
                    width: '95px',
                    formatter: formatoOpciones,
                    formatoptions: { keys: true }
                },
                { key: false, name: 'Nombre', index: 'Nombre' /*formatter: __jqGrid_formatter_sin_alterta_estado*/ },
                { key: false, name: 'Descripcion', index: 'Descripcion' },
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
                fn_DesbloquearPantalla();
                //$('[data-toggle="tooltip"]').tooltip();
            }
        });
    };

    var formatoOpciones = function (cellvalue, options, rowObject) {
        return '<a href="javascript(0);" id="btn-edit-perfil" data-pid ="' + cellvalue + '"  class="glyphicon glyphicon-pencil" title="Editar"></a>' + '&nbsp;&nbsp;&nbsp;' +
            '<a href="javascript(0);" id="btn-asignar-pagina" data-pid ="' + cellvalue + '"  class="fa fa-check-square-o" title = "Asignar Páginas"></a>';
    };

    //var validarPerfil = function ($frm){
    //    $.ajax({
    //        url: url.ValidarPost,
    //        data: $frm.serialize(),
    //        type: 'POST',
    //        async: false,
    //        cache: false,
    //        success: function (data) {
    //            if (data.Success) {
    //                validar = data.ObjectResult;
    //            } else {
    //                console.log(data.Error),
    //                toastr["error"]('Ocurrio un error inesperado');
    //                fn_DesbloquearPantalla();
    //            }
    //        }
    //    });
    //}

    var deletePaginaPerfil = function (selector) {
        var idPerfil = $('#IdPerfilSeleccionado').val();
        var idPagina = selector.attr('data-pid');
        fn_BloquearPantalla();
        $.ajax({
            url: url.DeletePaginaPerfil,
            data: { idPagina: idPagina, idPerfil: idPerfil },
            type: 'POST',
            async: false,
            cache: false,
            success: function (data) {
                if (data.Success) {
                    $('#modal-perfil .modal-content').load(url.GetListPagina, { id: idPerfil }, function () {
                        $('#IdPerfilSeleccionado').val(idPerfil);
                        toastr["success"]('Se elimino el registro');
                        fn_DesbloquearPantalla();
                    });
                } else {
                    //console.log(data.Error),
                        toastr["error"]('Ocurrio un error inesperado');
                    fn_DesbloquearPantalla();
                }
            }
        });
    };

    var addPaginaPerfil = function (pid) {
        if(pid == null){
            toastr["info"]('Debe seleccionar una página');
        } else{
            var idPerfil = $('#IdPerfilSeleccionado').val();
            fn_BloquearPantalla();
            $.ajax({
                url: url.AgregarPaginaPerfil,
                data: { idPagina: pid, idPerfil: idPerfil },
                type: 'POST',
                async: false,
                dataType:"JSON",
                success: function (data) {
                    if (data.Success) {
                        $('#modal-perfil .modal-content').load(url.GetListPagina, { id: idPerfil }, function () {
                            $('#IdPerfilSeleccionado').val(idPerfil);
                            toastr["success"]('Registro Correcto');
                            fn_DesbloquearPantalla();
                        });
                    } else {
                        //console.log(data.Error),
                            toastr["error"]('Ocurrio un error inesperado');
                        fn_DesbloquearPantalla();
                    }
                },
                error: function (data) {
                    //console.log(data);
                }
            });
        }
    };

    var modalPagina = function (id) {
        fn_BloquearPantalla();
        $('#modal-perfil .modal-content').load(url.GetListPagina, { id: id }, function () {
            $('#IdPerfilSeleccionado').val(id);
            fn_DesbloquearPantalla();
        });
        $('#modal-perfil').modal('show');
    };

    var abrirModal = function (id) {
        fn_BloquearPantalla();
        $('#modal-perfil .modal-content').empty();
        $('#modal-perfil .modal-content').load(url.Registrar, { id: id }, function () {
            fn_DesbloquearPantalla();
        });

        $('#modal-perfil').modal('show');
    };

})();