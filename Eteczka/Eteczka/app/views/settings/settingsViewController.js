'use strict';
angular.module('et.controllers').controller('settingsViewController', ['$scope', 'settingsService', function ($scope, settingsService) {



    $scope.importFiles = function () {
        settingsService.importFiles(true).then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importAll = function () {
        //ToDo : import wszystkich plikow i oznaczenie ze sa aktualne
    }

    $scope.refreshImportStatus = function () {

    }

    $scope.importArchives = function () {
        settingsService.importArchives().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importUsers = function () {
        settingsService.importUsers().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importFirms = function () {
        settingsService.importFirms().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importAreas = function () {
        settingsService.importAreas().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importWorkplaces = function () {
        settingsService.importWorkplaces().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }


    $scope.importSubdepartments = function () {
        settingsService.importSubdepartments().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }
    $scope.importDepartments = function () {
        settingsService.importDepartments().then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }
    $scope.importAccount5 = function () {
        settingsService.importAccount5().then(function () {
            alert('successfull');
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

    $scope.checkUpdateStatus = function (type) {
        settingsService.checkUpdateStatus(type).then(function (result) {
            $scope.updateStatus[type] = {
                success: result.success,
                countJson: result.importJson,
                countDb: result.importDb
            }
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


}]);