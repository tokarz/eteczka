'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'employeesService', 'modalService', function ($scope, $state, employeesService, modalService) {
    $scope.employees = [];
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

    $scope.getHired = function () {
        employeesService.getHired().then(function (result) {
            $scope.parameters.loading = false;
            $scope.parameters.employees = result.data;
        }, function (err) {
            $scope.parameters.loading = false;
            console.error(err);
        });
    };

    $scope.getAll = function () {
        employeesService.getAll().then(function (result) {
            $scope.parameters.loading = false;
            $scope.parameters.employees = result.Data.data;
        }, function (err) {
            $scope.parameters.loading = false;
            console.error(err);
        });
    };

    $scope.getRemaining = function () {
        employeesService.getRemaining().then(function (result) {
            $scope.parameters.loading = false;
            $scope.parameters.employees = result.data;
        }, function (err) {
            $scope.parameters.loading = false;
            console.error(err);
        });
    };
    $scope.clearedByTabChange = false;

    $scope.$watch('parameters.activeTab', function (val, oldVal) {
        if (val) {
            $scope.startProcessing();
            if ($scope.parameters.searchTerm.trim() !== '') {
                $scope.parameters.searchTerm = '';
                $scope.clearedByTabChange = true;
            }
            if (val.Id === 0) {
                $scope.getHired();
            } else if (val.Id === 1) {
                $scope.getAll();
            } else {
                $scope.getRemaining();
            }
        }
    });

    $scope.$watch('parameters.searchTerm', function (value) {
        if (value && value.trim() !== '' && value.trim().length > 1) {
            $scope.startProcessing();

            if ($scope.parameters.activeTab.Id === 0) {
                employeesService.searchHiredByText(value).then(function (result) {
                    $scope.parameters.loading = false;
                    $scope.parameters.employees = result.pracownicy;
                }, function (err) {
                    $scope.parameters.loading = false;
                    console.error(err);
                });
            } else if ($scope.parameters.activeTab.Id === 1) {
                employeesService.searchByText(value).then(function (result) {
                    $scope.parameters.loading = false;
                    $scope.parameters.employees = result.pracownicy;
                }, function (err) {
                    $scope.parameters.loading = false;
                    console.error(err);
                });
            } else {
                employeesService.searchRemainingByText(value).then(function (result) {
                    $scope.parameters.loading = false;
                    $scope.parameters.employees = result.pracownicy;
                }, function (err) {
                    $scope.parameters.loading = false;
                    console.error(err);
                });
            }

        } else {
            if (value === '') {
                if ($scope.clearedByTabChange) {
                    $scope.clearedByTabChange = false;
                } else {
                    var val = $scope.parameters.activeTab;
                    if (val.Id === 0) {
                        $scope.getHired();
                    } else if (val.Id === 1) {
                        $scope.getAll();
                    } else {
                        $scope.getRemaining();
                    }
                }
            }
        }

    });


}]);
