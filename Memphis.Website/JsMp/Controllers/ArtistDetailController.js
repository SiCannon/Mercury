angular.module("app").controller("ArtistDetailController",
    ["$scope", "$routeParams", "$route", "$location", "ArtistService",
        function ($scope, $routeParams, $route, $location, ArtistService) {

            $scope.Artist = {};
            $scope.HasCustomSortName = true;
            $scope.submitted = false;

            var isNew = $routeParams.artistId == "new";

            function loadArtist(id) {
                ArtistService.get(id, function (x) {
                    $scope.Artist = x;
                    $scope.HasCustomSortName = ($scope.Artist.Name != $scope.Artist.SortName);
                });
            }

            if (isNew) {
                $scope.HasCustomSortName = false;
                //todo: create tags array in scope.Artist. Or maybe do it later?
            }
            else {
                loadArtist($routeParams.artistId);
            }

            $scope.Submit = function (backToList) {
                $scope.submitted = true;
                if ($scope.artistDetailForm.$valid) {
                    ArtistService.save($scope.Artist, function (id) {
                        if (backToList) {
                            $location.path("/artist");
                        }
                        else {
                            if (isNew) {
                                $location.path("/artist/" + id.toString());
                            }
                            else {
                                loadArtist(id);
                            }
                        }
                    });
                }
            }

            $scope.Cancel = function () {
                $location.path("/artist");
            }

            $scope.NameChanged = function () {
                if (!$scope.HasCustomSortName) {
                    $scope.Artist.SortName = $scope.Artist.Name;
                }
            }

            $scope.SortNameClicked = function () {
                if (!$scope.HasCustomSortName) {
                    $scope.Artist.SortName = $scope.Artist.Name;
                }
            }

            $scope.newTag = function () {
                $scope.Artist.ArtistTags = $scope.Artist.ArtistTags || [];
                $scope.Artist.ArtistTags.push({
                    Tag: { Name: "new tag" },
                    isJustAdded: true,
                    IsNew: true
                });
            }


        }]);