'use strict';
angular.module('et.controllers').controller('menuController', ['$scope', '$state', function ($scope, $state) {
    $scope.isUserLoggedIn = function () {
        return $state.current.name !== 'login';
    }

}]);