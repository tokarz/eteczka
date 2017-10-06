'use strict';
angular.module('et.controllers').controller('headerController', ['$rootScope', '$scope', '$state', '$mdDialog', '$timeout', 'sessionService', function ($rootScope, $scope, $state, $mdDialog, $timeout, sessionService) {
    $scope.selectedcompany = null;

    $scope.userOptions = [
        {
            name: 'Wyloguj',
            iconClass: 'user-option fa fa-power-off',
            onclick: function () {
                sessionService.killSession($rootScope.SESSIONID.IdSesji).then(function () {
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
    $scope.menuGitVisible = false;
    $scope.menusVisible = false;

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {

        $scope.activeOption = toState.name;
        $scope.menusVisible = toState.name !== 'options' && toState.name !== 'login' && toState.name !== 'processing' && toState.name !== 'admin';
        $scope.menuEmployeesVisible = toState.name.startsWith('emp');

        if ($scope.menuEmployeesVisible) {
            $scope.smallOptions = [
        {
            id: 0,
            className: 'fa fa-address-book-o small-option-one',
            label: 'Katalog pracownikow',
            active: true,
            action: function () {
                $scope.navigateTo('emp-employees');
            }
        },
        {
            id: 1,
            className: 'fa  fa-file-text-o small-option-two',
            label: 'Teczki akt osobowych',
            active: false,
            action: function () {

                $scope.navigateTo('emp-files');
            }
        }
            ];

            $scope.activeSmallOption = toState.name === 'emp-employees' ? $scope.smallOptions[0] : $scope.smallOptions[1];
        }

        $scope.menuGitVisible = toState.name.startsWith('fi');
        if ($scope.menuGitVisible) {
            $scope.smallOptions = [
        {
            id: 0,
            className: 'fa fa-plus-square small-option-one',
            label: 'Wprowadzanie plikow',
            active: true,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[0];
                $scope.navigateTo('fi-files');
            }
        },
        {
            id: 1,
            className: 'fa fa-folder small-option-two',
            label: 'Katalog dokumentow',
            active: false,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[1];
                $scope.navigateTo('fi-catalog');
            }
        }
            ];

            $scope.activeSmallOption = toState.name === 'fi-files' ? $scope.smallOptions[0] : $scope.smallOptions[1];
        }
    });

    $scope.isActive = function (tab) {
        if ($scope.activeOption.startsWith(tab)) {
            return 'option-active';
        } else {
            return '';
        }
    }

    $scope.isSmallOptionActive = function (op) {
        var result = '';
        if ($scope.activeSmallOption.id === op.id) {
            result = 'option-active';
        }

        return result;
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
            $scope.loginStatus = user.userdetails.Nazwisko + ' ' + user.userdetails.Imie;

            $scope.firmparams = {
                selectedfirm: user.companies[0],
                firms: user.companies
            }
        }
    });

}]);