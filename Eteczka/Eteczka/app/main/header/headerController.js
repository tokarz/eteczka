'use strict';
angular.module('et.controllers').controller('headerController', ['$rootScope', '$scope', '$state', 'sessionService', function ($rootScope, $scope, $state, sessionService) {
    $scope.smallOptions = [
        {
            className: 'fa fa-address-book-o small-option-one',
            label: 'Katalog pracownikow',
            active: true,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[0];
                $scope.navigateTo('employees');
            }
        },
        {
            className: 'fa  fa-file-text-o small-option-two',
            label: 'Teczki akt osobowych',
            active: false,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[1];
                $scope.navigateTo('employeesfiles');
            }
        }
    ];

    $scope.activeSmallOption = $scope.smallOptions[0];

    $scope.isSmallOptionActive = function (op) {
        var result = '';

        if (op === $scope.activeSmallOption) {
            result = 'option-active';
        }

        return result;
    }

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
    $scope.menuEmployeesVisible = false;
    $scope.menusVisible = false;
    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $scope.activeOption = toState.name;
        $scope.menusVisible = toState.name !== 'options' && toState.name !== 'login' && toState.name !== 'processing' && toState.name !== 'admin';
        $scope.menuEmployeesVisible = (toState.name === 'employees' || toState.name === 'employeesfiles');
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
            $scope.loginStatus = 'ZALOGOWANO, ' + user.Nazwisko + ' ' + user.Imie;
        }
    });

}]);