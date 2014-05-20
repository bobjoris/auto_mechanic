'use strict';

angular.module('simulationApp')
  .factory('Simulations', function Simulation($resource) {
      return $resource('api/Simulation/:ID');
  });