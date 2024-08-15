$(document).ready(function () {

    $('body').on('click', '#btnBuscarReporteDetallado', function (e) {
        e.preventDefault();
        var codigo = $("#txtCodigoFiltro").val();
        //if ($("#BeneficiadoDropDown").val() != null) {
        //    listaUsuario = $("#BeneficiadoDropDown").val().join(',')
        //}
        //if ($("#ddlCecoMultiSelect").val() != null) {
        //    listaCentroCosto = $("#CentroCostoDropDown").val().join(',');
        //}
        if (codigo) {
            toastr["warning"]("Debe seleccionar datos");
        } else {
            cargarTabla(codigo);
        }

    });

    var cargarTabla = function (codigo) {
        fn_BloquearPantalla();
        $('#div-tbl-solicitudes').load(
            url.GetReporteAuditoria,
            { codigo: codigo },
            function (e) {
                fn_DesbloquearPantalla();
            }
        )
    }

});