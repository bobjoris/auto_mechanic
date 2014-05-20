'use strict';

angular.module('simulationApp')
  .controller('ConfigCtrl', function ($scope, $sce, Cars, Brands, Mechanics, Simulations) {
      $scope.cars = Cars.query();
      $scope.brands = Brands.query();
      $scope.mechanics = Mechanics.query();

      $scope.simulation = '';
      $scope.simulationIter = '';
      $scope.drive = '';
      $scope.planning = '';
      $scope.init = '';

      $scope.iter = 0;

      $scope.simulations = Simulations.query();

      $scope.setSimu = function () {
          var strConfig = $scope.GetParam();

          console.log(strConfig);

          $.post('/api/simulation', { '': strConfig }, function (data) {
              $scope.simulation = angular.fromJson(data);
              $scope.simulations.push($scope.simulation);
              $scope.$apply();
              $scope.showSimulation($scope.simulation);
          })
      };

      $scope.showSimulation = function (simulation) {
          $scope.simulation = simulation;
          $scope.iter = 0;
          $scope.init = $sce.trustAsHtml($scope.jsonEscape(simulation['Init']));
          $scope.fillIter(simulation['SimIterJeu'][$scope.iter]);

          $('html, body').animate({
              scrollTop: $("#result").offset().top
          }, 500);
      }

      $scope.nextIter = function () {
          $scope.iter += 1;
          $scope.fillIter($scope.simulation['SimIterJeu'][$scope.iter]);
      }

      $scope.prevIter = function () {
          $scope.iter -= 1;
          if ($scope.iter < 0)
              $scope.iter = 0;
          $scope.fillIter($scope.simulation['SimIterJeu'][$scope.iter]);
      }

      $scope.fillIter = function (simuIter) {
          $scope.simulationIter = simuIter;
          $scope.planning = $sce.trustAsHtml($scope.jsonEscape(simuIter['Planning']));
          $scope.drive = $sce.trustAsHtml($scope.jsonEscape(simuIter['Drive']));
      }

      $scope.jsonEscape = function(str) {
          return str.replace(/\n/g, "<br />").replace(/\r/g, "").replace(/\t/g, "&nbsp;");
      }

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
