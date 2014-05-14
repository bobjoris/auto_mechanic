'use strict';

angular.module('simulationApp')
  .controller('ServiceCtrl', function ($scope, Services) {
      $scope.service = new Services();
      $scope.services = Services.query();

      $scope.add = function (form) {
          $scope.service.$save();
          $scope.services.push($scope.service);
          $scope.service = new Services();
      };
      $scope.edit = function (service) {
          //TODO
      };
      $scope.delete = function (service) {
          Services.delete(service, function () {
              $scope.services = _.without($scope.services, service);
          });
      };
  });
