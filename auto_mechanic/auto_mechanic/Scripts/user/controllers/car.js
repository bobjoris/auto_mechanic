'use strict';

angular.module('simulationApp')
  .controller('CarCtrl', function ($scope, Cars, Brands, ServiceBooks) {
    $scope.car = new Cars();
    $scope.cars = Cars.query();
    $scope.brands = Brands.query();
    $scope.servicebooks = ServiceBooks.query();
    
    $scope.add = function (form) {
    $scope.car.$save();
    $scope.cars.push($scope.car);
    $scope.car = new Cars();
  };
  $scope.edit = function (car) {
    // TODO
  };
  $scope.delete = function (car) {
    Cars.delete(car, function(){
    $scope.cars = _.without($scope.cars, car);
    });
  };

  });
