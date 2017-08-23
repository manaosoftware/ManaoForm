angular.module('umbraco.resources').factory('dictionaryResource',
function ($http) {
    return {
        getText: function (key) {
            return $http.get('/umbraco/backoffice/UmbracoApi/Authentication/GetCurrentUser').then(function (response) {
                return $http.get("/umbraco/api/formdictionaryapi/getdictionary/?locale=" + response.data.locale + "&key=" + key);
            });
        }
    }
});