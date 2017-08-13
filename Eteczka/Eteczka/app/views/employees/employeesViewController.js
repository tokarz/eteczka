'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', 'employeesService', 'editEmployeeService', function ($scope, $state, utilsService, employeesService, editEmployeeService) {
    $scope.users = [];
    $scope.filesForUser = [];
    $scope.elementSelected = null;
    $scope.isTableLoaded = false;
    $scope.isUserSet = function () {
        return ($scope.elementSelected != null)
    }

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.setUser = function (user) {
        if ($scope.elementSelected && user.Id === $scope.elementSelected.Id) {
            $scope.elementSelected = null;
            $scope.isEmpTableLoaded = true;
        } else {
            $scope.isEmpTableLoaded = false;
            employeesService.getFilesForEmployee(user.Pesel).then(function (result) {
                $scope.filesForUser = result.pliki;
                $scope.elementSelected = user;
                $scope.isEmpTableLoaded = true;
            });
        }
    }

    $scope.isElementSelected = function (option) {
        if ($scope.isUserSet()) {
            return 'employeePanelSelected';
        } else {
            if (option === 'main') {
                return 'employeeTopNotSelected';
            } else {
                return 'employeeDetailNotSelected';
            }
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

    employeesService.getAll().then(function (result) {
        $scope.users = result.data;
        $scope.isTableLoaded = true;
    })

}]);
