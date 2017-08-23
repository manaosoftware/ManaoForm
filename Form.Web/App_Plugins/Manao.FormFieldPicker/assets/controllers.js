angular.module("umbraco").controller("manaoInsertFieldController",
    [
        "$scope", "dialogService", "$http", "$routeParams",
        function (scope, dialogService, http, routeParams) {
            scope.value = null;
            http.get("/umbraco/backoffice/ManaoForm/FormPlugin/GetFormFields?Id=" + routeParams.id + "&fieldAlias=").then(function (response) {
                scope.fields = response.data;
            });
            var input = "<ins id='{0}' class='formField'>{1}</ins>";
            scope.select = function (index) {
                scope.value = input.replace("{0}", scope.fields[index].Id).replace("{1}", scope.fields[index].Name);
                scope.submit(scope.value);
            }
            scope.close = function () {
                dialogService.closeAll()
            },
            scope.isValid = function () {
                return scope.value != null;
            }
            scope.done = function () {
            }
        }
    ]
)
