'use strict';
angular.module('et.controllers').controller('loginViewController', ['$rootScope', '$scope', '$state', 'loginService', function ($rootScope, $scope, $state, loginService) {

    $scope.credentials = {
        username: '',
        password: ''
    };

    $scope.tryLogin = function (credentials) {
        $state.go('processing');
        loginService.authenticate(credentials.username, credentials.password).then(function (result) {
            if (result && result.user) {
                $rootScope.$broadcast('USER_LOGGED_IN_EV', result.user);
                $state.go('options');
            }
            else {
                $state.go('login');
                alert('Login failed!');
            }
        },
        function (err) {
            console.error('LOGIN FAILED!' + err);
            $state.go('login');
        });
    }

    $scope.openAddUserForm = function () {
        $state.go('addUsers');
    }

}]);