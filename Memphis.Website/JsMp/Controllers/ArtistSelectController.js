angular.module("app")
    .controller("ArtistSelectController", ["$scope", "ArtistService", "SelectorService",
        function ($scope, ArtistService, SelectorService) {

            ArtistService.query(0, 10, null, function (x) { $scope.artists = x; });

            $scope.ok = function (artistId) {
                SelectorService.ok(artistId);
            };

            $scope.cancel = function () {
                SelectorService.cancel();   
            };

        }]);