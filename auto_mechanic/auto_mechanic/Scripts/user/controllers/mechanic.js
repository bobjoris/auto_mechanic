'use strict';

angular.module('simulationApp')
  .controller('MechanicCtrl', function ($scope, Mechanics, Franchises, Holidays) {
    $scope.mechanic = new Mechanics();
    $scope.mechanics = Mechanics.query();

    $scope.franchises = Franchises.query();

    $scope.holiday = new Holidays();
    $scope.holidays = Holidays.query();

    $scope.add = function (form) {
      $scope.mechanic.$save();
      $scope.mechanics.push($scope.mechanic);
      $scope.mechanic = new Mechanics();
    };

    $scope.delete = function (mechanic) {
        mechanic['Mechanic_Service'] = null;
      Mechanics.delete(mechanic, function(){
      $scope.mechanics = _.without($scope.mechanics, mechanic);
      });
    };

    $scope.addH = function (form) {
        $scope.holiday.$save();
        $scope.holidays.push($scope.holiday);
        $scope.holiday = new Holidays();
    };

    $scope.deleteH = function (holiday) {
        Holidays.delete(holiday, function () {
            $scope.holidays = _.without($scope.holidays, holiday);
        });
    };


    $scope.show = function (event) {

        var elem = $(event.target).parent().find('ul');

        if (elem.css('display') == 'none')
            elem.css('display', 'block');
        else
            elem.css('display', 'none');
    };

    $scope.update = function (event) {
        var elem = $(event.target);

        var idMechanic = elem.data('idm');
        var idService = elem.data('ids');
        var val = elem.val();

        var data = idMechanic + ':' + idService + ':' + val;

        $.post('/api/mechanicservice/', { '': data }, function () { });
        console.log(data);
    };

  });
