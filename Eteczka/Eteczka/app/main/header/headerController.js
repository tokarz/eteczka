'use strict';
angular.module('et.controllers').controller('headerController', ['$rootScope', '$scope', '$state', '$mdDialog', '$timeout', 'sessionService', 'shopCartService', 'modalService', function ($rootScope, $scope, $state, $mdDialog, $timeout, sessionService, shopCartService, modalService) {
    $scope.selectedcompany = null;
    $scope.userLoggedIn = false;
    $scope.loginStatus = '';
    $scope.isAdmin = false;
    $scope.menuEmployeesVisible = false;
    $scope.menuGitVisible = false;
    $scope.menusVisible = false;

    $scope.basket = {
        count: 0
    };
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


    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $scope.activeOption = toState.name;
        $scope.isAdmin = (toState.name === 'admin');
        $scope.menusVisible = toState.name !== 'options' && toState.name !== 'login' && toState.name !== 'processing' && !$scope.isAdmin;
        $scope.smallOptions = [];
        $scope.menuEmployeesVisible = toState.name.startsWith('emp');

        if ($scope.menuEmployeesVisible) {
            $scope.smallOptions = [
                {
                    id: 0,
                    className: 'fa  fa-file-text-o small-option-two',
                    label: 'Teczki akt osobowych',
                    active: false,
                    action: function () {

                        $scope.navigateTo('emp-files');
                    }
                },
                {
                    id: 1,
                    className: 'fa fa-address-book-o small-option-one',
                    label: 'Katalog pracownikow',
                    active: true,
                    action: function () {
                        $scope.navigateTo('emp-employees');
                    }
                }

            ];

            $scope.activeSmallOption = toState.name === 'emp-files' ? $scope.smallOptions[0] : $scope.smallOptions[1];
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
            label: 'Wyszukiwanie dokumentów',
            active: false,
            action: function () {
                $scope.activeSmallOption = $scope.smallOptions[1];
                $scope.navigateTo('fi-catalog');
            }
        },
        {
                id: 2,
                className: 'fa fa-book small-option-three',
                label: 'Słownik rodzajów dokumentów',
                active: false,
                action: function () {
                    $scope.activeSmallOption = $scope.smallOptions[2];

                    var modalOptions = {
                        body: 'app/main/header/modal/addFileTypeModal.html',
                        controller: function ($scope, $mdDialog) {
                            $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
                            $scope.editOptions = [{ name: 'TAK', value: 'b' }, { name: 'NIE', value: 'a' }]
                            $scope.docPartOptions = ['A', 'B', 'C']
                            $scope.modalResult.Dokwlasny = $scope.modalResult.Dokwlasny || $scope.yesNoOptions[0].value;

                            $scope.hide = function () {
                                $mdDialog.hide();
                            };

                            $scope.cancel = function () {
                                $mdDialog.cancel();
                            };

                            $scope.answer = function (answer, errors) {
                                console.log(errors)
                                if (!errors || Object.keys(errors).length === 0) {
                                    $mdDialog.hide(answer);
                                }
                            };
                        },
                    };

                    modalService.showModal(modalOptions)
                        .then(function (result) {
                            // TODO: add executor
                        })
                        .catch(function (ex) {
                            if (ex !== 'cancel' && ex !== 'backdrop click') {
                                console.error(ex);
                            }
                        });
                }
        }
            ];

            $scope.activeSmallOption = toState.name === 'fi-files' ? $scope.smallOptions[0] : $scope.smallOptions[1];
        }

        $scope.shoppingCartVisible = (toState.name === 'shopcart');
        if ($scope.shoppingCartVisible) {
            $scope.smallOptions = [
                {
                    id: 0,
                    className: 'fa fa-shopping-basket small-option-one',
                    label: 'Koszyk dokumentow',
                    active: true
                }
            ];

            $scope.activeSmallOption = $scope.smallOptions[0];
        }
        if (!$scope.isAdmin) {
            shopCartService.getShoppingCartFilesCount().then(function (result) {
                if (result) {
                    $scope.basket.count = result.ilosc;
                }
            });
        }
    });

    $scope.$on('RECALCULATE_CART', function () {
        if (!$scope.isAdmin) {
            shopCartService.getShoppingCartFilesCount().then(function (result) {
                if (result) {
                    $scope.basket.count = result.ilosc;
                }
            });
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
            $scope.isAdmin = user.isadmin;
            $scope.loginStatus = user.userdetails.Nazwisko + ' ' + user.userdetails.Imie;

            $scope.firmparams = {
                selectedfirm: user.companies[0],
                firms: user.companies
            }
            if ($scope.isAdmin) {
                shopCartService.getShoppingCartFilesCount().then(function (result) {
                    if (result) {
                        $scope.basket.count = result.ilosc;
                    }
                });
            }
        }
    });

}]);