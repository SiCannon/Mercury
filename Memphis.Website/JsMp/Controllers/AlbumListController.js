angular.module("app")
    .controller("AlbumListController", ["$scope", "$http", "AlbumService", "PagingTrackerService",
        function ($scope, $http, AlbumService, PagingTrackerService) {

            $scope.SearchField = "title";
            $scope.SearchText = "";
            $scope.sortBy = "title";
            $scope.sortAscending = true;

            $scope.Paging = PagingTrackerService.init("album", 10);

            function GetData() {
                AlbumService.query($scope.Paging.CurrentPage, $scope.Paging.PageSize, $scope.SearchField, $scope.SearchText, $scope.sortBy, $scope.sortAscending, function (x) { $scope.Albums = x; });
            }

            $scope.Paging.getData = GetData;

            function GetCount() {
                AlbumService.count($scope.SearchField, $scope.SearchText, function (x) { $scope.Paging.TotalCount = x; });
            }

            $scope.Search = function (resetToFirstPage) {
                if (resetToFirstPage) {
                    $scope.Paging.CurrentPage = 0;
                }
                GetData();
                GetCount();
            };

            $scope.Delete = function (id) {
                AlbumService.delete(id, GetData);
            };

            $scope.Search(false);

            $scope.$watch("Paging.PageSize", function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    $scope.Search(true);
                }
            });

        }]);