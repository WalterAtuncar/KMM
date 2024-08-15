$(document).ready(function () {
    var listaUsuario = "";
    var listaCentroCosto = "";

    $('body').on('click', '#btnBuscarReporteDetallado', function (e) {
        e.preventDefault();
        if ($("#BeneficiadoDropDown").val() != null) {
            listaUsuario = $("#BeneficiadoDropDown").val().join(',')
        }
        if ($("#ddlCecoMultiSelect").val() != null) {
            listaCentroCosto = $("#CentroCostoDropDown").val().join(',');
        }
        if (listaUsuario == "" && listaCentroCosto == "") {
            toastr["warning"]("Debe seleccionar datos");
        } else{
            cargarTabla(listaUsuario, listaCentroCosto);
        }
        
    });

    $('body').on('change.select2', '#BeneficiadoDropDown', function (e) {
        if ($("#BeneficiadoDropDown").val() != null) {
            listaUsuario = $("#BeneficiadoDropDown").val().join(',');
            $("#exportarExcel").attr("listaUsuario", listaUsuario);
            $("#exportarExcel").attr("href", "/Reporte/ExportarExcel?formato=EXCEL&listaUsuario=" + listaUsuario + "&listaCentroCosto=" + listaCentroCosto);
        } else{
            listaUsuario = "";
        }   
    });

    $('body').on('change.select2', '#CentroCostoDropDown', function (e) {
        if ($("#CentroCostoDropDown").val() != null) {
            listaCentroCosto = $("#CentroCostoDropDown").val().join(',');
            $("#exportarExcel").attr("listaCentroCosto", listaCentroCosto);
            $("#exportarExcel").attr("href", "/Reporte/ExportarExcel?formato=EXCEL&listaUsuario=" + listaUsuario + "&listaCentroCosto=" + listaCentroCosto);
        } else{
            listaCentroCosto = "";
        }
    });

    $('#SociedadDropDown').change(function () {
        fn_BloquearPantalla();
        var $id = $('#SociedadDropDown').val();
        $.post(url.GetDropDown, { sociedad: $id })
        .done(function (data) {
            $('#InputListBeneficiado').val(data.ListaBeneficiado);
            var listaBeneficiado = $('#InputListBeneficiado').val();
            $('#BeneficiadoDropDown').html(listaBeneficiado).trigger('change');
            $('#InputListCentroCosto').val(data.ListaCentroCosto);
            var listaCentroCosto = $('#InputListCentroCosto').val();
            $('#CentroCostoDropDown').html(listaCentroCosto).trigger('change');
        }).fail(function () {
            fn_DesbloquearPantalla();
        }).always(function () {
            fn_DesbloquearPantalla();
        });
    });
});

var cargarTabla = function (listaUsuario, listaCentroCosto) {
    fn_BloquearPantalla();
    $('#div-tbl-solicitudes').load(url.GetReporteDetallado, { listaUsuario: listaUsuario, listaCentroCosto: listaCentroCosto }, function (e) {
        fn_DesbloquearPantalla();
    });
}
