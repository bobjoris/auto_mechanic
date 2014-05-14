'use strict';

angular
  .module('simulationApp', [
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngRoute'
  ])
  .config(function ($routeProvider) {
      $routeProvider
        .when('/cars', {
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
        .otherwise({
            redirectTo: '/cars'
        });
  });
