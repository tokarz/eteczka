'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', 'editEmployeeService', function ($scope, $state, utilsService, employeesService, editEmployeeService) {
    $scope.users = [];
    $scope.selectedUser = null;
    $scope.isTableLoaded = false;
    $scope.isUserSet = function () {
        return ($scope.selectedUser != null)
    }

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.setUser = function () {
        console.log('clicked on a row', this);
        $scope.selectedUser = this.user;
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

    employeesService.getAll().then(function (result) {
        $scope.users = result.data.data;
        $scope.isTableLoaded = true;
    })

}]);