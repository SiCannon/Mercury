angular.module("app")
    .directive("mpRequired", ["ErrorElementService", function (ErrorElementService) {
        return {
            require: ["ngModel", "^form"],
            restrict: "A",
            link: function (scope, element, attr, ctrls) {
                ctrls[0].$parsers.unshift(function (value) {
                    var isValid = value ? true : false;
                    ctrls[0].$setValidity("mpRequired", isValid);
                    return isValid ? value : undefined;
                });
                ErrorElementService.create(scope, element, attr, ctrls[1], "mpRequired", "is required");
            }
        };
    }]);