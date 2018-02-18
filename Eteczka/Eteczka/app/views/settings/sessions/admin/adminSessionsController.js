'use strict';
angular.module('et.controllers').controller('settingsSessionsController', ['$scope', 'sessionService', 'settingsService', function ($scope, sessionService, settingsService) {
    $scope.openSessions = [];

    $scope.killSession = function (session) {
        sessionService.killGivenSession(session.IdSesji).then(function (result) {
            $scope.fetchAllSessions();
        });
    }

    $scope.fetchAllSessions = function () {
        settingsService.fetchAllOpenSessions().then(function (res) {
            $scope.openSessions = res.sesje;
        });
    }

    $scope.setPasswordDialog = function () {
        alert('ToDo');
    }

    $scope.fetchAllSessions();
}]);