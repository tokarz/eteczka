'use strict';
angular.module('et.controllers').controller('fileCatController', ['$scope', '$q', 'fileCatService', function ($scope, $q, fileCatService) {
    $scope.areasFilters = [];
    $scope.assignArea = function () {
    }
    $scope.selectedArea = '';

    $scope.departmentFilters = [];

    $scope.dateRange = {
        DateFrom: '',
        DateType: '',
        DateTo: ''
    }

    $scope.assignDepartment = function () {
        if ($scope.selectedDepartment && $scope.selectedDepartment.Wydzial) {
            fileCatService.getSubDepartments($scope.selectedDepartment.Wydzial.trim()).then(function (res) {
                $scope.subDepartmentsFilters = res.PodWydzialy;
                $scope.selectedSubDepartment = $scope.subDepartmentsFilters[0];
            });
        }
    }
    $scope.selectedDepartment = '';

    $scope.subDepartmentsFilters = [];
    $scope.assignSubDepartment = function () {
    }
    $scope.selectedSubDepartment = '';

    $scope.account5Filters = [];
    $scope.assignAccount5 = function () {
    }
    $scope.selectedAccount5 = '';
    $scope.employeeFilters = [];
    $scope.selectedEmployee = '';

    $scope.documentTypeFilters = [];
    $scope.selectedType = '';
    $scope.assignType = function () {
    }
    $scope.assignDateType = function () {
    }
    $scope.assignDateFrom = function () {
    }
    $scope.assignDateTo = function () {
    }
    $scope.assignEmployee = function () {
    }

    var createFilterFor = function (keys, query) {
        var lowercaseQuery = angular.lowercase(query);

        return function filterFn(object) {
            return keys.some(function (key) {
                return (object[key].toLowerCase().indexOf(lowercaseQuery) === 0);
            })
        };
    }

    $scope.employeeSearch = function (name) {
        return name ? $scope.employeeFilters.filter(createFilterFor(["Nazwisko"], name)) : $scope.employeeFilters;
    }
    
    $scope.init = function () {
        var deferred = $q.defer();

        fileCatService.getDocumentTypes().then(function (res) {
            $scope.documentTypeFilters = res.PobraneDokumenty;
            //$scope.selectedType = $scope.documentTypeFilters[0];

            fileCatService.getRegions().then(function (res) {
                $scope.areasFilters = res.Rejony;
                //$scope.selectedArea = $scope.areasFilters[0];

                fileCatService.getDepartments().then(function (res) {
                    $scope.departmentFilters = res.Wydzialy;
                    //$scope.selectedDepartment = $scope.departmentFilters[0];

                    fileCatService.getEmployees().then(function (res) {
                        $scope.employeeFilters = res.Data.data
                        $scope.selectedEmployee = '';

                        if ($scope.selectedDepartment && $scope.selectedDepartment.Wydzial) {
                            fileCatService.getSubDepartments($scope.selectedDepartment.Wydzial.trim()).then(function (res) {
                                $scope.subDepartmentsFilters = res.PodWydzialy;
                                //$scope.selectedSubDepartment = $scope.subDepartmentsFilters[0];

                                fileCatService.getSubDepartments().then(function (res) {
                                    $scope.subDepartmentsFilters = res.PodWydzialy;
                                    //$scope.selectedSubDepartment = $scope.subDepartmentsFilters[0];

                                    fileCatService.getAccounts5().then(function (res) {
                                        $scope.account5Filters = res.pobraneKonta5;
                                        //$scope.selectedAccount5 = $scope.account5Filters[0];

                                        deferred.resolve(true);
                                    });
                                });
                            });
                        } else {
                            fileCatService.getSubDepartments().then(function (res) {
                                $scope.subDepartmentsFilters = res.PodWydzialy;
                                //$scope.selectedSubDepartment = $scope.subDepartmentsFilters[0];

                                fileCatService.getAccounts5().then(function (res) {
                                    $scope.account5Filters = res.pobraneKonta5;
                                    //$scope.selectedAccount5 = $scope.account5Filters[0];

                                    deferred.resolve(true);
                                });
                            });
                        }
                    });
                });
            });
        });

        return deferred.promise;
    }

    $scope.clearAllFilters = function () {
        $scope.init().then(function () {
            $scope.rows = [];
        });
    }

    $scope.reloadTableContents = function () {
        $scope.rows = [];
        fileCatService.getValuesForFilters($scope.selectedArea, $scope.selectedDepartment, $scope.selectedSubDepartment, $scope.selectedAccount5, $scope.selectedType, $scope.selectedEmployee, $scope.dateRange).then(function (result) {
            $scope.rows = result.pliki;
        });
    }

    $scope.init().then(function () {
        $scope.rows = [];
        //$scope.reloadTableContents();
    });

}]);