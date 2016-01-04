angular.module("app")
    .factory("ErrorHandlerService", ["$location", function ($location) {
        var errorMessage;
        var lastPath;
        return {
            handle: function (data, status, headers, config) {
                if (status == 404) {
                    errorMessage = "Not found";
                } else {
                    errorMessage = (data || {}).ExceptionMessage;
                }
                lastPath = $location.path();
                $location.path("/error");
            },
            getErrorMessage: function () {
                return errorMessage;
            },
            getLastPath: function () {
                return lastPath;
            }
        };
    }]);