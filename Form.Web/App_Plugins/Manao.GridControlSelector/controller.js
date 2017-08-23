angular.module("umbraco").controller("manaoGridControlPickerController", function ($scope, dialogService, $routeParams, $timeout, $rootScope, $compile, angularHelper, $attrs, manaoGridControlResource) {

    $scope.fields = [];
    if (!$scope.model.value) {
        $scope.model.value = [];
    }
    else {
        manaoGridControlResource.GetFields($routeParams.id, "").success(function (data) {
            $scope.fields = data;
            for (i = 0; i < $scope.model.value.length; i++) {
                var fieldsMatched = $scope.fields.filter(function (f) {
                    if (f.Id == $scope.model.value[i].Id) {
                        return f;
                    }
                });
                if (!fieldsMatched.length) {
                    $scope.model.value.splice(i, 1);
                }
            }
        });
    }

    //dictionaryResource.getText("").then(function (response) {
    //    $scope.dictionary = response.data.language;
    //});

    $scope.$watch('model.value', function (n, o) {
        if (n != o) {
            $scope.setDirty();
        }
    });

    $scope.setDirty = function () {
        if (!$scope.isDirty) {
            $scope.isDirty = true;
            var currentForm = angularHelper.getCurrentForm($scope);
            currentForm.$setDirty();
        }
    }

    $scope.openItemSetting = function () {
        var editor = '/App_Plugins/Manao.GridControlSelector/setting.html';
        dialogService.open({
            template: editor,
            callback: function (data) {
                $scope.filterField();
            },
            dialogData: {
                model: $scope.model,
                dic: $scope.dictionary,
                id: $routeParams.id
            }
        });
    }
});

angular.module("umbraco").controller("manaoGridControlSelectorSettingController", function ($scope, $timeout, $rootScope, dialogService, notificationsService, $routeParams, manaoGridControlResource) {
    $scope.model = $scope.dialogData.model;
    $scope.dictionary = $scope.dialogData.dic;
    $scope.controlAlias = $scope.model.config.controlAlias;
    $scope.max = $scope.model.config.max;
    $scope.fields = [];
    if (!$scope.model.value) {
        $scope.model.value = [];
    }
    manaoGridControlResource.GetFields($scope.dialogData.id, $scope.controlAlias).success(function (data) {
        $scope.fields = data;
        $scope.renderFields();
    });

    $scope.select = function (field) {
        var prevalue = $scope.model.value.filter(function (value) {
            if (value.Id == field.Id) {
                return value;
            }
        });
        if (!prevalue.length) {
            if ($scope.max == null || $scope.max == '' || $scope.max == undefined || $scope.model.value.length < $scope.max) {
                $scope.model.value.push({ Id: field.Id, Icon: field.Icon, Name: field.Name });
            }
        }
        else {
            var index;
            angular.forEach($scope.model.value, function (prevalue, key) {
                if (field.Id == prevalue.Id) {
                    index = key;
                };
            });
            $scope.model.value.splice(index, 1);
        }
        $scope.renderFields();
    }
    $scope.renderFields = function () {
        angular.forEach($scope.fields, function (field, key) {
            $scope.fields[key].Icon = field.FieldType.Icon;
            $scope.fields[key].Style = '';

            angular.forEach($scope.model.value, function (prevalue, pkey) {
                if (field.Id == prevalue.Id) {
                    $scope.fields[key].Style = "selected";
                    $scope.fields[key].Icon = "icon-check";
                };
            });

        });
    }
});