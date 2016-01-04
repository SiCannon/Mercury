angular.module("app")
    .factory("ArtistService", ["$http", "$resource", "ErrorHandlerService", function ($http, $resource, ErrorHandler) {
        return {

            query: function (pageNumber, pageSize, searchText, success) {
                $http.get("/api/Artist", { params: { pageNumber: pageNumber, pageSize: pageSize, searchText: searchText } }).success(success).error(ErrorHandler.handle);
            },

            count: function (searchText, success) {
                $http.get("/api/Artist/Count", { params: { searchText: searchText } }).success(success);
            },

            get: function (id, success) {
                $http.get("/api/Artist/" + id.toString()).success(success).error(ErrorHandler.handle);;
            },

            save: function (artist, success) {
                $http.post("/api/Artist", artist).success(success);
            },

            delete: function (id, success) {
                $http.delete("/api/Artist/" + id.toString()).success(success);
            }

        };
    }]);
