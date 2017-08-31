'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'employeesService', 'modalService', function ($scope, $state, employeesService, modalService) {
    $scope.employees = [];
    $scope.parameters = {
        user: {}
    };
    $scope.tabs = [
        { Name: 'Zatrudnieni' },
        { Name: 'Wszyscy' },
        { Name: 'Pozostali' }
    ];

    $scope.searchTerm = '';

    $scope.$watch('searchTerm', function (value) {
        if (value && value.length > 1) {
            employeesService.searchByText(value).then(function (result) {
                $scope.employees = result.pracownicy;
            });
        } else {
            employeesService.getAll().then(function (result) {
                $scope.employees = result.data;
            });
        }
    });

    employeesService.getAll().then(function (result) {
        $scope.employees = result.data;
    });

}]);
