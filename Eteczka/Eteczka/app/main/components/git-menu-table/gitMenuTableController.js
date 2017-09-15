'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$scope', 'filesViewService', function ($scope, filesViewService) {
    $scope.newrows = [];
    $scope.stagedrows = [];
    $scope.loading = false;

    $scope.$watch('company', function (value) {
        if (value) {
            $scope.loading = true;
            filesViewService.getGitStateForCompany(value).then(function (result) {
                $scope.stagedrows = result.staged;
                $scope.newrows = result.newfiles;
                $scope.loading = false;
            });
        }
    });

    $scope.selectFile = function (file) {
        if ($scope.selectedfile == file) {
            $scope.selectedfile = {};
        } else {
            $scope.selectedfile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedfile) {
            result += ' active-row';
        }

        return result;
    }

}]);