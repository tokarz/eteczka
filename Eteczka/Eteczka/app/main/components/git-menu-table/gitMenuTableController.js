'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$scope', 'filesViewService', function ($scope, filesViewService) {
    $scope.newrows = [];
    $scope.stagedrows = [];
    $scope.loading = false;

    $scope.$watch('company', function (value) {
        if (value) {
            $scope.loading = true;
            filesViewService.getGitStateForCompany(value).then(function (result) {
                $scope.newrows = result.newfiles;
                $scope.stagedrows = [];
                $scope.loading = false;
            });
        }
    });

    $scope.selectStagedFile = function (file) {
        if ($scope.selectedStagedFile == file) {
            $scope.selectedStagedFile = {};
        } else {
            $scope.selectedStagedFile = file;
        }
    }
    $scope.getStagedRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedStagedFile) {
            result += ' active-row';
        }

        return result;
    }
    $scope.selectFile = function (file) {
        if ($scope.selectedfile == file) {
            $scope.selectedfile = {};
        } else {
            $scope.selectedfile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'git-table-row';

        if (file === $scope.selectedfile) {
            result += ' active-row';
        }

        return result;
    }

    $scope.stageFile = function (row) {
        if (row && $scope.selectedfile !== {}) {
            $scope.newrows.splice($scope.newrows.indexOf(row), 1);
            $scope.stagedrows.push(row);
            $scope.selectedStagedFile = row;
        }
    }

    $scope.unstageFile = function (row) {
        if (row && $scope.selectedStagedFile !== {}) {
            $scope.stagedrows.splice($scope.stagedrows.indexOf(row), 1);
            $scope.newrows.push(row);
            $scope.selectedfile = row;
        }
    }

}]);