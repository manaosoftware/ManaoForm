angular.module('umbraco.resources').factory('manaoGridControlResource', function ($http) {
    return {
        GetFields: function (id, fieldAlias) {
            return $http.get("/umbraco/backoffice/ManaoForm/FormPlugin/GetFormFields?Id=" + id + "&fieldAlias=" + fieldAlias);
        }
    };
});