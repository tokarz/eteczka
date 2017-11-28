'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'companiesService', 'filesViewService', 'modalService', 'sessionService', function ($scope, $state, companiesService, filesViewService, modalService, sessionService) {
    $scope.parameters = {
        company: '',
    };

    $scope.selectedStagedFile = null;
    $scope.createdMetaData = null;
    $scope.fileTypes = []
    $scope.employees = []
    $scope.fileDescription = {}

    var loadFileTypes = function () {
        filesViewService.getFileTypes().then(function (fileTypes) {
            $scope.fileTypes = fileTypes.PobraneDokumenty
        })
    }
    var loadEmployees = function () {
        filesViewService.getAllEmployees().then(function (employees) {
            $scope.employees = employees.Data.data
        })
    }

    loadFileTypes();
    loadEmployees()

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });


    $scope.upsertFileDescriptionCtrl = function ($scope, $mdDialog, description, fileTypes, employees, name) {
        if (description) {
            $scope.modalResult = description;
        }

        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.docPartOptions = ['A', 'B', 'C']
        $scope.modalResult.Dokwlasny = $scope.modalResult.Dokwlasny || $scope.yesNoOptions[0].value;
        $scope.modalResult.Nazwa = name;

        $scope.pracownikPesel = '';

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

        $scope.isDisabled = function () {
            return !$scope.modalResult.Typ || !$scope.isTypeWithDates($scope.modalResult.Typ.Symbol);
        }

        $scope.fillValidFromDate = function () {
            if (!$scope.modalResult.DataPocz && $scope.modalResult.DataWytworzenia) {
                $scope.modalResult.DataPocz = $scope.modalResult.DataWytworzenia
            }
        }

        $scope.isTypeWithDates = function (fileSymbol) {
            var type = fileTypes.find(function (file) {
                return file.Symbol === fileSymbol
            }).Typedycji

            if (type.trim() === 'b') {
                return true
            }
            else {
                return false
            }
        }

        var querySearch = function (arrayTosearchIn, key, query) {
            return query ? arrayTosearchIn.filter(createFilterFor(key, query)) : arrayTosearchIn;
        }

        $scope.fileTypeSearch = function (query) {
            return querySearch(fileTypes, "Symbol", query)
        }

        $scope.employeeSearch = function (query) {
            return querySearch(employees, "Nazwisko", query)
        }

        var createFilterFor = function (key, query) {
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(object) {
                return (object[key].toLowerCase().indexOf(lowercaseQuery) === 0);
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

    $scope.commitFile = function () {
        if ($scope.createdMetaData !== null) {
            filesViewService.commitFile($scope.createdMetaData).then(function (res) {
                if (res.success) {
                    modalService.alert('Zatwierdzanie pliku', 'Plik Zostal Dodany');
                    $state.reload();
                } else {
                    modalService.alert('Zatwierdzanie pliku', 'Blad! Plik Nie Zostal Dodany! Zweryfikuj dane i prawa dostepu lub skontaktuj sie z Administratorem');
                }
            });
        }
    }


    $scope.triggerUpsertFileDescriptionDialog = function () {
        var modalOptions = {
            body: 'app/views/files/addFile/fileDescriptionPopup/upsertFileDescription.html',
            controller: $scope.upsertFileDescriptionCtrl,
            locals: {
                description: $scope.fileDescription,
                fileTypes: $scope.fileTypes,
                employees: $scope.employees,
                name: $scope.selectedStagedFile.NazwaEad
            }
        };

        openModal(
            modalOptions,
            function (value) {
                $scope.createdMetaData = value;
            }
        )
    }

}]);