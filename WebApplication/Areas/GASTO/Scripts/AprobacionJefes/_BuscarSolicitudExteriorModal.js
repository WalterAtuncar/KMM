(function () {
    var _select = false;
    $(document).ready(function () {
        paginado();

        $('#BuscarButton').click(function () {
            fn_BloquearPantalla();
            //fetchGridData();
        });

        $('#AgregarSolicitudButton').click(function () {
            fn_BloquearPantalla();

            //var rowData = null;
            //var data = $("#tbl-solicitudes").jqGrid('getGridParam', 'data');
            //data.forEach(function (valueRow) {
            //    if (valueRow.Sel == true) {
            //        window.selectedSolicitud.push(valueRow);
            //    }
            //});

            //$("#tbl-solicitudes-registrar")[0].grid.beginReq();
            //$('#tbl-solicitudes-registrar').jqGrid('clearGridData');
            //$("#tbl-solicitudes-registrar").jqGrid('setGridParam', { data: window.selectedSolicitud });
            //$("#tbl-solicitudes-registrar")[0].grid.endReq();
            //$("#tbl-solicitudes-registrar").trigger('reloadGrid');

            //$('#SalirModalButton').click();
            //SumarContar();
            fn_DesbloquearPantalla();
        });

        //$('#chk_marcar_todos').on('change', function (e) {
        //    var state = e.target.checked;
        //    var rowData = null;
        //    var data = $("#tbl-solicitudes").jqGrid('getGridParam', 'data');
        //    data.forEach(function (valueRow) {
        //        valueRow.Sel = state;
        //        $('#tbl-solicitudes').jqGrid('setRowData', valueRow.ID, valueRow);
        //    });
        //    $("#tbl-solicitudes").trigger('reloadGrid');
        //});

        //
        //Permite marcar el checkbox
        //$('#tbl-solicitudes').on('change', '.checkrow', function (e) {
        //    var state = e.target.checked;
        //    var checked = this.checked;
        //    row = $(this).closest("tr");
        //    var valueRow = $('#tbl-solicitudes').jqGrid('getRowData', row.attr('id'));
        //    valueRow.Sel = state;
        //    $('#tbl-solicitudes').jqGrid('setRowData', valueRow.ID, valueRow);
        //});
    });

    function getURLGet() {
        var FechaInicioTxt = $('#FechaInicioTextBox').val();
        var FechaFinTxt = $('#FechaFinTextBox').val();
        return url.GetListModalSolicitudExteriorPaginado + '?fechaDesde=' + FechaInicioTxt + '&fechaHasta=' + FechaFinTxt;
    }

   
    //function paginado() {
    //    $("#tbl-solicitudes").jqGrid({
    //        datatype: 'local',
    //        colNames: ['ID', 'Sel', 'FechaServicio', 'Codigo Solicitud', 'Situacion Servicio', 'Hora', 'Centro Costo', 'Beneficiado', 'Documento', 'O/S', 'Centro Costo', 'Aprobador', 'Total', 'Tarifa', 'T.Espera', 'T.DesvRuta', 'T.Peaje', 'T.Estacionamiento', 'T.Desplazamiento', 'Hora Inicio', 'Negociado'],
    //        colModel: [
    //            { key: true, name: 'ID', index: 'ID', align: 'left', width: '100', hidden: true },
    //            {
    //                key: false, name: 'Sel', index: 'Sel', align: 'center', width: '40px',
    //                formatter: function (cellvalue, options, rowObject) {
    //                    return "<input type='checkbox' class='checkrow' id='chk_" + rowObject.ID + "' name='chk_" + rowObject.ID + "' " + (rowObject.Sel == true ? "checked": "") + " />";
    //                }
    //            },
    //            { key: false, name: 'FechaServicioStr', index: 'FechaServicioStr', align: 'center', width: '100px' },
    //            { key: false, name: 'CodigoSolicitud', index: 'CodigoSolicitud', align: 'center', width: '120px' },
    //            { key: false, name: 'SituacionServicioNombre', index: 'SituacionServicio', align: 'center', width: '120px' },
    //            { key: false, name: 'HoraServicio', index: 'HoraServicio', align: 'center', width: '80px' },
    //            { key: false, name: 'CentroCostoBeneficiado', index: 'CentroCostoBeneficiado', align: 'center', width: '83px' },
    //            { key: false, name: 'Beneficiado', index: 'Beneficiado', align: 'left', width: '285px' },
    //            { key: false, name: 'Documento', index: 'Documento', align: 'center', width: '102px' },
    //            { key: false, name: 'OrdenServicio', index: 'OrdenServicio', align: 'center', width: '71px' },
    //            { key: false, name: 'CentroCostoAfecto', index: 'CentroCostoAfecto', align: 'center', width: '71px' },
    //            { key: false, name: 'Aprobador', index: 'Aprobador', align: 'center', width: '278px' },
    //            { key: false, name: 'TotalGeneralStr', index: 'TotalGeneralStr', align: 'center', width: '98px' },
    //            { key: false, name: 'PrecioTarifaStr', index: 'PrecioTarifaStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'PrecioEsperaStr', index: 'PrecioEsperaStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'PrecioDesvioRutaStr', index: 'PrecioDesvioRutaStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'PrecioPeajeStr', index: 'PrecioPeajeStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'PrecioEstacionamientoStr', index: 'PrecioEstacionamientoStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'PrecioDesplazamientoStr', index: 'PrecioDesplazamientoStr', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'HoraInicio', index: 'HoraInicio', align: 'left', width: '100', hidden: true },
    //            { key: false, name: 'IndicadorNegociado', index: 'IndicadorNegociado', align: 'left', width: '100', hidden: true },
    //        ],
    //        rowNum: 10,
    //        rowList: [10, 20, 30, 40, 50],
    //        height: '230px',
    //        pager: '#jqControls2',
    //        sortname: 'id',
    //        viewrecords: true,
    //        sortorder: "desc",
    //        caption: "",
    //        rownumbers: true,
    //        cmTemplate: { sortable: false }
    //    });
    //    fetchGridData();
    //}


    function paginado() {
        $("#tbl-solicitudes").jqGrid({
            
            url: getURLGet(),
            datatype: 'JSON',
            mtype: 'GET',
            colNames: ['Sociedad', 'CECO', 'DNI', 'Nombre', 'Cod', 'Fec.Des', 'Mon', 'Importe', 'Fec.Registro', 'Motivo', 'Estado'],
            colModel: [
                { key: false, name: 'NombreCO', index: 'NombreCO', width: "70", align: 'center' },
                { key: false, name: 'CECO', index: 'CECO' },
                { key: false, name: 'DNI', index: 'DNI', width: "64", align: 'center' },
                { key: false, name: 'Beneficiario', index: 'Beneficiario', width: "100" },
                { key: true, name: 'Codigo', index: 'Codigo', width: "50", align: 'center', Key: true },
                { key: false, name: 'FechaDesde', index: 'FechaDesde', width: "65" },
                { key: false, name: 'Moneda', index: 'Moneda', width: "25", align: 'center' },
                { key: false, name: 'ImporteSolicitudStr', index: 'ImporteSolicitudStr', width: "60", align: 'right' },
                { key: false, name: 'FechaRegistroStr', index: 'FechaRegistroStr', width: "65" },
                { key: false, name: 'Motivo', index: 'Motivo', width: "60" },
                { key: false, name: 'Estado', index: 'Estado', width: "70" },

            ],
            pager: $('#jqControls2'),
            rowNum: 10,
            rowList: [10, 20, 30, 40, 50],
            height: '230px',
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
            },
            autowidth: true,
            multiselect: true,
            rownumbers: true,
            sortorder: "desc",
            cmTemplate: { sortable: false },
            //loadBeforeSend: function () {
            //    fn_BloquearPantalla();
            //},
            //gridComplete: function () {
            //},
            //loadComplete: function (data) {
            //    fn_DesbloquearPantalla();
            //},
        });
    }


    
})();