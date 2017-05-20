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


    $scope.hidePanel = false;

    $scope.elementSelected = {
        isSelected: false,
        elm: {
            Id: null
        },
        metadata: {
            CreationDate: '',
            ModificationDate: '',
            Size: '',
            PhysicalLocation: '',
            Type: '',
            Jrwa: ''
        }
    };

    $scope.usersForFile = [];

    $scope.deselectElement = function () {
        $scope.elementSelected.isSelected = false;
        $scope.elementSelected.elm = {
            Id: null
        };
        $scope.elementSelected.metadata = {
            CreationDate: '',
            ModificationDate: '',
            Size: '',
            PhysicalLocation: '',
            Type: '',
            Jrwa: ''
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


            httpService.get('Pliki/PobierzMetadane', { plik: elm.Nazwa }).then(function (metaDane) {
                $scope.elementSelected.metadata = metaDane.meta

                httpService.get('Pracownicy/PobierzDlaPliku', { id: elm.Pesel }).then(function (usersForFile) {
                    $scope.usersForFile = [usersForFile.users];

                    httpService.get('Statystyki/PobierzDaneWykresowe').then(function (result) {
                        $scope.chartDataForSelection = result.chartdata;

                    });
                    $scope.startPreview = false;
                });
            });
        });
    }

    $scope.previewPdf = function (elm) {
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

    $scope.chartDataForSelection = [
               
    ];

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