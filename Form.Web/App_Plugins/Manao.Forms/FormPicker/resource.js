angular.module('umbraco.resources').factory('ManaoFormPickerResource', function ($http) {
    return {
        GetForms: function () {
            return $http.get("/umbraco/backoffice/ManaoForm/FormPlugin/GetForms");
        }
    };
});