'use strict';

angular.module('simulationApp')
  .controller('ConfigCtrl', function ($scope, Cars, Brands, Franchises) {
      $scope.cars = Cars.query();
      $scope.brands = Brands.query();
      $scope.franchises = Franchises.query();

      $scope.add = function (form) {
      };
  });
