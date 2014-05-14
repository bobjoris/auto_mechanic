'use strict';

angular.module('simulationApp')
  .factory('Cars', function Cars($resource) {
    return $resource('api/Car/:ID');
  });
