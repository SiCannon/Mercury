angular.module("app")
    .controller("ArtistListController", ["$scope", "$http", "ArtistService", "PagingTrackerService", "AsyncStatus",
        function ($scope, $http, ArtistService, PagingTrackerService, AsyncStatus) {

            $scope.status = new AsyncStatus();

            $scope.SearchText = "";

            $scope.Paging = PagingTrackerService.init("artist");

            function GetData() {
                $scope.status.loading();
                ArtistService.query($scope.Paging.CurrentPage, $scope.Paging.PageSize, $scope.SearchText, function (x) {
                    $scope.Artists = x;
                    $scope.status.loaded();
                });
            }
            
            $scope.Paging.getData = GetData;

            function GetCount() {
                ArtistService.count($scope.SearchText, function (x) { $scope.Paging.TotalCount = x; });
            }

            $scope.Search = function (resetToFirstPage) {
                if (resetToFirstPage) {
                    $scope.Paging.CurrentPage = 0;
                }
                GetData();
                GetCount();
            };

            $scope.Delete = function (id) {
                ArtistService.delete(id, GetData);
            };

            $scope.Search(false);

        }]);