'use strict';
angular.module('et.controllers').controller('menuUsersContentController', ['$scope', '$state', '$mdDialog', 'settingsService', 'modalService', 'companiesService', 'usersService', '_', function ($scope, $state, $mdDialog, settingsService, modalService, companiesService, usersService, _) {
    $scope.userDetails = [];
    $scope.allCompanies = [];
    $scope.allSelected = false;
    $scope.selectedCompany = false;
    //$scope.confidentialValues = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    $scope.confidentialValues = _.range(1, 11).map(function (i) { return '' + i; });

    $scope.toggleSelectCompany = function (userCompany) {
        if (!_.isEmpty($scope.selectedCompany)) {
            if (_.isEqual($scope.selectedCompany, userCompany)) {
                $scope.selectedCompany = false;
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
    };

    $scope.toggleBox = function (x, company) {
        x = !x;

        company.allSelected = $scope.checkIfAllSelected(company.Uprawnienia);
    };

    $scope.toggleSelectAll = function (company) {
        var areAlleAlreadySelected = $scope.checkIfAllSelected(company.Uprawnienia);
        for (var property in company.Uprawnienia) {
            if (company.Uprawnienia.hasOwnProperty(property)) {
                company.Uprawnienia[property] = !areAlleAlreadySelected;
            }
        }

        company.allSelected = $scope.checkIfAllSelected(company.Uprawnienia);
    };

    $scope.$watch('user', function (user) {
        $scope.userDetails = [];
        $scope.userCompanies = [];
        $scope.unassignedCompanies = [];
        $scope.activeUser = null;
        if (user && !_.isEmpty(user) && $scope.details) {
            $scope.activeUser = user;

            $scope.fullDetailsForUser = $scope.details.find(function (detail) {
                return detail.Detale.Identyfikator === user.Identyfikator;
            });
            if ($scope.fullDetailsForUser) {
                $scope.userDetails = $scope.fullDetailsForUser.Detale;
                $scope.userCompanies = $scope.fullDetailsForUser.Firmy.map(x => { x.Confidential += ''; return x; });

                $scope.userCompanies.forEach(function (company) {
                    company.allSelected = $scope.checkIfAllSelected(company);
                });

                $scope.unassignedCompanies = $scope.allCompanies.filter(function (currentCompany) {
                    var found = false;
                    $scope.userCompanies.forEach(function (currentUserCompany) {
                        found = found || (currentUserCompany.Firma.trim() === currentCompany.Firma.trim());
                    });

                    return !found;
                });
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
    };

    $scope.triggerDeleteUser = function (user) {
        var confirm = $mdDialog.confirm()
            .title('Czy Chcesz Usunąć użytkownika [' + user.Identyfikator + '] i wszystkie jego uprawnienia?')
            .textContent('Usunięcie użytkownika może zostać cofnięte tylko przez specjalną interwencję')
            .ariaLabel('Lucky day')
            .ok('Tak')
            .cancel('Nie');

        $mdDialog.show(confirm).then(function (value) {
            settingsService.deleteUser(user).then(function (res) {
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
            });
        });
    };

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
            });
        });
    };

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
    };

    $scope.openEditUserDialog = function () {
        var modalOptions = {
            body: 'app/main/components/menu-users-content/addEditUserModal/userModal.html',
            controller: $scope.addUserControllerFunction,
            locals: {
                isEdit: true,
                user: $scope.activeUser
            }
        };

        openModal(
            modalOptions,
            function (value) {
                settingsService.editUser(value).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                }).catch();
            }
        );
    };

    $scope.openAddUserDialog = function () {
        var modalOptions = {
            body: 'app/main/components/menu-users-content/addEditUserModal/userModal.html',
            controller: $scope.addUserControllerFunction,
            locals: {
                isEdit: false,
                user: null
            }
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
    };

    $scope.addUserControllerFunction = function ($scope, $mdDialog, modalService, isEdit, user) {
        $scope.isEdit = isEdit ? true : false;

        $scope.modalResult = {};

        if (user) {
            $scope.modalResult = user;
        }

        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.docPartOptions = ['A', 'B', 'C'];

        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.isEditable = function () {
            let result = '';
            if (isEdit) {
                result = 'disabled-field';
            }

            return result;
        };

        $scope.answer = function (answer, errors) {
            console.log(errors)
            if (!errors || Object.keys(errors).length === 0) {
                $mdDialog.hide(answer);
            }
        };

        $scope.isNotEqual = function (baseText, textToMatch) {
            return (baseText !== textToMatch)
        };
    };

    $scope.companySelected = false;


    $scope.openDeleteUserRightsDialog = function () {

    };

    $scope.openSetUserRightsDialog = function (edit) {
        var modalOptions = {
            body: 'app/main/components/menu-users-content/setUserRightsModal/userRightsModal.html',
            controller: $scope.setUserRightsController,
            locals: {
                companies: edit ? [$scope.selectedCompany] : $scope.unassignedCompanies,
                selected: $scope.selectedCompany
            }
        };

        openModal(modalOptions, function (value) {
            var company = {
                Identyfikator: $scope.activeUser.Identyfikator,
                Firma: value.Firma.Firma,
                Uprawnienia: value.Uprawnienia,
                DataModify: new Date().toLocaleString(),
                Confidential: value.Confidential,
                Usuniety: false
            };

            if (edit) {
                settingsService.updateUserCompany(company).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                });
            } else {
                settingsService.addCompanyToUser(company).then(function (res) {
                    if (res.success) {
                        $state.reload();
                    }
                });
            }


        }
        ).catch();
    };

    $scope.setUserRightsController = function ($scope, $mdDialog, modalService, companies, selected) {
        $scope.modalResult = {};
        $scope.userCompanies = companies;

        if (selected) {
            $scope.modalResult.Firma = selected.Firma;
            $scope.modalResult.Confidential = selected.Confidential;
            $scope.modalResult.Uprawnienia = selected.Uprawnienia;
        }

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
    };

    companiesService.getAll().then(function (all) {
        $scope.allCompanies = all.Firmy;
    });
}]);