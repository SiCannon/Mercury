angular.module("app").directive("mpEditableArtistTag",
    ["$timeout",
        function ($timeout) {
            return {

                templateUrl: "Templates/Directives/mpEditableArtistTag.html",

                scope: {
                    artistTag: "="
                },

                link: function (scope, element, attrs) {

                    scope.isEditing = false;
                    scope.originalTag = angular.copy(scope.artistTag.Tag);

                    scope.toggleDeleteTag = function () {
                        scope.artistTag.IsDeleted = !scope.artistTag.IsDeleted;
                    };

                    scope.beginEditTag = function () {
                        scope.isEditing = true;
                        if (scope.artistTag.isJustAdded) {
                            $timeout(function () {
                                var input = element[0].querySelector(".artist-tag-name");
                                input.select();
                            });
                        }
                    };

                    scope.endEditTag = function () {
                        scope.isEditing = false;
                        scope.artistTag.isJustAdded = false;
                    };

                    scope.revert = function () {
                        scope.artistTag.Tag = angular.copy(scope.originalTag);
                    };

                    scope.$watch("artistTag.Tag.Name", function () {
                        scope.artistTag.IsEdited = scope.artistTag.Tag.Name !== scope.originalTag.Name;
                    });

                    if (scope.artistTag.isJustAdded) {
                        scope.beginEditTag();
                    }

                }
            };
        }]);
