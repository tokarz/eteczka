'use strict';
angular.module('et.controllers').controller('loginViewController', ['$rootScope', '$scope', '$state', 'loginService', 'sessionService', function ($rootScope, $scope, $state, loginService, sessionService) {

    $scope.credentials = {
        username: '',
        password: ''
    };

    $scope.tryLogin = function (credentials) {
        $state.go('processing');
        $scope.firmChoices = [];
        loginService.authenticate(credentials.username, credentials.password).then(function (result) {
            if (result && result.success) {
                sessionService.createSession().then(function (sessionId) {
                    $rootScope.SESSIONID = sessionId;
                    $scope.fetchedUser = result.user;


                    if ($scope.fetchedUser && $scope.fetchedUser[0].isAdmin) {
                        $state.go('admin');
                    } else {
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
                }, function () {
                    alert('Blad Sesji!');
                    $state.go('login');
                });
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