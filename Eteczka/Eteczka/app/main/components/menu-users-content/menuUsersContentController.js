﻿'use strict';
angular.module('et.controllers').controller('menuUsersContentController', ['$scope', '$state', '$mdDialog', 'settingsService', 'modalService', '_', function ($scope, $state, $mdDialog, settingsService, modalService, _) {
    $scope.userDetails = [];
    $scope.allSelected = false;
    $scope.selectedCompany = {};
    //$scope.confidentialValues = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    $scope.confidentialValues = _.range(1,11).map(function (i) { return '' + i; });

    $scope.toggleSelectCompany = function (userCompany) {
        if (!_.isEmpty($scope.selectedCompany)) {
            if (_.isEqual($scope.selectedCompany, userCompany)) {
                $scope.selectedCompany = {};
            } else {
                $scope.selectedCompany = userCompany;
            }
        } else {
            $scope.selectedCompany = userCompany;
        }
    }

    $scope.isCompanySelected = function (userCompany) {
        var result = '';
        if (_.isEqual($scope.selectedCompany, userCompany)) {
            result = 'active-row';
        }

        return result;
    }

    $scope.toggleBox = function (x, company) {
        x = !x;

        company.allSelected = $scope.checkIfAllSelected(company.Uprawnienia);
    }

    $scope.toggleSelectAll = function (company) {
        var areAlleAlreadySelected = $scope.checkIfAllSelected(company.Uprawnienia);
        for (var property in company.Uprawnienia) {
            if (company.Uprawnienia.hasOwnProperty(property)) {
                company.Uprawnienia[property] = !areAlleAlreadySelected;
            }
        }

        company.allSelected = $scope.checkIfAllSelected(company.Uprawnienia);
    }

    $scope.$watch('user', function (user) {
        $scope.userDetails = [];
        $scope.userCompanies = [];

        if (user && !_.isEmpty(user) && $scope.details) {
            $scope.fullDetailsForUser = $scope.details.find(function (detail) {
                return detail.Detale.Identyfikator === user.Identyfikator
            });
            if ($scope.fullDetailsForUser) {
                $scope.userDetails = $scope.fullDetailsForUser.Detale;
                $scope.userCompanies = $scope.fullDetailsForUser.Firmy.map(x => { x.Confidential += ''; return x; });

                $scope.userCompanies.forEach(function (company) {
                    company.allSelected = $scope.checkIfAllSelected(company);
                })
            }
        }
    });

    $scope.checkIfAllSelected = function (list) {
        var result = true;
        for (var property in list) {
            if (list.hasOwnProperty(property)) {
                result = (result && list[property]);
            }
        }

        return result;
    }

    $scope.triggerDeleteUserCompany = function (company) {
        var confirm = $mdDialog.confirm()
            .title('Czy Chcesz Usunąć dostęp do firmy' + company.Firma + ' ?')
            .textContent('Usunięcie firmy odbierze użytkownikowi prawa dostępu do pracowników i dokumentów firmy')
            .ariaLabel('Lucky day')
            .ok('Tak')
            .cancel('Nie');

        $mdDialog.show(confirm).then(function (value) {
            settingsService.deleteCompanyForUser(company).then(function (res) {
                if (res.success) {
                    $state.reload();
                }
            }).catch(function (ex) {

                $mdDialog.show(
                    $mdDialog.alert()
                        .clickOutsideToClose(true)
                        .title('Błąd podczas usuwania!')
                        .textContent('Wystąpił nieoczekiwany błąd serwera! Sprawdź logi')
                        .ariaLabel('Alert Dialog Demo')
                        .ok('Rozumiem')
                ).then(function () {
                    $state.go('login');
                });
            })
        });
    }

    var openModal = function (modalOptions, executor) {
        return modalService.showModal(modalOptions)
            .then(function (result) {
                return executor(result);
            })
            .catch(function (ex) {
                if (ex !== 'cancel' && ex !== 'backdrop click') {
                    console.error(ex);
                }
            });
    }

    $scope.openAddUserDialog = function () {
        var modalOptions = {
            body: 'app/main/components/menu-users-content/addEditUserModal/userModal.html',
            controller: $scope.addUserControllerFunction,
            //locals: {
            //    description: $scope.fileDescription,
            //    fileTypes: $scope.fileTypes,
            //    employees: $scope.employees,
            //    activeEmployee: $rootScope.activeUser,
            //    name: $scope.selectedstagedfile ? $scope.selectedstagedfile.NazwaEad : ''
            //}
        };

        openModal(
            modalOptions,
            function (value) {
                settingsService.addNewUser(value).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                }).catch();
            }
        );
    }

    $scope.addUserControllerFunction = function ($scope, $mdDialog, modalService) {
        $scope.modalResult = {};

        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.docPartOptions = ['A', 'B', 'C']

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

    }

}]);