'use strict';

angular
  .module('simulationApp', [
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngRoute',
    'checklist-model'
  ])
.filter('formatDate', function () {
    return function (text) {
        var s = text.split('-');

        return s[2].slice(0,2) + '-' + s[1] + '-' + s[0];
    };
})
  .config(function ($routeProvider) {
      $routeProvider
        .when('/cars', {
            templateUrl: 'back/cars',
            controller: 'MainCtrl'
        })
        .when('/cars/:id', {
            templateUrl: 'back/cars',
            controller: 'MainCtrl'
        })
        .when('/brands', {
            templateUrl: 'back/brands',
            controller: 'MainCtrl'
        })
        .when('/mechanics', {
            templateUrl: 'back/mechanics',
            controller: 'MainCtrl'
        })
        .when('/franchises', {
            templateUrl: 'back/franchises',
            controller: 'MainCtrl'
        })
        .when('/services', {
            templateUrl: 'back/services',
            controller: 'MainCtrl'
        })
        .when('/servicebooks', {
            templateUrl: 'back/servicebooks',
            controller: 'MainCtrl'
        })
        .when('/simuconf', {
            templateUrl: 'home/config',
            controller: 'MainCtrl'
        })
        .otherwise({
            redirectTo: '/cars'
        });
  });
