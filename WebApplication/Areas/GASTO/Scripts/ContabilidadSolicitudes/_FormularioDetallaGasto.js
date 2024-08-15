(function () {

      $(document).ready(function () {

        var $input = $("#archivo-adjuntoDG");
    
        $input.fileinput({
            language: "es",
            maxFileCount: 1,
            showPreview: false,
            maxFileSize: '91324',
            msgSizeTooLarge: 'Tamaño máximo de archivo: 90 MB',
            uploadUrl: '/GASTO/ContabilidadSolicitudes/UploadFileDG',
            uploadExtraData: { IdDetRendicionSolicitud: $('#hdfIdDetRendicionSolicitud').val()},
            elErrorContainer: "#errorBlock-productoDG",
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
                $("#content-attachmentDG").html(data.response.partial).promise().done(function () {
                    $(".remove-item-adjuntoDG").click(removeFile);
                });
            }
            else if (data.response.code == 3) {
                toastr["error"]("El archivo ya existe por favor ingrese uno nuevo");
            }
            else
            {
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
        $.ajax('/GASTO/ContabilidadSolicitudes/RemoveFileDG', {
            cache: false,
            type: "GET",
            beforeSend: function () {
                fn_BloquearPantalla();
            },
            success: function (form) {
                $("#content-attachmentDG").html(form).promise().done(function () {
                    $(".remove-item-adjuntoDG").click(removeFile);
                });
            },
            complete: function () {
                fn_DesbloquearPantalla();
            }
        })
    }

    $('.remove-item-adjuntoDG').click(function () {
        removeFile();
    });

})();