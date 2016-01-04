angular.module("app")
    .directive("mpGuid", ["ErrorElementService", function (ErrorElementService) {

        /*function createErrorElement(scope, element, attrs, formController) {
            var name = attrs.mpDesc || attrs.name;
            var show = formController.$name + "." + attrs.name + ".$error.mpGuid";
            if (attrs.mpShowError) {
                show = show + " && " + attrs.mpShowError;
            }
            var errorElement = angular.element("<p class='error' ng-show='" + show + "'>" + name + " must be a valid GUID</p>");
            element.after(errorElement);
            $compile(errorElement)(scope);
        }*/

        return {
            require: ["ngModel", "^form"],
            restrict: "A",
            link: function (scope, element, attr, ctrl) {
                var regex = new RegExp("^[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}$");
                ctrl[0].$parsers.unshift(function (value) {
                    var isValid = !value || regex.test(value);
                    ctrl[0].$setValidity('mpGuid', isValid);
                    return isValid ? value : undefined;
                });
                ErrorElementService.create(scope, element, attr, ctrl[1], "mpGuid", "must be a valid GUID!!");
            }
        };
    }]);