'use strict';
angular.module('et.controllers').controller('menuController', ['$scope', function ($scope) {
    $scope.userMenuVisible = false;

    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

}]);
