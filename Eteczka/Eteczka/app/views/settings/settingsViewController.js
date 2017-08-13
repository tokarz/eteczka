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
        settingsService.importArchives('someSessionId').then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importUsers = function () {
        settingsService.importUsers('someSessionId').then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importFirms = function () {
        settingsService.importFirms('someSessionId').then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }

    $scope.importAreas = function () {
        settingsService.importAreas('someSessionId').then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }


}]);