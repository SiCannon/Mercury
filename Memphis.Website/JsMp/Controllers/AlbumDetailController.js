angular.module("app")
    .controller("AlbumDetailController", ["$scope", "AlbumService", "$routeParams", "$location", "$route", "ArtistService", "SelectorService", "RecordingService",
        function ($scope, AlbumService, $routeParams, $location, $route, ArtistService, SelectorService, RecordingService) {

            function loadAlbum(id) {
                AlbumService.get(id, function (data) { $scope.album = data; });
            }

            function saveComplete(id) {
                loadAlbum(id);
            }

            $scope.Submit = function (backToList) {
                AlbumService.save($scope.album, function (id) {
                    if (backToList) {
                        $scope.GoBack();
                    }
                    else {
                        if (id !== $routeParams.albumId) {
                            $location.path("/album/" + id.toString());
                        }
                        else {
                            $route.reload();
                        }
                    }
                });
            };

            $scope.GoBack = function () {
                $location.path("/album");
            };

            if (SelectorService.isReturned()) {
                SelectorService.applySelection($scope);
            }
            else {
                if ($routeParams.albumId == "new") {
                    $scope.album = {};
                }
                else {
                    loadAlbum($routeParams.albumId);
                }
            }

            function populateAlbum(scope, album) {
                scope.album = album;
            }

            function populateArtistDetails(scope, artistId) {
                ArtistService.get(artistId, function (artist) {
                    scope.album.ArtistId = artist.ArtistId;
                    scope.album.ArtistName = artist.Name;
                });
            }

            function addTrack(scope, recordingId) {
                RecordingService.get(recordingId, function (recording) {
                    scope.album.Tracks = scope.album.Tracks || [];
                    scope.album.Tracks.push({
                        TrackId: null,
                        Position: getNewTrackPosition(),
                        RecordingId: recording.RecordingId,
                        RecordingTitle: recording.Title
                    });
                });
            }

            $scope.selectArtist = function () {
                SelectorService.select("/artist/select", $scope.album, populateAlbum, populateArtistDetails);
            };

            $scope.selectNewTrack = function () {
                SelectorService.select("/recording/select", $scope.album, populateAlbum, addTrack);
            };

            function getNewTrackPosition() {
                var max = 0;
                angular.forEach($scope.album.Tracks, function (track) {
                    if (!track.isDeleted && track.Position > max) {
                        max = track.Position;
                    }
                });
                return max + 1;
            }

            $scope.hasArtist = function () {
                return typeof $scope.album !== 'undefined'
                    && typeof $scope.album.ArtistId !== 'undefined'
                    && $scope.album.ArtistId !== null;
            };

            $scope.removeArtist = function () {
                if (typeof $scope.album !== 'undefined') {
                    $scope.album.ArtistId = null;
                    $scope.album.ArtistName = null;
                }
            };

        }]);