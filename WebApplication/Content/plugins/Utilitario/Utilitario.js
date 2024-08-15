function fn_BloquearPantalla() {
    var strMensaje = "Cargando";
    vRutaServidor = "/";
    $.blockUI({
        message: '<table align="center"><tr><td style="padding-top:15px;"><img src="' + vRutaServidor + 'Content/img/gif-loading.gif" width="50" /></td></tr><tr><td style="padding:11px;font-size:13px;">' + strMensaje + '</td></tr></table>',
        centerY: 0,
        css: {
            width: '150px',
            top: ($(window).height() - 150) / 2 + 'px',
            left: ($(window).width() - 150) / 2 + 'px',
            border: '0px solid #ff8304',
            padding: '3px',
            backgroundColor: '#333333',
            opacity: .99,
            color: '#FFFFFF'
        },
        fadeIn: 200,
        fadeOut: 400
    });
}

function fn_DesbloquearPantalla() {
    $.unblockUI();
}