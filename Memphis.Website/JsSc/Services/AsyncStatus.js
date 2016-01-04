/*
    Constructor factory object: AsyncStatus
    Summary: store and retrieve the status of an async process
    Description: This is a simple class which can be used to track the status of an item within a controller.
        It is designed so that you do not need to use cryptic integers to store status or anything like that.
        e.g. ng-if="status.IsLoaded()" is easier to understand in the page than ng-if="status == 1"
    Note: this returns a constructor object so you must call "new AsyncStatus()" to create one.
*/

angular.module("sc")
    .factory("AsyncStatus", function () {
        return function () {

            var stats = {
                loading: 0,
                loaded: 1,
                saving: 2,
                saved: 3,
                error: 4
            }

            var status = stats.loading;

            this.isLoading = function () {
                return status == stats.loading;
            }
            this.isLoaded = function () {
                return status == stats.loaded;
            }
            this.isSaving = function () {
                return status == stats.saving;
            }
            this.isSaved = function () {
                return status == stats.saved;
            }
            this.isError = function () {
                return status == stats.error;
            }

            this.clear = function () {
                status = null;
            }

            this.loading = function () {
                status = stats.loading;
            }
            this.loaded = function () {
                status = stats.loaded;
            }
            this.saving = function () {
                status = stats.saving;
            }
            this.saved = function () {
                status = status.saved;
            }
            this.error = function () {
                status = stats.error;
            }

        }
    });