'use strict';
angular.module('et.controllers').controller('employeesFilesController', ['$scope', function ($scope) {
    $scope.tabs = [
        { Id: 0, Name: 'Zatrudnieni' },
        { Id: 1, Name: 'Wszyscy' },
        { Id: 2, Name: 'Pozostali' }
    ];

    $scope.parameters = {
        user: {},
        tabs: $scope.tabs,
        activeTab: $scope.tabs[0],
        searchTerm: '',
        employees: [],
        loading: false
    };

    $scope.startProcessing = function () {
        $scope.parameters.loading = true;
        $scope.parameters.employees = [];
    }

}]);