'use strict';

angular.module('simulationApp')
  .controller('BrandCtrl', function ($scope, Brands) {
    $scope.brand = new Brands();
    $scope.brands = Brands.query();
    $scope.isEdit = false;
    $scope.labelAdd = "Ajouter";
    $scope.previous = new Brands();

    $scope.add = function (form) {
        $scope.brand.$save();
        if(!$scope.isEdit)
            $scope.brands.push($scope.brand);
        $scope.isEdit = false;
        $scope.cancel();
    };
    $scope.edit = function (brand) {
        $scope.cancel();
        $scope.brand = brand;
        $scope.brand.Duration = 1;
        $scope.previous = angular.copy(brand);
        $scope.isEdit = true;
        $scope.labelAdd = "Modfier";
    };
    $scope.delete = function (brand) {
      Brands.delete(brand, function(){
      $scope.brands = _.without($scope.brands, brand);
      });
    };
    $scope.cancel = function () {
        if ($scope.isEdit)
            $scope.brand.Name = $scope.previous.Name;
        $scope.brand = new Brands();
        $scope.labelAdd = "Ajouter";
        $scope.isEdit = false;
    };
  });
