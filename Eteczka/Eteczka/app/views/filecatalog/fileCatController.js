'use strict';
angular.module('et.controllers').controller('fileCatController', ['$scope', '$q', 'fileCatService', function ($scope, $q, fileCatService) {
    $scope.areasFilters = [];
    $scope.assignArea = function () {
    }
    $scope.selectedArea = '';

    $scope.departmentFilters = [];

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

    $scope.documentTypeFilters = [];
    $scope.selectedType = '';
    $scope.assignType = function () {
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

                                    $scope.selectedUser = '';

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

                                $scope.selectedUser = '';

                                deferred.resolve(true);
                            });
                        });
                    }
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
        fileCatService.getValuesForFilters($scope.selectedArea, $scope.selectedDepartment, $scope.selectedSubDepartment, $scope.selectedAccount5, $scope.selectedType, $scope.selectedUser).then(function (result) {
            $scope.rows = result.pliki;
        });
    }

    $scope.init().then(function () {
        $scope.rows = [];
        //$scope.reloadTableContents();
    });

}]);