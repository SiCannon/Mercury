angular.module("app")
    .directive('mpEscapeCancel', ["$parse", function ($parse) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ngModel) {
                var eventHandler = attr.mpEscapeCancel ? $parse(attr.mpEscapeCancel) : null;
                element.on("keyup", function (event) {
                    if (event.keyCode == 27) {
                        ngModel.$rollbackViewValue();
                        if (eventHandler) {
                            scope.$apply(function () {
                                eventHandler(scope);
                            });
                        }
                    }
                });
            }
        };
    }]);