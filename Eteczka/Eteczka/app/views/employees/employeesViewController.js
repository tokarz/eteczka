'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', 'editEmployeeService', function ($scope, $state, utilsService, employeesService, editEmployeeService) {
    $scope.users = [];

    employeesService.getAll().then(function (result) {
        $scope.users = result.data.data;
    })

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.triggerAddEmployeePopup = function () {

        var modalOptions = {
            title: 'Dodawanie nowego pracownika',
            body: 'app/views/employees/editEmployeesPopup/newUserTemplate.html'
        }

        editEmployeeService.showModal(modalOptions).then(function (result) {
            console.log(result)
        }).catch(function (error) {
            console.log("error found!");
        });
    }

}]);