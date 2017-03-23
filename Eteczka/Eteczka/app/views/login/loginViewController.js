'use strict';
angular.module('et.controllers').controller('loginViewController', ['$scope', '$state', function ($scope, $state) {

    $scope.tryLogin = function () {
        $state.go('options');
    }

}]);