'use strict';

/* Controllers */

angular.module('app.controllers', ['rx', 'ui.bootstrap', 'xeditable', 'ui.sortable'])
    .controller('MainCtrl', [
        '$scope', '$routeParams', '$http', 'login', function($scope, $routeParams) {
            console.log("MainCtrl");

            console.log($routeParams);
        }
    ])
    .controller('RecipeCtrl', [
        '$scope',
        '$routeParams',
        '$location',
        'recipe',
        function ($scope, $routeParams, $location, recipe) {
            var kind = 'recipe';
            var id = $routeParams['r'];
            if (!id) {
                $location.path('/welcome');
            } else if ($location.path() === '/welcome') {
                $location.path('/recipe');
            }

            $scope.recipe = new Recipe();
            console.log("RecipeCtrl");
            console.log('Recipe ' + id);
            recipe.recipe(id).subscribe(function (value) {
                console.log('Recipe ' + id + ' = ' + JSON.stringify(value));
                $scope.recipe = value;
            });
            $scope.isEditing = true;
            $scope.addIngredient = addCollectionValue('ingredients');
            $scope.addDirection = addCollectionValue('directions');

            $scope.set = recipe.set;
            $scope.add = function () {
                recipe.add().subscribe(function (idString) {
                    var newId = JSON.parse(idString).replace(new RegExp('-', 'g'), '');
                    $location.search({ r: newId });
                    $location.path('/recipe');
                });
            };

            $scope.share = function () {
                var orig = $location.path();
                $location.path('/r');
                var shareUrl = $location.absUrl();
                $location.path(orig);
                var tweetUrl = 'https://twitter.com/share?text=Check out my recipe &hashtags=ForkTip&url=' + encodeURIComponent(shareUrl);
                var width = 575,
                height = 400,
                left = ($(window).width() - width) / 2,
                top = ($(window).height() - height) / 2,
                url = tweetUrl,
                opts = 'status=1' +
                         ',width=' + width +
                         ',height=' + height +
                         ',top=' + top +
                         ',left=' + left;

                window.open(url, 'twitter', opts);
            };
            
            $scope.setAtIndex = function (property, index, data) {
                $scope[kind][property][index] = data;
                $scope.set(property)($scope[kind].id, $scope[kind][property]);
            };

            $scope.removeAtIndex = function (property, index) {
                var collection = $scope[kind][property];

                if (index >= 0) {
                    collection.splice(index, 1);
                }

                $scope.set(property)($scope[kind].id, $scope[kind][property]);
            };

            $scope.fork = function() {
                return recipe.fork($scope[kind].id).subscribe(function (idString) {
                    var newId = JSON.parse(idString).replace(new RegExp('-', 'g'), '');
                    $location.search({ r: newId });
                });
            };

            $scope.ingredientsSortableOptions = {
                update: updateCollection('ingredients')
            };

            $scope.directionsSortableOptions = {
                update: updateCollection('directions')
            };

            function addCollectionValue(property) {
                return function () {
                    var collection = $scope[kind][property];
                    var last = collection[collection.length - 1];
                    if (last || collection.length === 0) {
                        $scope[kind][property].push('');
                    }
                };
            }

            function updateCollection(property) {
                return function () {
                    $scope.$apply(function () {
                        console.log($scope[kind][property]);
                        return $scope.set(property)($scope[kind].id, $scope[kind][property]);
                    });
                };
            }
        }
    ]);

function Recipe() {
    this.forkedFrom = '';
    this.name = '';
    this.author = '';
    this.description = '';
    this.ingredients = [];
    this.directions = [];
    this.images = [];
}