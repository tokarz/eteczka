'use strict';
angular.module('et.controllers').controller('settingsFilesController', ['$scope', 'settingsService', 'companiesService', function ($scope, settingsService, companiesService) {
    $scope.folders = [];

    $scope.createSourceFolder = function (name) {
        if (!$scope.existingFolders[name]) {
            settingsService.createSourceFolder(name).then(function (result) {
                if (result.success) {
                    $scope.checkButtonsState([name]);
                }
            });
        }
    }

    $scope.importAllCompanies = function () {
        companiesService.getAll().then(function (result) {
            $scope.folders = result.Firmy;
            $scope.checkButtonsState($scope.folders);
        });
    }

    $scope.doesFolderExist = function (folder) {
        var result = 'button-error';
        if ($scope.existingFolders[folder]) {
            var result = 'button-success';
        }

        return result;
    }

    $scope.importAllCompanies();
}]);