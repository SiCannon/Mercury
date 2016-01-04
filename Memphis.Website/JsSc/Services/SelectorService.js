angular.module("app")
    .factory("SelectorService", ["$location", function ($location) {

        var isReturning = false;
        var sourceUrl = null;
        var savedSourceData = null;
        var selectedId = null;
        var setDataFunction = null;
        var setIdFunction = null;

        function select(selectorUrl, sourceData, setData, setId) {
            isReturning = false;
            selectedId = null;
            sourceUrl = $location.url();
            savedSourceData = sourceData;
            setDataFunction = setData;
            setIdFunction = setId;
            $location.path(selectorUrl);
        }

        function ok(id) {
            isReturning = true;
            selectedId = id;
            $location.url(sourceUrl);
        }

        function cancel() {
            ok(null);
        }

        function isReturned() {
            return isReturning;
        }

        function applySelection(scope) {
            setDataFunction(scope, savedSourceData);
            if (selectedId) {
                setIdFunction(scope, selectedId);
            }
            isReturning = false;
        }

        return {
            select: select,
            ok: ok,
            cancel: cancel,
            isReturned: isReturned,
            applySelection: applySelection
        };

    }]);