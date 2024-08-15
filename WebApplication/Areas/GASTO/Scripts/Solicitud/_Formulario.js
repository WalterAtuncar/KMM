(function () {


      $(document).ready(function () {
        
        var $input = $("#archivo-adjunto");

          $input.fileinput({
            
            language: "es",
            maxFileCount: 1,
            showPreview: false,
            maxFileSize: '91324',
            msgSizeTooLarge: 'Tamaño máximo de archivo: 90 MB',
            uploadUrl: '/GASTO/Solicitud/UploadFile',
            uploadExtraData: { IdSolicitud : $('#hdfIdSolicitud_Formulario').val()},
            elErrorContainer: "#errorBlock-producto",
            showCancel: false,
            showUpload: false,
            showRemove: false,
            showCaption: false,
            browseClass: "btn btn-primary btn-sm",
            removeIcon: '<i class="glyphicon glyphicon-trash"></i>',
            uploadAsync: false,
              allowedFileExtensions: ["jpg", "png", "pdf"]
            //allowedFileExtensions: ["pdf"]
        }).on('change', function (event) {
            var file = event.target.files[0];
            var valido = false;
            if (file.type.match('application/pdf') || file.type.match('image/jpeg') || file.type.match('image/png')) {
                valido = true;
            }
            if (!valido) {
                removeFile();
                return;
            }
        }).on('filebatchselected', function (event, files) {
            $input.fileinput('upload');
        }).on('filebatchuploadsuccess', function (event, data) {
            if (data.response.code == 1) {
                $("#content-attachment").html(data.response.partial).promise().done(function () {
                    $(".remove-item-adjunto").click(removeFile);
                });
            }
            else {
                //notifyError('Ocurrio un error inesperado');
                var file = event.target.files[0];
                var valido = false;
                if (file.type.match('application/pdf') || file.type.match('image/jpeg') || file.type.match('image/png')) {
                    valido = true;
                }
                if (!valido) {
                    removeFile();
                    return;
                }
                toastr["error"]('Ocurrio un error inesperado');
            }
        });
    });


    var removeFile = function () {
        $.ajax('/GASTO/Solicitud/RemoveFile', {
            cache: false,
            type: "GET",
            beforeSend: function () {
                fn_BloquearPantalla();
            },
            success: function (form) {
                $("#content-attachment").html(form).promise().done(function () {
                    $(".remove-item-adjunto").click(removeFile);
                });
            },
            complete: function () {
                fn_DesbloquearPantalla();
            }
        })
    }

    $('.remove-item-adjunto').click(function () {
        removeFile();
    });

})();