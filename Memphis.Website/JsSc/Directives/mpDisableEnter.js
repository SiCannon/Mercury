angular.module("app")
    .directive("mpDisableEnter", function () {
        return {
            restrict: "A",
            link: function (scope, element, attr) {
                element.on("keypress", function (event) {
                    if (event.keyCode == 13) {
                        event.preventDefault();
                    }
                });
            }
        };
    });