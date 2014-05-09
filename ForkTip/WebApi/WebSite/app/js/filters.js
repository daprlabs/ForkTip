'use strict';

/* Filters */

angular.module('app.filters', []).
    filter('interpolate', [
        'version', function(version) {
            return function(text) {
                return String(text).replace(/\%VERSION\%/mg, version);
            };
        }
    ])
    .filter('either', [
        function () {
            return function (input, truthy, falsy) {
                return input ? truthy : falsy;
            };
        }
    ])
    .filter('length', [
        function () {
            return function (input) {
                var result = 0;
                if (typeof input === 'number') {
                    result = input;
                } else if (typeof input === 'object' && input instanceof Array) {
                    result = input.length;
                } else if (typeof input === 'string') {
                    result = input.length;
                }

                return '' + result.toString(10);
            };
        }
    ]);