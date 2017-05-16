'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'httpService', 'filesViewService', 'editEmployeeService', function ($scope, $state, httpService, filesViewService, editEmployeeService) {
    $scope.files = [];
    $scope.addFileTemplate = {
        title: 'Dodawanie nowego Pliku',
        body: 'app/views/files/addFile/addFile.html',
        parameters: []
    };

    filesViewService.getAllFiles().then(function (res) {
        if (res && res.pliki) {
            $scope.files = res.pliki
        }
    });

    $scope.allUsersForFile = {
        '0': { date: '1410-01-01', name: 'test', type: '01' },
        '1': { date: '1410-01-02', name: 'test', type: 'X2' },
        '2': { date: '1410-01-03', name: 'test', type: 'A3' },
        '3': { date: '1410-01-04', name: 'test', type: 'X4' },
        '4': { date: '1410-01-05', name: 'test', type: 'X5' },
        '5': { date: '1410-01-06', name: 'test', type: 'X6' },
        '6': { date: '1410-01-07', name: 'test', type: 'X7' },
        '7': { date: '1410-01-08', name: 'test', type: 'X8' },
        '8': { date: '1410-01-09', name: 'test', type: 'X9' },
        '9': { date: '1410-01-10', name: 'test', type: 'X0' }
    }

    $scope.hidePanel = false;

    $scope.elementSelected = {
        isSelected: false,
        elm: {
            Id: null
        }
    };

    $scope.usersForFile = [];

    $scope.deselectElement = function () {
        $scope.elementSelected.isSelected = false;
        $scope.elementSelected.elm = {
            Id: null
        }
        $('#pdfPreviewer').attr('data', '');
    };

    $scope.selectElement = function (elm) {
        $scope.elementSelected.isSelected = true;
        $scope.startPreview = true;
        $scope.elementSelected.elm = elm;
        httpService.get('Resources/GetRestrictedResource', {
            fileName: elm.Nazwa
        }).then(function (result) {

            $('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.data);
            $scope.startPreview = false;
        });
    }

    //$('#pdfPreviewer').attr('src', 'Content/img/logo9.png');

    $scope.previewPdf = function (elm) {
        $scope.usersForFile = [$scope.allUsersForFile[elm.Id]];
        if ($scope.elementSelected.elm.Id === elm.Id) {
            $scope.deselectElement();
        } else {
            $scope.selectElement(elm);
        }
    }

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.isSubMenuVisible = function (panel) {
        if (panel === 'lowerLeft') {
            if ($scope.elementSelected.isSelected) {
                return 'col-md-6'
            } else {
                return 'col-md-12'
            }

        } else if (panel === 'lowerRight') {
            if ($scope.elementSelected.isSelected) {
                return 'col-md-6'
            } else {
                return 'not-visible'
            }
        }
        else {
            if ($scope.elementSelected.isSelected && panel === 'lower') {
                return 'lowerPanelVisible';

            } else if ($scope.elementSelected.isSelected && panel === 'upper') {
                return 'upperPanelVisible'

            } else if (!$scope.elementSelected.isSelected && panel === 'lower') {
                return 'lowerPanelInvisible';
            } else {
                return 'upperPanelInvisible';
            }

        }

    }

    $scope.ok = function () {
        alert('OK LOCAL');
    }

    $scope.tryAddFile = function () {
        $scope.addFileTemplate.parameters = [
            { jrwa: 'xxx' },
            { files: ['Umowa', 'Szkolenie', 'BHP', 'Certyfikat'] }
        ];
        editEmployeeService.showModal($scope.addFileTemplate).then(function (result) {
            alert('Result ' + result.selectedFile)
        }).catch(function (error) {
            console.log("error found!");
        });

    }


}]);