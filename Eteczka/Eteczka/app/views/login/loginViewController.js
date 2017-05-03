'use strict';
angular.module('et.controllers').controller('loginViewController', ['$scope', '$state', 'loginService', function ($scope, $state, loginService) {

    $scope.credentials = {
        username: 'xxx',
        password: ''
    };

    $scope.tryLogin = function (credentials) {
        loginService.authenticate(credentials.username, credentials.password).then(function (result) {
            if (result && result.user) {
                $scope.$emit('USER_LOGGED_IN_EV', result.user);
                $state.go('options');
            }
            else {
                alert('Login failed!');
            }
        });
    }

    $scope.openAddUserForm = function () {
        $state.go('addUsers');
    }

}]);