﻿'use strict';
angular.module('et.controllers').controller('mainController', ['$rootScope', '$scope', '$state', 'startupService', function ($rootScope, $scope, $state, startupService) {
    $state.go('login');
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;

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