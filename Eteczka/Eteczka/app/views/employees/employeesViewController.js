'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', function ($scope, $state, utilsService, employeesService) {
    $scope.users = [];

    employeesService.getAll().then(function (result) {
        $scope.users = result.data.data;
    })

    $scope.goBack = function () {
        $state.go('options');
    }

}]);