'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', 'editEmployeeService', function ($scope, $state, utilsService, employeesService, editEmployeeService) {
    $scope.employees = [];

    employeesService.getAll().then(function (result) {
        $scope.employees = result.data;
    })

}]);
