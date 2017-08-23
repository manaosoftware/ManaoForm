angular.module("umbraco").controller("ManaoFolderPickerController", function ($scope, $attrs, dialogService, dictionaryResource, angularHelper, notificationsService) {

    $scope.isDirty = false;
    if (!$scope.model.value) {
        $scope.model.value = {};
    }
    

    $scope.openFolderPicker = function () {
        dialogService.mediaPicker({
            startNodeId: -1,
            multiPicker: false,
            callback: function (data) {
                if (data.contentTypeAlias == 'Folder') {
                    $scope.model.value.id = data.id;
                    $scope.model.value.name = data.name;
                    console.log($scope.model.value);
                }
                else {
                    notificationsService.error($scope.dictionary.general.fail, $scope.dictionary.field.uploadfielderrormessage);
                }
            }
        });
    }

    $scope.removeFolder = function () {
        $scope.model.value = {};
    }

    dictionaryResource.getText($attrs.dictionary).then(function (response) {
        $scope.dictionary = response.data.language;
    });

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
});