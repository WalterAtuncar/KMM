var __defaultParsley = function () {
    return {
        successClass: 'has-success',
        errorClass: 'has-error',
        classHandler: function (ps) {
            var $el = $(ps.$element);
            return $el.closest('.input-group');
        },
        errorsWrapper: '<ul class="help-block list-unstyled" style="padding-left:3px;"></ul>'
    };
};

var __defaultParsleyForm = function () {
    return {
        successClass: 'has-success',
        errorClass: 'has-error',
        classHandler: function (ps) {
            var $el = $(ps.$element);
            if (typeof $el.attr('readonly') == 'undefined') {
                return $el.closest('.form-group, td');
            } else {
                return '';
            }
        },
        errorsContainer: function errorsContainer(ps) {
            var $el = $(ps.$element);
            var type = $el.attr('type');
            if (typeof type != 'undefined') {
                type = type.toLowerCase();
                if (type == 'checkbox' || type == 'radio') {
                    return $el.closest('[data-content-group]');
                }
                if (type == 'file') {
                    return $el.closest('[data-content-file]');
                }
            }
        },
        errorsWrapper: '<ul class="help-block list-unstyled" style="padding-left:3px;"></ul>'
    };
};
