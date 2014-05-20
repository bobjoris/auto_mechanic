'use strict';

angular.module('simulationApp')
  .factory('Holidays', function Holidays($resource) {
      return $resource('api/Holiday/:ID');
  });
