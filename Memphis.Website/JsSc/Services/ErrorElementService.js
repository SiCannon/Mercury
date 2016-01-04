angular.module("app")
    .factory("ErrorElementService", ["$compile", function ($compile) {
        return {
            create: function (scope, element, attrs, formController, directive, message) {
                var name = attrs.mpDesc || attrs.name;
                var show = formController.$name + "." + attrs.name + ".$error." + directive;
                if (attrs.mpShowError) {
                    show = show + " && " + attrs.mpShowError;
                }
                var errorElement = angular.element("<p class='error' ng-show='" + show + "'>" + name + " " + message + "</p>");
                element.after(errorElement);
                $compile(errorElement)(scope);
            }
        };
    }]);