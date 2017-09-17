'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$scope', 'filesViewService', function ($scope, filesViewService) {
    $scope.selectedFile = null;

    $scope.userFiles = [];

    $scope.selectFile = function (file) {
        if ($scope.selectedFile === file) {
            $scope.selectedFile = null;
            $scope.userFiles = [];
        } else {
            $scope.selectedFile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedFile) {
            result += ' active-row';
        }

        return result;
    }

    $scope.$watch('user', function (user) {
        if (user) {
            filesViewService.getFilesForUser(user).then(function (result) {
                $scope.userFiles = result.pliki;
            });
        }
    });
}]);