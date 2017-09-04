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

    $scope.triggerUpsertEmployeeDialog = function () {
        var modalOptions = {
            title: 'Dodawanie nowego pracownika',
            body: 'app/views/employees/editEmployeesPopup/upsertUserModal.html'

        }
        console.log('should display employee dialog')

        // modalService.openModal(modalOptions, 'default-modal-body')

        modalService.showModal(modalOptions, $scope.elementSelected).then(function (result) {
            // do wywyołania funkcja z serwisu pracownika - edytujPracownika
            console.log(result)
        }).catch(function (error) {
            console.log("error found!");
        });
    } 
}]);