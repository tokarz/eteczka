'use strict';
angular.module('et.controllers').controller('gitMenuTableController', ['$scope', 'filesViewService', function ($scope, filesViewService) {
    $scope.newrows = [];
    $scope.stagedrows = [];
    $scope.loading = false;
    $scope.filetopreview = null;

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
        if ($scope.selectedstagedfile == file) {
            $scope.selectedstagedfile = null;
            $scope.selectedfile = null;
            $scope.filetopreview = null;
        } else {
            $scope.selectedstagedfile = file;
            $scope.filetopreview = file;
            $scope.selectedfile = null;
        }
    }
    $scope.getStagedRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedstagedfile) {
            result += ' active-row';
        }

        return result;
    }
    $scope.selectFile = function (file) {
        if ($scope.selectedfile == file) {
            $scope.selectedfile = null;
            $scope.filetopreview = null;
            $scope.selectedstagedfile = null;
        } else {
            $scope.selectedfile = file;
            $scope.filetopreview = file;
            $scope.selectedstagedfile = null;
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
        if (row && $scope.selectedfile !== null) {
            $scope.newrows.splice($scope.newrows.indexOf(row), 1);
            $scope.stagedrows.push(row);
            $scope.selectedfile = null;
            $scope.selectedstagedfile = row;
            $scope.filetopreview = row;
        }
    }

    $scope.unstageFile = function (row) {
        if (row && $scope.selectedstagedfile !== null) {
            $scope.stagedrows.splice($scope.stagedrows.indexOf(row), 1);
            $scope.newrows.push(row);
            $scope.selectedstagedfile = null;
            $scope.selectedfile = row;
            $scope.filetopreview = row;
        }
    }

}]);