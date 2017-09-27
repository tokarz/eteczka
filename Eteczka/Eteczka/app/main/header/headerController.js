'use strict';
angular.module('et.controllers').controller('headerController', ['$rootScope', '$scope', '$state', '$mdDialog', 'sessionService', function ($rootScope, $scope, $state, $mdDialog, sessionService) {
    $scope.selectedcompany = null;

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
        $scope.menuEmployeesVisible = (toState.name === 'employees' || toState.name === 'employeesfiles');
        if ($scope.menuEmployeesVisible) {
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
        }
        $scope.menuGitVisible = (toState.name === 'files' || toState.name === 'filecatalog');
        if ($scope.menuGitVisible) {
            $scope.smallOptions = [
        {
            className: 'fa fa-plus-square small-option-one',
            label: 'Wprowadzanie plikow',
            active: true,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[0];
                $scope.navigateTo('files');
            }
        },
        {
            className: 'fa  fa-folder small-option-two',
            label: 'Katalog dokumentow',
            active: false,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[1];
                $scope.navigateTo('filecatalog');
            }
        }
            ];

            $scope.activeSmallOption = $scope.smallOptions[0];
        }
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
            $scope.loginStatus = user.userdetails.Nazwisko + ' ' + user.userdetails.Imie;

            $scope.firmparams = {
                selectedfirm: user.companies[0],
                firms: user.companies
            }
        }
    });

}]);