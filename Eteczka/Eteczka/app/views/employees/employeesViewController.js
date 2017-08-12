'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', 'editEmployeeService', function ($scope, $state, utilsService, employeesService, editEmployeeService) {
    $scope.users = [];
    $scope.selectedUser = null;

    $scope.isUserSet = function () {
        return ($scope.selectedUser != null)
    }

    employeesService.getAll().then(function (result) {
        $scope.users = result.data.data;
    })

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.setUser = function () {
        if($scope.selectedUser == this.user) {
            console.log('clicked on the same row', this);
            $scope.selectedUser = null
        }
        else {
            console.log('clicked on a new row', this);
            $scope.selectedUser = this.user;
        }
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

    $scope.triggerEditEmployeePopup = function () {
        var modalOptions = {
            title: 'Edytowanie istniejacego pracownika',
            body: 'app/views/employees/editEmployeesPopup/newUserTemplate.html'
        }

        editEmployeeService.showModal(modalOptions, $scope.selectedUser).then(function (result) {
            console.log(result)
        }).catch(function (error) {
            console.log("error found!");
        });
    }

    $scope.triggerDeleteEmployeePopup = function () {
        var modalOptions = {
            title: 'Usuwanie pracownika z bazy danych',
            body: 'app/views/employees/editEmployeesPopup/deleteUserTemplate.html'
        }

        editEmployeeService.showModal(modalOptions, $scope.selectedUser).then(function (result) {
            console.log('result')
            console.log(result)
        }).catch(function (error) {
            console.log("error found!");
        });
    }

}]);
