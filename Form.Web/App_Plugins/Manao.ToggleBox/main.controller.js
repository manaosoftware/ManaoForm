angular.module("umbraco").controller("ToggleBoxController", function ($scope, angularHelper) {

        if (!$scope.model.config.onlabel) {
            $scope.model.config.onlabel = 'True';
        }

        if (!$scope.model.config.offlabel) {
            $scope.model.config.onlabel = 'False';
        }
         //console.log($scope.model);
        if (!$scope.model.value) {
            if ($scope.model.config.default == 1) {
                $scope.model.value = 1;
            }
            else {
                $scope.model.value = 0;
            }
        }

        $scope.setActive = function () {
            if ($scope.model.value == 1) {
                $scope.model.leftActiveClass = 'active';
                $scope.model.rightActiveClass = '';
            }
            else {
                $scope.model.rightActiveClass = 'active';
                $scope.model.leftActiveClass = '';
            }
        }
        $scope.setActive();

        $scope.setTrue = function () {
            $scope.setDirty();
            $scope.model.value = 1;
            $scope.setActive();
        }
        $scope.setFalse = function () {
            $scope.setDirty();
            $scope.model.value = 0;
            $scope.setActive();
        }

        $scope.setDirty = function () {
            if (!$scope.isDirty) {
                $scope.isDirty = true;
                var currentForm = angularHelper.getCurrentForm($scope);
                currentForm.$setDirty();
            }
        }
});
