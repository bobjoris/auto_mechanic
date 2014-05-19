'use strict';

angular.module('simulationApp')
  .controller('FranchiseCtrl', function ($scope, Franchises) {
      $scope.franchise = new Franchises();
      $scope.franchises = Franchises.query();
      $scope.isEdit = false;
      $scope.labelAdd = "Ajouter";
      $scope.previous = new Franchises();

      $scope.add = function (form) {
          console.log($scope.franchise.ID);
          $scope.franchise.$save();

          if (!$scope.isEdit)
              $scope.franchises.push($scope.franchise);

          $scope.isEdit = false;
          $scope.cancel();
      };
      $scope.edit = function (franchise) {
          $scope.cancel();
          $scope.franchise = franchise;
          $scope.previous = angular.copy(franchise);
          console.log($scope.previous);
          $scope.isEdit = true;
          $scope.labelAdd = "Modfier";
      };
      $scope.delete = function (franchise) {
          Franchises.delete(franchise, function () {
              $scope.franchises = _.without($scope.franchises, franchise);
          });
      };
      $scope.cancel = function () {
          if ($scope.isEdit)
              $scope.franchise.Name = $scope.previous.Name;
          $scope.franchise = new Franchises();
          $scope.labelAdd = "Ajouter";
          $scope.isEdit = false;
      };
  });
