angular.module("app")
    .controller("RecordingSelectController", ["$scope", "RecordingService", "SelectorService",
        function ($scope, RecordingService, SelectorService) {

            $scope.recordings = [];

            RecordingService.query(0, 10, "title", "", "title", true, function (data) { $scope.recordings = data; });

            $scope.ok = function (id) {
                SelectorService.ok(id);
            }

        }]);