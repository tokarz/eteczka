'use strict';
angular.module('et.controllers').controller('addFileController', ['$scope', 'httpService', function ($scope, httpService) {
    $scope.isElementChosen = false;

    $scope.wczytajPlik = function () {
        $('#addFileIO').click();
    }

    $scope.modalResult.jrwa = $scope.modalOptions.selectedElement.isSelected ? $scope.modalOptions.selectedElement.metadata.Jrwa : '';
    $scope.modalResult.files = ['A', 'B'];
    $scope.modalResult.size = $scope.modalOptions.selectedElement.isSelected ? $scope.modalOptions.selectedElement.metadata.Size : '';

    $scope.selectedFile = $scope.modalResult[0];
    $scope.upload = {};

    $('#uploadFile').on('change', function () {
        $scope.upload.visiblePath = $('#uploadFile')[0].files[0].name;
        $scope.upload.files = $('#uploadFile')[0].files;
        $scope.isElementChosen = true;

        $scope.showUploadedFile($scope.upload.files);
        httpService.get('Pliki/PobierzMetadane', { name: $scope.upload.visiblePath }).then(function (result) {
            $scope.modalResult = result.meta;
        });
    });

    $scope.showFileDialog = function () {
        $('#uploadFile').click();
    }

    $scope.selectType = function (newtype) {
        $scope.modalResult.jrwa = 'Something New!';
    }

    $scope.onFilesDropped = function ($files, $event) {
        $scope.upload.visiblePath = $files.name;
        $scope.upload.files = $files;
        $scope.isElementChosen = true;

        $scope.showUploadedFile($files);
        httpService.get('Pliki/PobierzMetadane', { name: $scope.upload.visiblePath }).then(function (result) {
            $scope.modalResult = result.meta;
        });
    }

    $scope.$on('$destroy', function () {
        $('#uploadFile').off('click');
    });

    $scope.showUploadedFile = function (file) {
        $('.pdfPreviewer').attr('data', window.URL.createObjectURL(file[0]));
    }

    if ($scope.modalOptions.isEdit) {
        httpService.get('Resources/GetRestrictedResource', {
            fileName: $scope.modalOptions.selectedElement.file.Nazwa
        }).then(function (result) {

            $('.pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.data);

            httpService.get('Pracownicy/PobierzDlaPliku', { id: $scope.modalOptions.selectedElement.file.Pesel }).then(function (usersForFile) {
                $scope.modalResult.eployeeToModify = {
                    pesel: usersForFile.users ? [usersForFile.users][0].PESEL : '',
                    name: usersForFile.users ? [usersForFile.users][0].Nazwisko : ''
                }
            });

        });
    }


}]);