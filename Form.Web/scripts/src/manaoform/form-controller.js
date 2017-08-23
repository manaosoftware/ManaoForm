app.controller('ManaoFormSubmitController', ['$scope', '$timeout', 'Upload',
    function ($scope, $timeout, Upload) {
        $scope.dragdropfiles = [];
        $scope.submitResult = {};
        $scope.formId = 0;

        $scope.submit = function (formId) {
            if (!context.Valid) return;

            $scope.formId = formId;
            var form = $('#' + $scope.formId);
            var fields = form.serializeArray();

            var data = {};
            $.each(form.serializeArray(), function () {
                data[this.name] = this.value
                    .replace(/&/g, '&amp;')
                    .replace(/"/g, '&quot;')
                    .replace(/'/g, '&#39;')
                    .replace(/</g, '&lt;')
                    .replace(/>/g, '&gt;');
            });

            disableButton(true);
            postData(JSON.stringify(fields));
        }

        function postData(fieldData) {
            Upload.upload({
                url: '/ManaoFormApi/AjaxSubmit',
                data: {
                    data: fieldData,
                    file: $scope.dragdropfiles
                }
            }).then(function (resp) {
                $timeout(function () {
                    $scope.submitResult = resp.data;
                    if ($scope.submitResult.Status == 'success') {
                        afterSuccess();
                    } else if ($scope.submitResult.Status == 'fail') {
                        formValidation();
                    } else {
                        formReset();
                    }
                    disableButton(false);
                }, 200);

            }, function (resp) {
                // handle error
                formValidation();

            }, function (evt) {
                // progress notify
            });
        }

        function disableButton(value) {
            if (value) {
                var form = $('#' + $scope.formId);
                $(form).find('input[type="submit"]').each(function () {
                    $(this).attr("disabled", "disabled");
                });
            } else {
                $('input[type="submit"]').removeAttr('disabled');
            }
        }

        function afterSuccess() {
            $('#myModal' + $scope.formId).modal('show');
            formReset();
        }

        function formReset() {
            //reset form
            $('#' + $scope.formId)[0].reset();
            //reset dropdown
            $('.chosen-select').select2('val', 'All');
            //reset file upload
            $('.fileinput').fileinput('clear');
            //replace placeholder in file upload
            setFilePlaceHolder(null);
            //reset file upload object
            $scope.dragdropfiles = [];
        }

        $('.fileinput').each(function () {
            var input = $(this);
            $(this).click(function () {
                $(this).find('input[type="file"]').change(function (e) {
                    if (!e.target.files[0]) {
                        setTimeout(function () {
                            setFilePlaceHolder(input);
                        }, 100);
                    }
                });
            });
        });

        function setFilePlaceHolder(el) {
            if (!el) {
                el = $('.fileinput');
            }
            $(el).each(function () {
                var fileText = $(this).find('.fileUploadText');
                var placeHolder = $(fileText).attr('data-placeholder');
                $(fileText).text(placeHolder);
            });
        };

        function formValidation() {

            var form = $('#' + $scope.formId);
            var formGroups = form.find('.form-group');
            formGroups.each(function () {
                RemoveAllMessages(this);
            });

            angular.forEach($scope.submitResult.Errors, function (value, key) {
                var field = form.find('input:not(.drop-box)[name="' + value.FieldId + '"], textarea[name="' + value.FieldId + '"], select[name="' + value.FieldId + '"]');

                if (value.ErrorType == 'require') {
                    ValidateRequireField(field);
                    ValidateRadioCheckboxGroup(field);
                } else if (value.ErrorType == 'format') {
                    ValidateTelephoneField(field);
                    ValidateEmailField(field);
                }
            });

            ScrollTop(form);
            context.Valid = false;
        }

        //start drag and drop function
        $scope.uploadDragAndDropFiles = function (files, id) {

            var input = $('input:file.drop-box[id=' + id + ']');
            var formGroup = input.parents('.form-group');
            RemoveRequireMessage(formGroup);

            if (!$scope.dragdropfiles[id])
                $scope.dragdropfiles[id] = [];

            angular.forEach(files, function (file) {

                var hasFile = false;
                angular.forEach($scope.dragdropfiles[id], function (_file) {
                    if (_file.name == file.name)
                        hasFile = true;
                });

                if (!hasFile)
                    $scope.dragdropfiles[id].push(file);
            });

        }

        $scope.hasDragAndDropFiles = function (id) {
            return $scope.dragdropfiles[id] != null && !$.isEmptyObject($scope.dragdropfiles[id])
        }

        $scope.getDragAndDropFiles = function (id) {
            return $scope.dragdropfiles[id];
        }

        $scope.removeDragAndDropFile = function (id, index) {
            $scope.dragdropfiles[id].splice(index, 1);
        }
        //end drag and drop function

    }]);

