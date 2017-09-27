'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', 'companiesService', 'filesViewService', 'modalService', function ($scope, companiesService, filesViewService, modalService) {
    $scope.parameters = {
        company: '',
    };

    $scope.selectedStagedFile = null;

    $scope.fileTypes = []

    var loadFileTypes = function () {
        filesViewService.getFileTypes().then(function (fileTypes) {
            console.log(fileTypes)
            $scope.fileTypes = fileTypes.PobraneDokumenty
        })
    }

    loadFileTypes()

    companiesService.getActiveCompany().then(function (result) {
        $scope.parameters.company = result.firma;
    });


    $scope.upsertFileDescriptionCtrl = function ($scope, $mdDialog, description, fileTypes) {
        if (description) {
            $scope.modalResult = description;
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

        $scope.isTypeWithDates = function (fileSymbol) {
            console.log(fileTypes)
            var type = fileTypes.find(function (file) {
                console.log(file.Symbol, fileSymbol)
                return file.Symbol ===  fileSymbol
            }).Typedycji
            console.log(type)

            if (type.trim() === '2') {
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
                description: $scope.modalResult,
                fileTypes: $scope.fileTypes
            }
        };

        openModal(
            modalOptions,
            function (value) {
                console.log('tu bedzie wywolanie funkcji opisu plikow', value);
            }
        )
    }

}]);