'use strict';

angular.module('simulationApp')
  .factory('Brands', function Brands ($resource) {
    return $resource('api/brand/:ID');
  });
