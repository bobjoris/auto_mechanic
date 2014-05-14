'use strict';

angular.module('simulationApp')
  .factory('Franchises', function Franchises($resource) {
      return $resource('api/Franchise/:ID');
  });
