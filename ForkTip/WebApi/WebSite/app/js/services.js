'use strict';

/* Services */
angular.module('app.services', ['rx', 'restangular']).
    value('version', '0.1').
    service('pw', ['$window', function ($window) { return new PasswordService($window); }]).
    service('recipe', ['Restangular', 'pw', function (Restangular, pw) { return new RecipeService(Restangular, pw); }]);

function RecipeService(Restangular, pw) {
    var self = this;
    MixinCrudService(Restangular, pw, self, 'recipe');
    self.fork = function (id) {
        var base = self.base;
        return Rx.Observable.fromPromise(Restangular.one(base, id).customPOST('', '', { newPassword: pw.getPassword() }));
    };
}

function PasswordService(window) {
    var self = this;

    self.password = '';

    self.setPassword = function (newPassword) {
        window.localStorage.setItem('access_token', newPassword);
        self.password = newPassword;
        console.log('access_token: ' + self.password);
    };

    self.getPassword = function () {
        return window.localStorage.getItem('access_token');
    };

    self.generate = function() {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 16; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    };

    self.setPassword(self.getPassword() || self.generate());
}

function MixinCrudService(Restangular, pw, target, kind) {
    target.base = 'api/v1/' + kind;

    /*target[kind + 's'] = lazy(Restangular.one(base).get);*/

    target[kind] = function(id) {
        if (id) {
            return lazy(function() { return Restangular.one(target.base, id).get(); });
        }

        return Rx.Observable.empty();
    };
    
    target.add = function() {
        return Rx.Observable.fromPromise(Restangular.one(target.base).customPOST('', '', { password: pw.getPassword() }));
    };

    target.set = propertySetterMaker(Restangular, target.base, pw);
}

function propertySetterMaker(Restangular, base, pw) {
    return function (property) {
        return function (id, value) {
            return Rx.Observable.fromPromise(Restangular.one(base, id).one(property).customPUT(JSON.stringify(value), '', { password: pw.getPassword() }));
        };
    }
}

function lazy(getPromise) {
    return Rx.Observable.defer(function () {
        return Rx.Observable.fromPromise(getPromise());
    }).replay(function (x) { return x; }, 1);
}