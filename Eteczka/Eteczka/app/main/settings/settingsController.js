'use strict';
angular.module('et.controllers').controller('settingsController', ['$scope', '$state', function ($scope, $state) {
    $scope.goToSettings = function () {
        $state.go('settingsusers');
    }
}]);