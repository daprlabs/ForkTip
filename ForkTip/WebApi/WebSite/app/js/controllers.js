'use strict';

/* Controllers */

angular.module('app.controllers', ['rx', 'ui.bootstrap', 'xeditable'])
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
            $scope.recipe = new Recipe();
            console.log("RecipeCtrl");
            var id = $routeParams['r'];
            console.log('Recipe ' + id);
            recipe.recipe(id).subscribe(function (value) {
                console.log('Recipe ' + id + ' = ' + JSON.stringify(value));
                $scope.recipe = value;
            });
            $scope.isEditing = true;
            $scope.addIngredient = addCollectionValue('ingredients');
            $scope.addDirection = addCollectionValue('directions');

            $scope.set = recipe.set;
            $scope.add = function() {
                recipe.add().subscribe(function(idString) {
                    var newId = JSON.parse(idString).replace(new RegExp('-', 'g'), '');
                    $location.search({ r: newId });
                });
            };

            $scope.setAtIndex = function(property, index, data) {
                $scope[kind][property][index] = data;
                $scope.set(property)($scope[kind].id, $scope[kind][property]);
            };

            $scope.fork = function() {
                return recipe.fork($scope[kind].id).subscribe(function (idString) {
                    var newId = JSON.parse(idString).replace(new RegExp('-', 'g'), '');
                    $location.search({ r: newId });
                });
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
        }
    ])/*
    .controller('UsersCtrl', [
        '$scope',
        'ngTableParams',
        'users',
        function($scope, ngTableParams, users) {
            $scope.users = {};
            console.log("UsersCtrl");
            users.users.subscribe(function(result) {
                console.log(result);
                $scope.users = result;
                $scope.tableParams.reload();
            });

            $scope.add = function() {
                users.add().subscribe(function() {
                    $scope.tableParams.reload();
                });
            }

            // Configure the producers table.
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: {
                    id: 'asc'
                }
            }, {
                total: function() { return $scope.users.total; },
                getData: function($defer, params) {
                    console.log(params);
                    var start = (params.page() - 1) * params.count();
                    console.log("Total users: " + $scope.users.total);
                    var end = Math.min($scope.users.total, params.page() * params.count());
                    console.log("From " + start + " to " + end);
                    Rx.Observable.range(start, end).flatMap(users.user).toArray().subscribe($defer.resolve);
                }
            });
        }
    ])
    .controller('ProducersCtrl', [
        '$scope',
        'ngTableParams',
        'producers',
        function($scope, ngTableParams, producers) {
            $scope.add = function() {
                producers.add().subscribe(function(id) {
                    var idNum = parseInt(id);
                    if (typeof idNum === 'number') {
                        $scope.producers.total = idNum + 1;
                    }
                    $scope.tableParams.reload();
                });
            };

            $scope.set = producers.set;
            /*$scope.setWebSite = producers.setWebSite;#1#

            $scope.producers = {
                total: 0
            };
            producers.producers.subscribe(function(result) {
                console.log(result);
                $scope.producers = result;
                $scope.tableParams.reload();
            });

            // Configure the producers table.
            $scope.tableParams = new ngTableParams({
                page: 1,
                count: 10,
                sorting: {
                    id: 'asc'
                }
            }, {
                total: function() { return $scope.producers.total; },
                getData: function($defer, params) {
                    console.log(params);
                    var start = (params.page() - 1) * params.count();
                    console.log("Total producers: " + $scope.producers.total);
                    var end = Math.min($scope.producers.total, params.page() * params.count());
                    console.log("From " + start + " to " + end);
                    Rx.Observable.range(start, end).flatMap(producers.producer).toArray().subscribe($defer.resolve);
                }
            });
        }
    ])*/;

function Recipe() {
    this.forkedFrom = '';
    this.name = '';
    this.author = '';
    this.description = '';
    this.ingredients = [];
    this.directions = [];
    this.images = [];
}