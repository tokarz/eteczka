'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'employeesService', 'modalService', function ($scope, $state, employeesService, modalService) {
    $scope.employees = [];

    employeesService.getAll().then(function (result) {
        $scope.employees = result.data;
    })

}]);
