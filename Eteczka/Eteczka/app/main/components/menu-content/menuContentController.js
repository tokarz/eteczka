'use strict';
angular.module('et.controllers').controller('menuContentController', ['$scope', 'menuContentService', 'modalService', function ($scope, menuContentService, modalService) {

    $scope.$watch('user', function (value) {
        console.log('watching user', value)
        if (value && value !== {}) {
            menuContentService.getUserWorkplaces(value).then(function (result) {
                console.log('result', result)
                $scope.workplaces = result.MiejscaPracy;
            });
        }
    });

    var openModal = function (modalOptions, executor, user) {
        modalService.showModal(modalOptions, user)
            .then(function (result) { executor(result) })
            .catch(function (error) {
                if (error !== 'cancel' && error !== 'backdrop click') {
                    console.log("error found!", error);
                }
            });
    }

    $scope.triggerAddEmployeeDialog = function () {
        var modalOptions = {
            title: 'Dodawanie nowego pracownika',
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html'

        }

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji dodawania pracownika', value) }
        )
    }

    $scope.triggerEditEmployeeDialog = function () {
        var modalOptions = {
            title: 'Edytowanie pracownika',
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html'
        }
        var userToPass = Object.assign({}, $scope.user)

        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji edytowania pracownika', value) },
            userToPass
        )
    }

    $scope.triggerDeleteEmployeePopup = function () {        var modalOptions = {            title: 'Usuwanie pracownika z bazy danych',            body: 'app/views/employees/editEmployeesPopup/deleteUserModal.html'        }        openModal(
            modalOptions,
            function (value) { console.log('tu bedzie wywolanie funkcji usuwania pracownika', value) },
            $scope.user
        )    }
}]);