'use strict';

angular.module('simulationApp')
  .controller('MainCtrl', function ($scope, $location) {
    $scope.isActive = function (arr) {
      return _.reduce(arr, function(memo, item){
        return memo || (item == $location.path());
      }, false);
    };
  });
