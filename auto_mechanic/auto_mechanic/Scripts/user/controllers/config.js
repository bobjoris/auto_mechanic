'use strict';

angular.module('simulationApp')
  .controller('ConfigCtrl', function ($scope, Cars, Brands, Mechanics) {
      $scope.cars = Cars.query();
      $scope.brands = Brands.query();
      $scope.mechanics = Mechanics.query();

      $scope.setSimu = function () {
          var strConfig = $scope.GetParam();

          console.log(strConfig);
      };

      $scope.GetParam = function () {
          // Format : Date,durée;id_marque:count,;id_model:count,..;idgaragiste
          var res = '';

          // Date
          res += $('#inputBeginDate').val() + ',';
          res += $('#inputDuration').val() + ';';

          // Marque
          $('.countBrand').each(function () {
              res += $(this).data('id') + ':' + $(this).val() + ',';
          });
          res = res.slice(0, -1);
          res += ';';

          // Modele
          $('.countCar').each(function () {
              res += $(this).data('id') + ':' + $(this).val() + ',';
          });
          res = res.slice(0, -1);
          res += ';';

          // Garagiste
          $('.chxMechanic').each(function () {
              if ($(this).is(':checked'))
                  res += $(this).data('idm') + ',';
          });

          res = res.slice(0, -1);

          return res;
      };
  });
