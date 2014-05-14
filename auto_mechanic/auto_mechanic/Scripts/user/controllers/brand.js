'use strict';

angular.module('simulationApp')
  .controller('BrandCtrl', function ($scope, Brands) {
    $scope.brand = new Brands();
    $scope.brands = Brands.query();

    $scope.add = function (form) {
      $scope.brand.$save();
      $scope.brands.push($scope.brand);
      $scope.brand = new Brands();
    };
    $scope.edit = function (brand) {
      //TODO
    };
    $scope.delete = function (brand) {
      Brands.delete(brand, function(){
      $scope.brands = _.without($scope.brands, brand);
      });
    };
  });
