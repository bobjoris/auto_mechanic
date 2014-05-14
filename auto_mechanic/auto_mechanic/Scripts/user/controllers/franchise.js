'use strict';

angular.module('simulationApp')
  .controller('FranchiseCtrl', function ($scope, Franchises) {
    $scope.franchise = new Franchises();
    $scope.franchises = Franchises.query();

    $scope.add = function (form) {
      $scope.franchise.$save();
      $scope.franchises.push($scope.franchise);
      $scope.franchise = new Franchises();
    };
    $scope.edit = function (franchise) {
      //TODO
    };
    $scope.delete = function (franchise) {
      Franchises.delete(franchise, function(){
      $scope.franchises = _.without($scope.franchises, franchise);
      });
    };
  });
