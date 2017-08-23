angular.module("umbraco").controller("ManaoFormPickerController", function ($scope, $timeout, $element, ManaoFormPickerResource) {
    $scope.initData = function () {
        $scope.control.value = {
            Id: 0,
            FormId: $scope.control.$uniqueId,
            Title: ''
        }
    }
    if (!$scope.control.value) {
        $scope.initData();
    }
    else {
        if (!$scope.control.value.Id) {
            var tempId = angular.copy($scope.control.value);
            $scope.initData();
            $scope.control.value.Id = tempId;
        }
    }

    ManaoFormPickerResource.GetForms().then(function (response) {
        if (response.data) {
            $scope.forms = response.data;
            $scope.forms.filter(function (item) {
                if (item.Id == $scope.control.value.Id) {
                    item.select = 'selected';
                }
            });
            $timeout(function () {
                setSelected($element);
            }, 50);
        }
    });

    var setSelected = function (element) {
        $(element).find('option').each(function () {
            if ($(this).attr('data-select')) {
                if ($(this).attr('data-select').indexOf('selected') > -1) {
                    $(this).attr('selected', 'selected');
                }
            }
            if ($(this).attr('value').indexOf('?') > -1) {
                $(this).remove();
            }
        });
    }
});