﻿'use strict';
angular.module('et.controllers').controller('settingsViewController', ['$scope', 'settingsService', 'companiesService', 'sessionService', function ($scope, settingsService, companiesService, sessionService) {
    $scope.folders = [];

    $scope.importFiles = function () {
        settingsService.importFiles(true).then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.existingFolders = {};

    $scope.importAllCompanies = function () {
        companiesService.getAll().then(function (result) {
            $scope.folders = result.Firmy;
            $scope.checkButtonsState($scope.folders);
        });
    }

    $scope.checkButtonsState = function (folders) {
        angular.forEach(folders, function (folder) {
            settingsService.doesFolderExist(folder.Firma).then(function (result) {
                $scope.existingFolders[folder.Firma] = result.success;
            });
        });
    };

    $scope.importArchives = function () {
        settingsService.importArchives().then(function () {
            $scope.checkUpdateStatus('archives');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importUsers = function () {
        settingsService.importUsers().then(function () {
            $scope.checkUpdateStatus('users');

        },
        function () {
            alert('failed!');
        });
    }

    $scope.importFirms = function () {
        settingsService.importFirms().then(function () {
            $scope.checkUpdateStatus('firms');

        },
        function () {
            alert('failed!');
        });
    }

    $scope.importAreas = function () {
        settingsService.importAreas().then(function () {
            $scope.checkUpdateStatus('areas');

        },
        function () {
            alert('failed!');
        });
    }

    $scope.importWorkplaces = function () {
        settingsService.importWorkplaces().then(function () {
            $scope.checkUpdateStatus('workplaces');

        },
        function () {
            alert('failed!');
        });
    }

    $scope.importSubdepartments = function () {
        settingsService.importSubdepartments().then(function () {
            $scope.checkUpdateStatus('subdepartment');

        },
        function () {
            alert('failed!');
        });
    }
    $scope.importDepartments = function () {
        settingsService.importDepartments().then(function () {
            $scope.checkUpdateStatus('department');

        },
        function () {
            alert('failed!');
        });
    }
    $scope.importAccount5 = function () {
        settingsService.importAccount5().then(function () {
            $scope.checkUpdateStatus('account5');
        },
        function () {
            alert('failed!');
        });
    }


    $scope.updateStatus = {
        users: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        firms: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        archives: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        areas: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        workplaces: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        subdepartment: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        department: {
            success: false,
            countJson: 0,
            countDb: 0
        },
        account5: {
            success: false,
            countJson: 0,
            countDb: 0
        }
    }

    $scope.checkButtonClass = function (type) {
        var result = 'button-success';
        if (!$scope.updateStatus[type].success) {
            result = 'button-error';
        }

        return result;
    }

    $scope.doesFolderExist = function (folder) {
        var result = 'button-error';
        if ($scope.existingFolders[folder]) {
            var result = 'button-success';
        }

        return result;
    }

    $scope.checkUpdateStatus = function (type) {
        settingsService.checkUpdateStatus(type).then(function (result) {
            $scope.updateStatus[type] = {
                success: result.success,
                countJson: result.importJson,
                countDb: result.importDb
            }
        });
    }

    $scope.killSession = function (session) {
        sessionService.killGivenSession(session.IdSesji).then(function (result) {
            $scope.fetchAllSessions();
        });
    }

    $scope.openSessions = [];
    $scope.fetchAllSessions = function () {
        settingsService.fetchAllOpenSessions().then(function (res) {
            $scope.openSessions = res.sesje;
        });
    }

    $scope.checkUpdateStatus('users');
    $scope.checkUpdateStatus('firms');
    $scope.checkUpdateStatus('archives');
    $scope.checkUpdateStatus('areas');
    $scope.checkUpdateStatus('workplaces');
    $scope.checkUpdateStatus('subdepartment');
    $scope.checkUpdateStatus('department');
    $scope.checkUpdateStatus('account5');
    $scope.importAllCompanies();
    $scope.fetchAllSessions();

    $scope.createSourceFolder = function (name) {
        if (!$scope.existingFolders[name]) {
            settingsService.createSourceFolder(name).then(function (result) {
                if (result.success) {
                    $scope.checkButtonsState([name]);
                }
            });
        }
    }
}]);