'use strict';

angular.module('simulationApp')
  .controller('MechanicCtrl', function ($scope, Mechanics, Franchises) {
    $scope.mechanic = new Mechanics();
    $scope.mechanics = Mechanics.query();

    $scope.franchises = Franchises.query();

    $scope.add = function (form) {
      $scope.mechanic.$save();
      $scope.mechanics.push($scope.mechanic);
      $scope.mechanic = new Mechanics();
    };
    $scope.edit = function (mechanic) {
      // TODO
    };
    $scope.delete = function (mechanic) {
      Mechanics.delete(mechanic, function(){
      $scope.cars = _.without($scope.mechanics, mechanic);
      });
    };
  });
