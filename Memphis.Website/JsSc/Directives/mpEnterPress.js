angular.module("app")
    .directive("mpEnterPress", ["$parse", function ($parse) {
        return {
            restrict: "A",
            link: function (scope, element, attr) {
                var eventHandler = $parse(attr.mpEnterPress);
                element.on("keypress", function (event) {
                    if (event.keyCode == 13) {
                        scope.$apply(function () {
                            eventHandler(scope);
                            event.preventDefault();
                        });
                    }
                });
            }
        };
    }]);