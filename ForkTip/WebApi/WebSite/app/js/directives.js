'use strict';

/* Directives */


angular.module('app.directives', ['app.controllers', 'xeditable']).
    directive('appVersion', ['version', function (version) {
        return function (scope, elm, attrs) {
            elm.text(version);
        };
    }]).
    directive('xeditable', [function () {
        return {
            templateUrl: 'app/partials/xeditable.html',
            restrict: 'E',
            link: function (scope, elem, attrs) {
                console.log(elem['0'].attributes[0]);
                scope.onBeforeSaveFunc = function ($data) {
                    if (scope.onBeforeSave) {
                        scope.onBeforeSave({ $data: $data });
                    }
                };
                scope.onAfterSaveFunc = function ($data) {
                    if (scope.onAfterSave) {
                        scope.onAfterSave({ $data: $data });
                    }
                };

                scope.default = scope.default || 'Click to edit';
            },
            scope: {
                property: '=',
                trigger: '=',
                default: '@',
                onBeforeSave: '&',
                onAfterSave: '&'
            }
        };
    }]).
    directive('xeditablearea', [function() {
        return {
            templateUrl: 'app/partials/xeditableArea.html',
            restrict: 'E',
            link: function (scope, elem, attrs) {
                console.log(elem['0'].attributes[0]);
                scope.onBeforeSaveFunc = function ($data) {
                    if (scope.onBeforeSave) {
                        scope.onBeforeSave({ $data: $data });
                    }
                };
                scope.onAfterSaveFunc = function ($data) {
                    if (scope.onAfterSave) {
                        scope.onAfterSave({ $data: $data });
                    }
                };

                scope.default = scope.default || 'Click to edit';
            },
            scope: {
                property: '=',
                trigger: '=',
                default: '@',
                onBeforeSave: '&',
                onAfterSave: '&'
            }
        };
    }]);