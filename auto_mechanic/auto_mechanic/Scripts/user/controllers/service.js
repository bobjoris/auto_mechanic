'use strict';

angular.module('simulationApp')
  .controller('ServiceCtrl', function ($scope, Services) {
      $scope.service = new Services();
      $scope.services = Services.query();

      $scope.isEdit = false;
      $scope.labelAdd = "Ajouter";
      $scope.previous = new Services();

      $scope.add = function (form) {
          if (!$scope.isEdit)
            $scope.service.label = $scope.service.Label + '#' + $scope.service.Duration;
          $scope.service.$save();
          if(!$scope.isEdit)
            $scope.services.push($scope.service);
          $scope.isEdit = false;
          $scope.cancel();
      };
      $scope.edit = function (service) {
          $scope.cancel();
          $scope.service = service;
          $scope.previous = angular.copy(service);
          $scope.isEdit = true;
          $scope.labelAdd = "Modfier";
      };
      $scope.delete = function (service) {
          Services.delete(service, function () {
              $scope.services = _.without($scope.services, service);
          });
      };
      $scope.cancel = function () {
          if ($scope.isEdit) {
              $scope.service.Label = $scope.previous.Label;
              $scope.service.KM = $scope.previous.KM;
          }
          $scope.service = new Services();
          $scope.labelAdd = "Ajouter";
          $scope.isEdit = false;
      };
  });
