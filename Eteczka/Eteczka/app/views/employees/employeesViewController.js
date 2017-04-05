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

        //$("#userModal").modal;

        var custName = 'Ola' + ' ' + 'Burqin';

        var modalOptions = {
            closeButtonText: 'Cancel',
            actionButtonText: 'Add Employee',
            headerText: 'Add ' + custName + '?',
            bodyText: 'Are you sure you want to add this employee?'
        };

        editEmployeeService.showModal({}, modalOptions).then(function (result) {
            console.log("in the function");
            /*dataService.deleteCustomer($scope.customer.id).then(function () {
                $location.path('/customers');*/
        }).catch(function () {
            console.log("error found!");
        });
    }

}]);