angular.module("app")
    .factory("PagingTrackerService", ["PagingInfo", function (PagingInfo) {

        var cache = {};

        return {

            init: function (id, initialPageSize) {
                if (!cache[id]) {
                    cache[id] = new PagingInfo(initialPageSize);
                }
                return cache[id];
            }

        }

    }]);