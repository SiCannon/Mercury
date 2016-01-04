angular.module("app")
    .factory("AlbumService", ["$http", function ($http) {
        return {

            query: function (pageNumber, pageSize, searchField, searchText, sortBy, sortAscending, success) {
                $http.get("/api/Album", { params: { pageNumber: pageNumber, pageSize: pageSize, searchField: searchField, searchText: searchText, sortBy: sortBy, sortAscending: sortAscending } }).success(success);
            },

            count: function (searchField, searchText, success) {
                $http.get("/api/Album/Count", { params: { searchField: searchField, searchText: searchText } }).success(success);
            },

            get: function (id, success) {
                $http.get("/api/Album/" + id.toString()).success(success);
            },

            save: function (album, success) {
                $http.post("/api/Album", album).success(success);
            },

            delete: function (id, success) {
                $http.delete("/api/Album/" + id.toString()).success(success);
            }

        };
    }]);
