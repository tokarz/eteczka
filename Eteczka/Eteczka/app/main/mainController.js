'use strict';
angular.module('et.controllers').controller('mainController', ['$rootScope', '$scope', '$state', 'startupService', function ($rootScope, $scope, $state, startupService) {
    $state.go('login');
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;
    $scope.selectedUser = '';

    $scope.isLoggedIn = false;

    $scope.$on('USER_LOGGED_IN_EV', function (ev, user) {
        $scope.isLoggedIn = true;
        $scope.selectedUser = user; 
    });

    $scope.startupContext = {
        title: 'EAd',
        version: '0.1a'
    };

    $scope.currentState = {
        state: $state.current.name
    };

    $scope.selectedFirm = '';

    $rootScope.$watch('SELECTED_FIRM', function (value) {
        $scope.selectedFirm = value;
    });

}]);