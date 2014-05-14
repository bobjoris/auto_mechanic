'use strict';

angular.module('simulationApp')
  .factory('Services', function Services($resource) {
      return $resource('api/Service/:ID');
  });