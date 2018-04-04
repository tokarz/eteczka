'use strict';
angular.module('et.controllers').controller('employeesFilesController', ['$scope', 'modalService', function ($scope, modalService) {
    $scope.tabs = [
        { Id: 0, Name: 'Zatrudnieni' },
        { Id: 1, Name: 'Wszyscy' },
        { Id: 2, Name: 'Archiwum' }
    ];

    $scope.parameters = {
        user: {},
        tabs: $scope.tabs,
        activeTab: $scope.tabs[0],
        searchTerm: '',
        employees: [],
        loading: false
    };

    $scope.startProcessing = function () {
        $scope.parameters.loading = true;
        $scope.parameters.employees = [];
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

    $scope.triggerEditEmployeeFileDialog = function () {
        var modalOptions = {
            body: 'app/views/employeefile/editEmployeeFileModal.html',
            controller: $scope.editEmployeeFileCtrl,
            locals: {
                employeeFile: $scope.parameters.user, // do sprawdzenia jaki obiekt mozemy przeslac do modala (sam plik czy zbior plikow)
            }
        };

        openModal(
            modalOptions,
            function (value) {
                // do dodania egzekutor po zamknieciu modala
            }
        )
    }

    $scope.editEmployeeFileCtrl = function ($scope, $mdDialog, employeeFile) {
        if (description) {
            $scope.modalResult = employeeFile; // na razie przesylamy usera, jesli nie da rady zmienic na plik, do zmiany tutaj
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

        $scope.fillValidFromDate = function () {
            if (!$scope.modalResult.DataPocz && $scope.modalResult.DataWytworzenia) {
                $scope.modalResult.DataPocz = $scope.modalResult.DataWytworzenia
            }
        }

        $scope.isDisabled = function () {
            return !$scope.modalResult.DataKoniec;
        }
    }
}]);