'use strict';
angular.module('et.controllers').controller('loginViewController', ['$rootScope', '$scope', '$state', 'loginService', function ($rootScope, $scope, $state, loginService) {

    $scope.credentials = {
        username: '',
        password: ''
    };

    $scope.tryLogin = function (credentials) {
        $state.go('processing');
        $scope.firmChoices = [];
        loginService.authenticate(credentials.username, credentials.password).then(function (result) {
            if (result) {
                $scope.fetchedUser = result.user;
                $rootScope.$broadcast('USER_LOGGED_IN_EV', $scope.fetchedUser);
                if ($scope.fetchedUser.length > 0) {
                    $scope.choices = $scope.fetchedUser.map(function (x) {
                        return x.FirmaSymbol;
                    });

                    $state.go('choosefirm', {
                        "choices": $scope.choices
                    });
                } else {
                    $rootScope.$broadcast('USER_LOGGED_IN_EV', $scope.fetchedUser);
                    $state.go('options');
                }

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