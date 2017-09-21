'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', 'companiesService', function ($scope, companiesService) {
    $scope.parameters = {
        company: '',
    };

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });

}]);