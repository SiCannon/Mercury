angular.module("app")
    .directive("mpFocus", ["$timeout", function ($timeout) {
        return {
            scope: {
                mpFocus: "="
            },
            link: function (scope, element, attrs) {
                scope.$watch("mpFocus", function (value) {
                    if (value === true) {
                        $timeout(function () {
                            element[0].focus();
                        });
                    }
                });
            }
        };
    }]);