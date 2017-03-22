'use strict';
angular.module('et.controllers').controller('mainController', ['$scope', 'startupService', function ($scope, startupService) {
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;

    $scope.dot = {
        one: 'one'
    };

    startupService.initializeApplicationConttext().then(function () {
        $scope.isLoaded = true;
        $scope.title = 'ETeczka Ready!';
    });


}]);