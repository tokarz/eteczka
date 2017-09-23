﻿'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', 'menuContentService', 'modalService', 'peselService', 'utilsService', function ($scope, menuContentService, modalService, peselService, utilsService) {
    $scope.$watch('user', function (value) {
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                $scope.workplaces = result.MiejscaPracy;
            });
        }
    });

    $scope.$watch('company', function (value) {
    $scope.$watch('firmparams.selectedfirm', function (value) {
    $scope.workplaceParams = {

    var loadRegionList = function (company) {
        return menuContentService.getRegionsForFirm(company)
    var loadDepartmentList = function (company) {

    $scope.selectedWorkplace = {};

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
                }
                }

                var modalOptions = {
                    modalOptions,
                    function (value) {
                        triggerShortPasswordCheck()
                            .then(function (isShortPasswordCorrect) {
                                if (typeof isShortPasswordCorrect === 'undefined') {
                                }

                                if (isShortPasswordCorrect === false) {
                                }

                                console.log('tu bedzie wywolanie funkcji usuwania pracownika', value)
                            })
                            .catch(console.error)
                    },
                    $scope.user
                )
            })

    var triggerAdminPasswordCheck = function() {
        var modalOptions = {
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
        loadSubDepartmentList: function (department) {
            $scope.workplaceParams.loadingSubDepartments = true;
        }
    }

    $scope.triggerAddWorkplaceDialog = function () {
        var modalOptions = {
            title: 'Dodawanie nowego miejca pracy pracownika',
            body: 'app/views/employees/editWorkplacesPopup/upsertWorkplaceModal.html',
            availableRegions: $scope.workplaceParams.regions,
            availableDepartments: $scope.workplaceParams.departments
        }

        openModal(
            Object.assign(modalOptions, upsertWorkplaceModalCommonOptions),
            function (value) { console.log('tu bedzie wywolanie funkcji dodawania miejsca pracy', value) },
            { Firma: 'AFM' }
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
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji usuwania miejsca pracy', value) },
            $scope.selectedWorkplace
        )
}]);