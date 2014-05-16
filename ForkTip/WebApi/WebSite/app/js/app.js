'use strict';

// Declare app level module which depends on filters, and services
angular.module('app', [
  'ngRoute',
  'ui.bootstrap',
  'app.filters',
  'app.services',
  'app.directives',
  'app.controllers'
]).
config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/recipe', { templateUrl: 'app/partials/recipeEdit.html', controller: 'RecipeCtrl' });
    $routeProvider.when('/r', { templateUrl: 'app/partials/recipeView.html', controller: 'RecipeCtrl' });
    $routeProvider.when('/welcome', { templateUrl: 'app/partials/welcome.html', controller: 'RecipeCtrl' });
    $routeProvider.otherwise({ redirectTo: '/recipe' });
}]).run(function (editableOptions) {
    editableOptions.theme = 'bs3';
});