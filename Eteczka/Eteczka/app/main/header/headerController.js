'use strict';
angular.module('et.controllers').controller('headerController', ['$rootScope', '$scope', '$state', 'sessionService', function ($rootScope, $scope, $state, sessionService) {
    $scope.userOptions = [
        {
            name: 'Wyloguj',
            iconClass: 'user-option fa fa-power-off',
            onclick: function () {
                sessionService.killSession($rootScope.SESSIONID).then(function () {
                    $rootScope.SELECTED_FIRM = '';
                    $scope.userLoggedIn = false;
                    $scope.loginStatus = '';
                    $state.go('login');
                }, function (err) {
                    $rootScope.SELECTED_FIRM = '';
                    $scope.loginStatus = '';
                    $scope.userLoggedIn = false;
                    $state.go('login');
                    console.error(err);
                });
            }
        }
    ];
    $scope.menusVisible = false;
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $scope.activeOption = toState.name;
        $scope.menusVisible = toState.name !== 'options' && toState.name !== 'login' && toState.name !== 'processing';
    });

    $scope.isActive = function (tab) {
        if (tab === $scope.activeOption) {
            return 'option-active';
        } else {
            return '';
        }
    }

    $scope.userLoggedIn = false;
    $scope.loginStatus = '';

    $scope.goHome = function () {
        if ($scope.userLoggedIn) {
            $state.go('options');
        }
    }

    $scope.userButtons = [
        {
            label: '',
            iconClass: '',
            action: function () {
            }
        },
        {
            label: '',
            iconClass: '',
            action: function () {
            }
        },
        {
            label: '',
            iconClass: '',
            action: function () {
            }
        },
        {
            label: '',
            iconClass: '',
            action: function () {
            }
        }
    ];

    $scope.navigateTo = function (view) {
        $state.go(view);
    }

    $rootScope.$on('USER_LOGGED_IN_EV', function (ev, user) {
        $scope.userLoggedIn = true;
        if (user) {
            $scope.loginStatus = 'ZALOGOWANO, ' + user.Nazwa;
        }
    });

}]);