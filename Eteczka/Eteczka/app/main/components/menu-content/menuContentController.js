﻿'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', 'menuContentService', 'modalService', 'peselService', 'utilsService', 'sessionService', function ($scope, menuContentService, modalService, peselService, utilsService, sessionService) {
    $scope.$watch('user', function (value) {
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                $scope.workplaces = result.MiejscaPracy;
            });
        }
    });

    $scope.sessionId = null;
    $scope.company = null;
    $scope.selectedWorkplace = {};
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

    var loadDataWithSesionId = function () {
        $scope.sessionId = sessionService.getSessionId()

        loadActiveCompany($scope.sessionId)
        loadRegionList($scope.sessionId)
        loadDepartmentList($scope.sessionId)
        loadAccounts5($scope.sessionId)
    }

    var loadActiveCompany = function (sessionId) {
        return menuContentService.getActiveCompany(sessionId)
            .then(function (activeCompany) {
                console.log('zaladowano firme', activeCompany)
                $scope.company = activeCompany
            })
    }

    var loadRegionList = function (sessionId) {
        $scope.workplaceParams.loadingRegions = true;
        $scope.workplaceParams.regions = []

        return menuContentService.getRegionsForFirm(sessionId)
            .then(function (result) {
                console.log('zaladowano rejony')
                $scope.workplaceParams.loadingRegions = false;
                $scope.workplaceParams.regions = result.Rejony
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingRegions = false;
                console.error(error)
            });
    }

    var loadDepartmentList = function (sessionId) {
        $scope.workplaceParams.loadingDepartments = true;
        $scope.workplaceParams.departments = []

        return menuContentService.getDepartmentsForFirm(sessionId)
            .then(function (result) {
                console.log('zaladowano wydzialy')
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
        $scope.workplaceParams.accounts5 = [];

        return menuContentService.getAccounts5(sessionId)
            .then(function (result) {
                console.log('zaladowano konta5')
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

    $scope.upsertEmployeeCtrl = function ($scope, $mdDialog, user) {
        if (user) {
            $scope.modalResult = user;
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

        $scope.isPeselValid = function (pesel, gender) {
            return peselService.isPeselValid(pesel, gender)
        }
        $scope.getBirthdate = function (pesel, gender) {
            return peselService.getDateFromPesel(pesel, gender)
        }

        $scope.shouldDisableByPesel = function (pesel, field) {
            var isNoPesel = (pesel === null || pesel === '' || typeof pesel === 'undefined')
            var fieldHasValue = (typeof field === 'string' && field.trim() !== '')

            if (fieldHasValue || (!fieldHasValue && isNoPesel)) {
                return false;
            }

            return true;
        }
    }

    $scope.upsertWorkplaceCtrl = function ($scope, $mdDialog, workplace, workplaceParams, sessionId) {
        if (workplace) {
            $scope.modalResult = workplace;
        }

        $scope.sessionId = sessionId;
        $scope.workplaceParams = workplaceParams;

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

        $scope.loadSubDepartmentList = function (department) {
            $scope.workplaceParams.loadingSubDepartments = true;
            $scope.workplaceParams.subDepartments = []
            console.log($scope.sessionId, department)
            return menuContentService.getSubDepartmets($scope.sessionId, department.Wydzial)
                .then(function (result) {
                    console.log('podwydzialy', result)
                    $scope.workplaceParams.loadingSubDepartments = false;
                    $scope.workplaceParams.subDepartments = result.PodWydzialy
                })
                .catch(function (error) {
                    $scope.workplaceParams.loadingSubDepartments = false;
                    console.error(error)
                });
        };

        $scope.validateIfProperAccount = function (account5skr) {
            console.log(account5)
            var account5 = $scope.workplaceParams.accounts5.find(function (acc) {
                return acc.Kontoskr.trim() === account5skr.trim()
            })
            if (account5) {
                console.log('account5 found', account5)
            }
            else {
                console.error('account not found')
            }
        }
    }

    var openModal = function (modalOptions, executor) {
        return modalService.showModal(modalOptions)
            .then(function (result) {
                return executor(result);
            })
            .catch(function (error) {
                if (error !== 'cancel' && error !== 'backdrop click') {
                    console.log("error found!", error);
                }
            });
    }


    $scope.triggerAddEmployeeDialog = function () {
        var modalOptions = {
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html',
            controller: $scope.upsertEmployeeCtrl,
            locals: {
                user: null
            }
        };

        openModal(modalOptions, function (value) {
            console.log('tu bedzie wywolanie funkcji dodawania pracownika', value)
        });
    }

    $scope.triggerEditEmployeeDialog = function () {
        var modalOptions = {
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html',
            controller: $scope.upsertEmployeeCtrl,
            locals: {
                user: Object.assig({}, $scope.user)
            }
        };

        openModal(
            modalOptions,
            function (value) {
                console.log('tu bedzie wywolanie funkcji edytowania pracownika', value);
            }
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
                    body: 'app/views/employees/editEmployeesPopup/deleteUserModal.html',
                    controller: function ($scope, $mdDialog, user) {
                        if (user) {
                            $scope.modalResult = user;
                        }

                        $scope.cancel = function () {
                            $mdDialog.cancel();
                        };

                        $scope.answer = function (answer, errors) {
                            $mdDialog.hide(answer);
                        };
                    },
                    locals: {
                        user: $scope.user
                    }
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

    var triggerAdminPasswordCheck = function () {
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

    $scope.triggerAddWorkplaceDialog = function () {
        var modalOptions = {
            body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
            controller: $scope.upsertWorkplaceCtrl,
            locals: {
                workplace: { Firma: $scope.company.firma},
                workplaceParams: $scope.workplaceParams,
                sessionId: $scope.sessionId
            }
        }

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji dodawania miejsca pracy', value) }
        )
    }

    $scope.triggerEditWorkplaceDialog = function () {
        var modalOptions = {
            body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
            controller: $scope.upsertWorkplaceCtrl,
            locals: {
                workplace: Object.assign({}, $scope.selectedWorkplace),
                workplaceParams: $scope.workplaceParams,
                sessionId: $scope.sessionId
            }
        }

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji edytowania miejsca pracy', value) }
        )
    }

    $scope.triggerDeleteWorkplaceDialog = function () {
        var modalOptions = {
            body: 'app/views/employees/editWorkplacesPopup/deleteWorkplace.html',
            controller: function ($scope, $mdDialog, workplace) {
                if (workplace) {
                    $scope.modalResult = workplace;
                }

                $scope.cancel = function () {
                    $mdDialog.cancel();
                };

                $scope.answer = function (answer, errors) {
                    $mdDialog.hide(answer);
                };
            },
            locals: {
                workplace: $scope.selectedWorkplace,
                workplaceParams: $scope.workplaceParams
            }
        }

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji usuwania miejsca pracy', value) }
        )
    }

    loadDataWithSesionId();
}]);