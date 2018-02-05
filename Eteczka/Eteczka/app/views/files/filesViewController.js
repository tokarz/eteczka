'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'companiesService', 'filesViewService', 'modalService', 'sessionService', function ($scope, $state, companiesService, filesViewService, modalService, sessionService) {
    $scope.parameters = {
        company: '',
    };

    //$scope.selectedStagedFile = null;
    //$scope.createdMetaData = null;
    //$scope.fileTypes = []
    //$scope.employees = []
    

    //var loadFileTypes = function () {
    //    filesViewService.getFileTypes().then(function (fileTypes) {
    //        $scope.fileTypes = fileTypes.PobraneDokumenty
    //    })
    //}
    //var loadEmployees = function () {
    //    filesViewService.getAllEmployees().then(function (employees) {
    //        $scope.employees = employees.Data.data
    //    })
    //}

    //loadFileTypes();
    //loadEmployees()

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });

}]);