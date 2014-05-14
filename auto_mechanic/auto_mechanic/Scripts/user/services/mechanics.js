'use strict';

angular.module('simulationApp')
  .factory('Mechanics', function Mechanics($resource) {
    return $resource('api/Mechanic/:ID');
  });
