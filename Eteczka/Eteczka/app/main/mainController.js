'use strict';
angular.module('et.controllers').controller('mainController', ['$scope', '$state', 'startupService', function ($scope, $state, startupService) {
    $state.go('login');
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;

    $scope.startupContext = {
        title: 'EAd',
        version: '0.1a'
    };

    startupService.initializeApplicationConttext().then(function () {
        $scope.isLoaded = true;
    });

}]);