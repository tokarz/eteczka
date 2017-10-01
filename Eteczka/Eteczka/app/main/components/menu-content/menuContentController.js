﻿'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', 'menuContentService', 'modalService', 'peselService', 'utilsService', function ($scope, menuContentService, modalService, peselService, utilsService) {
    $scope.$watch('user', function (value) {
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                $scope.workplaces = []

                result.MiejscaPracy.forEach(function (workplace) {
                    var region = getRegionById(workplace.Rejon)
                    var department = getDepartmentById(workplace.Wydzial)
                    var account5 = getAccount5ByNumber(workplace.Konto5)

                    if (typeof workplace.Podwydzial !== 'string' || workplace.Podwydzial.trim() === '') {
                        workplace.Podwydzial = {}
                    }
                    else {
                        getSubdepartmentById(workplace.Wydzial, workplace.Podwydzial).then((subdepartment) => {
                            workplace.Podwydzial = subdepartment
                        });
                    }

                    workplace.Rejon = region
                    workplace.Wydzial = department
                    workplace.Konto5 = account5

                    $scope.workplaces.push(workplace)
                });
            });
        }
    });

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
        loadActiveCompany()
        loadRegionList()
        loadDepartmentList()
        loadAccounts5()
    }

    var getRegionById = function (regionId) {
        if (typeof regionId !== 'string' || regionId.trim() === '') {
            return {}
        }
        
        var region = $scope.workplaceParams.regions.find(function (item) {
            return item.Rejon.trim() === regionId.trim()
        })
        
        if (typeof region === 'undefined') {
            return {}
        }

        return region
    }

    var getDepartmentById = function (departmentId) {
        if (typeof departmentId !== 'string' || departmentId.trim() === '') {
            return {}
        }
        
        var department = $scope.workplaceParams.departments.find(function (item) {
            return item.Wydzial.trim() === departmentId.trim()
        })
        
        if (typeof department === 'undefined') {
            return {}
        }

        return department
    }

    var getSubdepartmentById = function (departmentId, subDepartmentId) {
        
        if (typeof subDepartmentId !== 'string' || subDepartmentId.trim() === '') {
            return {}
        }
        
        return menuContentService.getSubDepartmets(departmentId)
            .then(function (subDepartments) {
                var subDepartment = subDepartments.PodWydzialy.find(function (item) {
                    return item.Podwydzial.trim() === subDepartmentId.trim()
                })
                
                if (typeof subDepartment === 'undefined') {
                    return {}
                }

                return subDepartment
                
            })
            .catch(function (error) {
                console.error(error)

                return {}
            });
    }

    var getAccount5ByNumber = function (account5Number) {
        if (typeof account5Number !== 'string' || account5Number.trim() === '') {
            return {}
        }

        var account5 = $scope.workplaceParams.accounts5.find(function (item) {
            return item.Konto5.trim() === account5Number.trim()
        })

        if (typeof account5 === 'undefined') {
            return {}
        }

        return account5
    }

    var loadActiveCompany = function () {
        return menuContentService.getActiveCompany()
            .then(function (activeCompany) {
                $scope.company = activeCompany
            })
    }

    var loadRegionList = function () {
        $scope.workplaceParams.loadingRegions = true;
        $scope.workplaceParams.regions = []

        return menuContentService.getRegionsForFirm()
            .then(function (result) {
                $scope.workplaceParams.loadingRegions = false;
                $scope.workplaceParams.regions = result.Rejony
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingRegions = false;
                console.error(error)
            });
    }

    var loadDepartmentList = function () {
        $scope.workplaceParams.loadingDepartments = true;
        $scope.workplaceParams.departments = []

        return menuContentService.getDepartmentsForFirm()
            .then(function (result) {
                $scope.workplaceParams.loadingDepartments = false;
                $scope.workplaceParams.departments = result.Wydzialy
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingDepartments = false;
                console.error(error)
            });
    }

    var loadAccounts5 = function () {
        $scope.workplaceParams.loadingAccouts5 = true;
        $scope.workplaceParams.accounts5 = [];

        return menuContentService.getAccounts5()
            .then(function (result) {
                $scope.workplaceParams.loadingAccouts5 = false;
                $scope.workplaceParams.accounts5 = result.pobraneKonta5
            })
            .catch(function (error) {
                $scope.workplaceParams.loadingAccouts5 = false;
                console.error(error)
            });
    }

    $scope.selectRow = function (workplace) {
        console.log(workplace, $scope.workplaceParams)
        if ($scope.selectedWorkplace === workplace) {
            $scope.selectedWorkplace = {};
        } else {
            $scope.selectedWorkplace = workplace
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

    $scope.upsertWorkplaceCtrl = function ($scope, $mdDialog, workplace, workplaceParams) {
        if (workplace) {
            $scope.modalResult = workplace;
        }

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
            return menuContentService.getSubDepartmets(department.Wydzial)
                .then(function (result) {
                    $scope.workplaceParams.loadingSubDepartments = false;
                    $scope.workplaceParams.subDepartments = result.PodWydzialy
                })
                .catch(function (error) {
                    $scope.workplaceParams.loadingSubDepartments = false;
                    console.error(error)
                });
        };

        $scope.validateIfProperAccount = function (account5skr) {
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

        $scope.querySearch = function (query) {
            var allAccounts5 = $scope.workplaceParams.accounts5
            return query ? allAccounts5.filter(createFilterFor(query)) : allAccounts5;
        }

        var createFilterFor = function (query) {
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(account) {
                return (account.Kontoskr.toLowerCase().indexOf(lowercaseQuery) === 0);
            };

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
                user: Object.assign({}, $scope.user)
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
                workplaceParams: $scope.workplaceParams
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
                workplaceParams: $scope.workplaceParams
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