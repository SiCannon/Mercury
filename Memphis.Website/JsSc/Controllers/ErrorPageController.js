angular.module("app")
    .controller("ErrorPageController", ["$scope", "ErrorHandlerService", "$window",
        function ($scope, ErrorHandlerService, $window) {

            $scope.errorMessage = ErrorHandlerService.getErrorMessage();

            //This causes an infinte loop in the digest
            //$window.history.replaceState(null, null, ErrorHandlerService.getLastPath());

        }]);