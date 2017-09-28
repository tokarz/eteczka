'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', 'companiesService', 'filesViewService', 'modalService', 'sessionService', function ($scope, companiesService, filesViewService, modalService, sessionService) {
    $scope.parameters = {
        company: '',
    };

    $scope.selectedStagedFile = null;

    $scope.fileTypes = []
    $scope.fileDescription = {}

    var loadFileTypes = function () {
        filesViewService.getFileTypes().then(function (fileTypes) {
            console.log(fileTypes)
            $scope.fileTypes = fileTypes.PobraneDokumenty
        })
    }

    loadFileTypes();

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });


    $scope.upsertFileDescriptionCtrl = function ($scope, $mdDialog, description, fileTypes) {
        if (description) {
            $scope.modalResult = description;
        }
        console.log(fileTypes)

        $scope.yesNoOptions = [{ name: 'TAK', value: true }, { name: 'NIE', value: false }]
        $scope.modalResult.Dokwlasny = $scope.modalResult.Dokwlasny || $scope.yesNoOptions[0]
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
            return !$scope.modalResult.fileType || !$scope.isTypeWithDates($scope.modalResult.fileType.Symbol);
        }

        $scope.findUserByPesel = function () {
            if ($scope.pracownikPesel.length === 11) {
                filesViewService.findEmployee($scope.pracownikPesel).then(function (pracownik) {
                    if (Array.isArray(pracownik.Pracownicy)) {
                        $scope.modalResult.Pracownik = pracownik.Pracownicy[0]
                    }
                    else {
                        $scope.modalResult.Pracownik = {}
                    }
                });
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

        $scope.querySearch = function (query) {
            return query ? fileTypes.filter(createFilterFor(query)) : fileTypes;
        }

        var createFilterFor = function (query) {
            var lowercaseQuery = angular.lowercase(query);

            return function filterFn(fileType) {
                return (fileType.Symbol.toLowerCase().indexOf(lowercaseQuery) === 0);
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

    $scope.triggerUpsertFileDescriptionDialog = function () {
        var modalOptions = {
            body: 'app/views/files/addFile/fileDescriptionPopup/upsertFileDescription.html',
            controller: $scope.upsertFileDescriptionCtrl,
            locals: {
                description: $scope.fileDescription,
                fileTypes: $scope.fileTypes
            }
        };

        openModal(
            modalOptions,
            function (value) {
                console.log('tu bedzie wywolanie funkcji opisu plikow', value);
                $scope.fileDescription = value
            }
        )
    }

}]);