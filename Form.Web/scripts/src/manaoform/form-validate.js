var context = {};

//To replace plachoder function in main.js
//if ($('html').is('.ie6, .ie7, .ie8, .ie9')) {
//    //setPlaceHolder(); //function placeholder here
//}

$(document).ready(function () {
    $('.submitForm').each(function () {
        $(this).click(function () {
            context.Valid = true;
            var form = $(this).parents('form');
            Validate(form);
            return context.Valid;
        });
    });
});

function Validate(form) {
    $(form).find('input:not(.drop-box)[data-require="required"], textarea[data-require="required"], select[data-require="required"]').each(function () {
        ValidateRequireField(this);
    });
    $(form).find('input[type="email"]').each(function () {
        ValidateEmailField(this);
    });
    $(form).find('input[type="tel"]').each(function () {
        ValidateTelephoneField(this);
    });
    $(form).find('.checkbox-group div[data-require="required"],.radio-group div[data-require="required"]').each(function () {
        ValidateRadioCheckboxGroup(this);
    });
    $(form).find('input:file:not(.drop-box)').each(function () {
        ValidateUploadField(this);
    });
    $(form).find('input:file.drop-box').each(function () {
        var file = $(this);
        var formGroup = file.parents('.form-group');
        var id = file.attr('id');
        var required = file.attr('data-require');
        var $scope = file.scope();
        var files = $scope.dragdropfiles[id];
        validateDragAndDropField(formGroup, files, file);
    });

    if (!context.Valid) {
        ScrollTop(form);
    }
}

$('input:not(.drop-box)[data-require="required"], textarea[data-require="required"]').bind('change', function () {
    ValidateRequireField(this);
});

$('.checkbox-group div[data-require="required"],.radio-group div[data-require="required"]').bind('change', function () {
    ValidateRadioCheckboxGroup(this);
});

$('input:file:not(.drop-box)').bind('change', function () {
    ValidateRequireField(this);
    ValidateUploadField(this);
});

function validateDragAndDropField(formGroup, files, file) {
    if (file.attr('data-require') == 'required') {

        if (!files) {
            context.Valid = false;
            DisplayRequireMessage(formGroup);
        } else if (files.length == 0) {
            context.Valid = false;
            DisplayRequireMessage(formGroup);
        } else {
            RemoveRequireMessage(formGroup);
        }

        if (files && files.length > 0 && context.Valid) {
            var totalSize = 0;
            $.each(files, function (key, file) {
                totalSize += (file.size / 1024);
            });

            if (parseInt(file.attr('data-maxfilesize')) < totalSize) {
                context.Valid = false;
                DisplayErrorMessage(formGroup, "error-overMaxSize");
            } else {
                RemoveErrorMessage(formGroup, "error-overMaxSize");
            }
        }
    }
}

function ValidateUploadField(inputField) {
    if (inputField.files[0] === undefined) return;

    var field = $(inputField).parents('.form-group');
    var fileSize = inputField.files[0].size || 0;
    if (fileSize !== undefined) fileSize = fileSize / 1024;

    if (inputField.files.length > 0
        && inputField.getAttribute('data-maxfilesize') != 0
        && fileSize > inputField.getAttribute('data-maxfilesize')) {
        context.Valid = false;
        DisplayErrorMessage(field, "error-overMaxSize");
    }
    else {
        RemoveErrorMessage(field, "error-overMaxSize");
    }
}

function ValidateRequireField(inputField) {
    var field = $(inputField).parents('.form-group');
    if (!$(inputField).val()) {
        context.Valid = false;
        DisplayRequireMessage(field);
    }
    else {
        if ($(inputField).attr('type')) {
            if ($(inputField).attr('type').indexOf('checkbox') > -1) {
                if (!inputField.checked) {
                    context.Valid = false;
                    DisplayRequireMessage(field);
                }
                else {
                    RemoveRequireMessage(field);
                }
            }
            else {
                RemoveRequireMessage(field);
            }
        }
        else {
            RemoveRequireMessage(field);
        }
    }
}

function ValidateEmailField(inputField) {
    var field = $(inputField).parents('.form-group');
    if ($(inputField).val()) {
        var reg = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9.]{1,10}\s*$/;
        if (reg.test($(inputField).val())) {
            RemoveInvalidMessage(field);
        }
        else {
            context.Valid = false;
            DisplayInvalidMessage(field);
        }
    }
}

function ValidateTelephoneField(inputField) {
    var field = $(inputField).parents('.form-group');
    if ($(inputField).val()) {
        var reg = /^.*(?:\d.*){1,}$/;
        if (reg.test($(inputField).val())) {
            RemoveInvalidMessage(field);
        }
        else {
            context.Valid = false;
            DisplayInvalidMessage(field);
        }
    }
}

function ValidateRadioCheckboxGroup(inputField) {
    var checkedCount = $(inputField).find('input:checked').length;
    var field = $(inputField).parents('.form-group');
    if (checkedCount > 0) {
        RemoveRequireMessage(field);
    }
    else {
        context.Valid = false;
        DisplayRequireMessage(field);
    }
}

function DisplayRequireMessage(field) {
    $(field).addClass('error-require');
    $(field).removeClass('error-invalid');
}
function RemoveRequireMessage(field) {
    $(field).removeClass('error-require');
}
function DisplayInvalidMessage(field) {
    $(field).addClass('error-invalid');
}
function RemoveInvalidMessage(field) {
    $(field).removeClass('error-invalid');
}
function DisplayErrorMessage(field, errorClass) {
    $(field).addClass(errorClass);
}
function RemoveErrorMessage(field, errorClass) {
    $(field).removeClass(errorClass);
}

function RemoveAllMessages(field) {
    RemoveRequireMessage(field);
    RemoveInvalidMessage(field);
}

function ScrollTop(form) {
    var errorField;
    var scroll;

    if ($(form).find('.form-group.error-require,.form-group.error-invalid').length > 1) {
        errorField = $(form).find('.form-group.error-require,.form-group.error-invalid')[0];

    } else {
        errorField = $(form).find('.form-group.error-require,.form-group.error-invalid');
    }


    $('html, body').animate({ //scroll to submit result
        scrollTop: $(errorField).offset().top - 250
    }, 500);

    $(errorField).find('input,textarea').focus();
}

$(function () {
    //setFormDropdownList();
});