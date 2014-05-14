'use strict';

angular.module('simulationApp')
  .factory('ServiceBooks', function ServiceBooks($resource) {
      return $resource('api/ServiceBook/:ID');
  });