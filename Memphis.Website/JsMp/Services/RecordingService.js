angular.module("app")
    .factory("RecordingService", ["$http", function ($http) {
        return {

            query: function (pageNumber, pageSize, searchField, searchText, sortBy, sortAscending, success) {
                $http.get("/api/Recording", { params: { pageNumber: pageNumber, pageSize: pageSize, searchField: searchField, searchText: searchText, sortBy: sortBy, sortAscending: sortAscending } }).success(success);
            },

            get: function (id, success) {
                $http.get("/api/Recording/" + id.toString()).success(success);
            }

        };
    }]);