'use strict';

angular
  .module('simulationApp', [
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngRoute',
    'checklist-model'
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
