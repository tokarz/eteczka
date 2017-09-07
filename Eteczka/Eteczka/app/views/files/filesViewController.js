'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'httpService', 'filesViewService', 'editEmployeeService', function ($scope, $state, httpService, filesViewService, editEmployeeService) {
    $scope.parameters = {
        newrows: {},
        stagedrows: $scope.tabs,
        loading: false
    };


}]);