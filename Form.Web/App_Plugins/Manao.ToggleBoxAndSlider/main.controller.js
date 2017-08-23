angular.module("umbraco").controller("ToggleBoxSliderController", function ($scope, angularHelper, $timeout, $element) {
    
    if (!$scope.model.config.onlabel) {
        $scope.model.config.onlabel = 'True';
    }

    if (!$scope.model.config.offlabel) {
        $scope.model.config.onlabel = 'False';
    }
    if (!$scope.model.value) {
        $scope.model.value = {};
    }
    if ($scope.model.value.state != 0 && $scope.model.value.state != 1) {
        if ($scope.model.config.default == 1) {
            $scope.model.value.state = 1;
        }
        else {
            $scope.model.value.state = 0;
        }
    }
    if (!$scope.model.value.priority) {
        $scope.model.value.priority = 0.5;
    }
        
    $scope.setActive = function () {
        $timeout(function () {
            if ($scope.model.value.state == 1) {
                $scope.model.leftActiveClass = 'active';
                $scope.model.rightActiveClass = '';
            }
            else {
                $scope.model.rightActiveClass = 'active';
                $scope.model.leftActiveClass = '';
            }
        }, 500);
        
    }
    
    $scope.setActive();

    $scope.setTrue = function () {
        $scope.setDirty();
        $scope.model.value.state = 1;
        $scope.setActive();
    }
    $scope.setFalse = function () {
        $scope.setDirty();
        $scope.model.value.state = 0;
        $scope.setActive();
    }

    $scope.setDirty = function () {
        if (!$scope.isDirty) {
            $scope.isDirty = true;
            var currentForm = angularHelper.getCurrentForm($scope);
            currentForm.$setDirty();
        }
    }

    $scope.rangeChange = function () {
        $scope.model.value.priority = $scope.range/10;
    }
        
    $scope.initRange = function() {
        var element = $($element).find('input.range-slider')[0];
        if(element) {
            $scope.range = $scope.model.value.priority * 10;
            $timeout(function () {
                $(element).rangeslider({
                    polyfill: false
                });
            }, 500);
        }
        else {
            $timeout(function(){
                $scope.initRange();
            },500);
        }
    }
    $scope.initRange();
});