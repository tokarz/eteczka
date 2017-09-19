'use strict';
angular.module('et.controllers').controller('mainController', ['$window', '$rootScope', '$scope', '$state', 'startupService', 'sessionService', function ($window, $rootScope, $scope, $state, startupService, sessionService) {
    $state.go('login');
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;
    $scope.selectedUser = '';
    $scope.selectedFirm = '';

    $scope.isLoggedIn = false;

    $scope.startupContext = {
        title: 'EAd',
        version: '0.2a'
    };

    $scope.currentState = {
        state: $state.current.name
    };

    $scope.$on('USER_LOGGED_IN_EV', function (ev, user) {
        $scope.isLoggedIn = true;
        $scope.selectedUser = user.userdetails;
        $scope.companies = user.companies;
        $scope.selectedFirm = $scope.companies[0];
    });

    $window.onbeforeunload = function () {
        sessionService.killSession().then(function () {
            $rootScope.isLoggedIn = false;
        });
    };
}]);