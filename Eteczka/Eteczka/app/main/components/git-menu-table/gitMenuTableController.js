'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$scope', 'filesViewService', function ($scope, filesViewService) {
    $scope.newrows = [];
    $scope.stagedrows = [];

    $scope.$watch('company', function (value) {
        if (value) {
            filesViewService.getGitStateForCompany(value).then(function () {

            });
        }

    });

}]);