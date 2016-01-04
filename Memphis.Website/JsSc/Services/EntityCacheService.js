angular.module("app")
    .factory("EntityCacheService", ["$cacheFactory", function ($cacheFactory) {
        return {
            get: function (id) {
                var cache = $cacheFactory.get("entityCache");
                if (cache) {
                    return cache.get(id);
                }
                else {
                    return null;
                }
            },
            put: function (id, item) {
                var cache = $cacheFactory.get("entityCache");
                if (!cache) {
                    cache = $cacheFactory("entityCache");
                }
                cache.put(id, item);
            }
        };
    }]);