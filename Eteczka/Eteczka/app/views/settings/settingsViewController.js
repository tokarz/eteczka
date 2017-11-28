'use strict';
angular.module('et.controllers').controller('settingsViewController', ['$scope', '$state', 'settingsService', 'companiesService', 'sessionService', 'modalService', 'usersService', function ($scope, $state, settingsService, companiesService, sessionService, modalService, usersService) {
    $scope.folders = [];
    $scope.users = [];
    $scope.existingFolders = {};

    $scope.importAllUsers = function () {
        settingsService.getAllUserAccounts().then(function (result) {
            $scope.users = result.users;
        }, function (err) {
            modalService.alert('Import Pracownikow', 'Blad! Sprawdz Logi Systemowe!');
            console.error(err);
        });
    }

    $scope.importAllCompanies = function () {
        companiesService.getAll().then(function (result) {
            $scope.folders = result.Firmy;
            $scope.checkButtonsState($scope.folders);
        });
    }

    $scope.removeCompanyFromUser = function (user, firma) {
        usersService.removeCompanyFromUser(user, firma).then(function (res) {
            $state.reload();
        }, function (err) {
            modalService.alert('Usuwanie firmy dla pracownika', 'Blad! Sprawdz Logi Systemowe!');
            console.error(err);
        });
    }

    $scope.changePassword = function (user) {

    }

    $scope.markAsDeleted = function (user) {

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
        function (err) {
            modalService.alert('Import Lokalizacji Archiwow', 'Blad! Sprawdz Logi Systemowe!');
            console.error(err);
        });
    }

    $scope.importUsers = function () {
        settingsService.importUsers().then(function () {
            $scope.checkUpdateStatus('users');

        },
        function () {
            modalService.alert('Import Pracownikow', 'Blad! Sprawdz Logi Systemowe!');
        });
    }

    $scope.importFirms = function () {
        settingsService.importFirms().then(function () {
            $scope.checkUpdateStatus('firms');
            $scope.importAllCompanies();
        },
        function () {
            modalService.alert('Import Firm', 'Blad! Sprawdz Logi Systemowe!');
        });
    }

    $scope.importAreas = function () {
        settingsService.importAreas().then(function () {
            $scope.checkUpdateStatus('areas');

        },
        function () {
            modalService.alert('Import Rejonow', 'Blad! Sprawdz Logi Systemowe!');
        });
    }

    $scope.importWorkplaces = function () {
        settingsService.importWorkplaces().then(function () {
            $scope.checkUpdateStatus('workplaces');

        },
        function () {
            modalService.alert('Import Miejsc Pracy', 'Blad! Sprawdz Logi Systemowe!');
        });
    }

    $scope.importSubdepartments = function () {
        settingsService.importSubdepartments().then(function () {
            $scope.checkUpdateStatus('subdepartment');

        },
        function () {
            modalService.alert('Import Podwydzialow', 'Blad! Sprawdz Logi Systemowe!');
        });
    }
    $scope.importDepartments = function () {
        settingsService.importDepartments().then(function () {
            $scope.checkUpdateStatus('department');

        },
        function () {
            modalService.alert('Import Wydzialow', 'Blad! Sprawdz Logi Systemowe!');
        });
    }
    $scope.importAccount5 = function () {
        settingsService.importAccount5().then(function () {
            $scope.checkUpdateStatus('account5');
        },
        function () {
            modalService.alert('Import Kont5', 'Blad! Sprawdz Logi Systemowe!');
        });
    }

    $scope.importDocumentTypes = function () {
        settingsService.importDocumentTypes().then(function () {
            $scope.checkUpdateStatus('dokRodzaj');
        },
        function () {
            modalService.alert('Import Kont5', 'Blad! Sprawdz Logi Systemowe!');
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
        },
        dokRodzaj: {
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
    $scope.checkUpdateStatus('dokRodzaj');
    $scope.importAllCompanies();
    $scope.importAllUsers();
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