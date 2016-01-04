angular.module("app", ["ngRoute", "ngResource", "ui.select", "ngSanitize", "sc"])

    .config(['$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                when('/error', {
                    templateUrl: 'Templates/Common/Error.html',
                    controller: 'ErrorPageController'
                }).
                when('/album', {
                    templateUrl: 'Templates/Album/List.html',
                    controller: 'AlbumListController'
                }).
                when('/album/:albumId', {
                    templateUrl: 'Templates/Album/Detail.html',
                    controller: 'AlbumDetailController'
                }).
                when('/artist', {
                    templateUrl: 'Templates/Artist/List.html',
                    controller: 'ArtistListController'
                }).
                when('/artist/select', {
                    templateUrl: 'Templates/Artist/Select.html',
                    controller: 'ArtistSelectController'
                }).
                when('/artist/:artistId', {
                    templateUrl: 'Templates/Artist/Detail.html',
                    controller: 'ArtistDetailController'
                }).
                when('/recording/select', {
                    templateUrl: 'Templates/Recording/Select.html',
                    controller: 'RecordingSelectController'
                }).
                otherwise({
                    redirectTo: '/artist'
                });
        }]);