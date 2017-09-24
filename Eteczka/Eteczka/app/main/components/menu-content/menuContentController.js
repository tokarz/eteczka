'use strict';
angular.module('et.controllers').controller('menuContentController', ['$rootScope', '$scope', 'menuContentService', 'modalService', 'peselService', 'utilsService', function ($rootScope, $scope, menuContentService, modalService, peselService, utilsService) {
    $scope.$watch('user', function (value) {
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                $scope.workplaces = result.MiejscaPracy;
            });
        }
    });

    $scope.activeSession = {}
    $scope.selectedWorkplace = {};

    $rootScope.$watch('SESSIONID', function (value) {
        if (value && value !== $scope.activeSession) {
            console.log('selectedfirm changed', value, $scope.activeSession)
            $scope.activeSession = value
            loadRegionList($scope.activeSession.AktywnaFirma)
            loadDepartmentList($scope.activeSession.AktywnaFirma)
            loadAccounts5($scope.activeSession.IdSesji)
        }
    })

    $scope.workplaceParams = {
        loadingRegions: false,
        loadingDepartments: false,
        loadingSubDepartments: false,
        loadingAccouts5: false,
        regions: [],
        departments: [],
        subDepartments: [],
        accounts5: []
    };

    var loadRegionList = function (company) {
        $scope.workplaceParams.loadingRegions = true;
        $scope.workplaceParams.regions = []

        return menuContentService.getRegionsForFirm(company)
            .then(function (result) {
                $scope.workplaceParams.loadingRegions = false;
                $scope.workplaceParams.regions = result.Rejony
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingRegions = false;
                console.error(error)
            });
    }

    var loadDepartmentList = function (company) {
        $scope.workplaceParams.loadingDepartments = true;
        $scope.workplaceParams.departments = []

        return menuContentService.getDepartmentsForFirm(company)
            .then(function (result) {
                $scope.workplaceParams.loadingDepartments = false;
                $scope.workplaceParams.departments = result.Wydzialy
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingDepartments = false;
                console.error(error)
            });
    }

    var loadAccounts5 = function (sessionId) {
        $scope.workplaceParams.loadingAccouts5 = true;
        $scope.workplaceParams.accounts5 = []

        return menuContentService.getAccounts5(sessionId)
            .then(function (result) {
                console.log('konta5', result)
                $scope.workplaceParams.loadingAccouts5 = false;
                $scope.workplaceParams.accounts5 = result.pobraneKonta5
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingAccouts5 = false;
                console.error(error)
            });
    }

    $scope.selectRow = function (workplace) {
        if ($scope.selectedWorkplace === workplace) {
            $scope.selectedWorkplace = {};
        } else {
            $scope.selectedWorkplace = workplace;
        }
    }

    $scope.getRowStyle = function (workplace) {
        var result = 'details-table-row';

        if (workplace === $scope.selectedWorkplace) {
            result += ' active-row';
        }

        return result;
    }

    var openModal = function (modalOptions, executor, initialInput) {
        return modalService.showModal(modalOptions, initialInput)
            .then(function (result) { return executor(result) })
            .catch(function (error) {
                if (error !== 'cancel' && error !== 'backdrop click') {
                    console.log("error found!", error);
                }
            });
    }

    var upsertEmployeeModalFunctions = {
        shouldDisableByPesel: function (pesel, field) {
            var isNoPesel = (pesel === null || pesel === '' || typeof pesel === 'undefined')
            var fieldHasValue = (typeof field === 'string' && field.trim() !== '')

            if (fieldHasValue || (!fieldHasValue && isNoPesel)) {
                return false;
            }

            return true;
        },
        isPeselValid: function (pesel, gender) {
            return peselService.isPeselValid(pesel, gender)
        },
        getBirthdate: function (pesel, gender) {
            return peselService.getDateFromPesel(pesel, gender)
        }
    }

    $scope.triggerAddEmployeeDialog = function () {
        var modalOptions = {
            title: 'Dodawanie nowego pracownika',
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html'
        }

        openModal(
            Object.assign(modalOptions, upsertEmployeeModalFunctions),
            function (value) { console.log('tu bedzie wywolanie funkcji dodawania pracownika', value) }
        )
    }

    $scope.triggerEditEmployeeDialog = function () {
        var modalOptions = {
            title: 'Edytowanie pracownika',
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html'
        }
        var userToPass = Object.assign({}, $scope.user)

        console.log(userToPass)

        openModal(
            Object.assign(modalOptions, upsertEmployeeModalFunctions),
            function (value) { console.log('tu bedzie wywolanie funkcji edytowania pracownika', value) },
            userToPass
        )
    }

    $scope.triggerDeleteEmployeePopup = function () {
        triggerAdminPasswordCheck()
            .then(function (isAdminPasswordCorrect) {
                if (typeof isAdminPasswordCorrect === 'undefined') {
                    return 
                }
               
                if (isAdminPasswordCorrect === false) {
                    return alert('haslo administratora niepoprawne')
                }

                var modalOptions = {
                    title: 'Usuwanie pracownika z bazy danych',
                    body: 'app/views/employees/editEmployeesPopup/deleteUserModal.html'
                }

                openModal(
                    modalOptions,
                    function (value) {
                        triggerShortPasswordCheck()
                            .then(function (isShortPasswordCorrect) {
                                if (typeof isShortPasswordCorrect === 'undefined') {
                                    return
                                }

                                if (isShortPasswordCorrect === false) {
                                    return alert('haslo uzytkownika niepoprawne')
                                }

                                console.log('tu bedzie wywolanie funkcji usuwania pracownika', value)
                            })
                            .catch(console.error)
                    },
                    $scope.user
                )
            })
        .catch(console.error)
    }

    var triggerAdminPasswordCheck = function() {
        var modalOptions = {
            title: 'Wymagane haslo administratora',
            body: 'app/main/utils/modalTemplate/adminPasswordModal.html'
        }

        return openModal(
            modalOptions,
            function (value) {
                /* return menuContentService.getCurrentAdminPassword().then(function (result) {
                    return result === value
                })
                */

                return true
            }
        )
    }

    var triggerShortPasswordCheck = function () {
        var modalOptions = {
            title: 'Wymagane potwierdzenie haslem uzytkownika',
            body: 'app/main/utils/modalTemplate/userPasswordModal.html'
        }

        return openModal(
            modalOptions,
            function (value) {
                /* return menuContentService.getUserPassword().then(function (result) {
                    return result === value
                })
                */

                return true
            }
        )
    }

    var upsertWorkplaceModalCommonOptions = {
        loadSubDepartmentList: function (workplaceParams, department) {
            workplaceParams.loadingSubDepartments = true;
            workplaceParams.subDepartments = []
            console.log($scope.activeSession, department)
            return menuContentService.getSubDepartmets($scope.activeSession.IdSesji, department.Wydzial)
                .then(function (result) {
                    console.log('podwydzialy', result)
                    workplaceParams.loadingSubDepartments = false;
                    workplaceParams.subDepartments = result.PodWydzialy
                })
                .catch(function (error) {
                    workplaceParams.loadingSubDepartments = false;
                    console.error(error)
                });
        },
        validateIfProperAccount(account5skr) {
            console.log(account5)
            var account5 = $scope.workplaceParams.accounts5.find(function (acc) {
                return acc.Kontoskr.trim() === account5skr.trim()})
            if (account5) {
                console.log('account5 found', account5)
            }
            else {
                console.error('account not found')
            }
        }
    }

    $scope.triggerAddWorkplaceDialog = function () {
        var modalOptions = {
            title: 'Dodawanie nowego miejca pracy pracownika',
            body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
            workplaceParams: $scope.workplaceParams
        }

        openModal(
            Object.assign(modalOptions, upsertWorkplaceModalCommonOptions),
            function (value) { console.log('tu bedzie wywolanie funkcji dodawania miejsca pracy', value) },
            { Firma: $scope.activeSession.AktywnaFirma }
        )
    }

    $scope.triggerEditWorkplaceDialog = function () {
        var modalOptions = {
            title: 'Edytowanie miejsca pracy pracownika',
            body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
            availableRegions: $scope.workplaceParams.regions,
            availableDepartments: $scope.workplaceParams.departments
        }
        var workplaceToPass = Object.assign({}, $scope.selectedWorkplace)

        console.log(workplaceToPass)

        openModal(
            Object.assign(modalOptions, upsertWorkplaceModalCommonOptions),
            function (value) { console.log('tu bedzie wywolanie funkcji edytowania miejsca pracy', value) },
            workplaceToPass
        )
    }

    $scope.triggerDeleteWorkplaceDialog = function () {
        var modalOptions = {
            title: 'Usuwanie miejsca pracy pracownika',
            body: 'app/views/employees/editWorkplacesPopup/deleteWorkplaceModal.html'
        }

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji usuwania miejsca pracy', value) },
            $scope.selectedWorkplace
        )
    }
}]);