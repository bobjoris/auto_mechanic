'use strict';

angular.module('simulationApp')
  .controller('ServiceBookCtrl', function ($scope, ServiceBooks, Services) {
      $scope.servicebook = new ServiceBooks();
      $scope.servicebooks = ServiceBooks.query();
      $scope.services = Services.query();

      $scope.add = function (form) {
          $scope.servicebook.$save();
          $scope.servicebooks.push($scope.servicebook);
          $scope.servicebook = new ServiceBooks();
      };
      $scope.edit = function (servicebook) {
          //TODO
      };
      $scope.delete = function (servicebook) {
          ServiceBooks.delete(servicebook, function () {
              $scope.servicebooks = _.without($scope.servicebooks, servicebook);
          });
      };
  });
