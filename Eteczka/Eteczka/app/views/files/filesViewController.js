'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'httpService', function ($scope, $state, httpService) {
    $scope.files = [
        { id: 0, date: '2011-11-11', name: 'FolienMisko.pdf', type: 'Szkolenie' },
         { id: 1, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 2, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 3, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 4, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 5, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 5, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 6, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 7, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 8, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 8, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 8, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' },
         { id: 9, date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' }
    ];

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

    $scope.fillInUserData = function (user) {

    }

    $scope.hidePanel = false;

    $scope.togglePanel = function () {
        $scope.hidePanel = !$scope.hidePanel;

        if ($scope.hidePanel) {
            $('#lowerFilePanel').css('height', '0');
            $('#upperFilePanel').css('height', '98%');
        } else {
            $('#upperFilePanel').css('height', '49%');
            $('#lowerFilePanel').css('height', '49%');
        }

    };


    $scope.elementSelected = {
        isSelected: false,
        elm: {
            id: null
        }
    };

    $scope.usersForFile = [];

    $scope.deselectElement = function () {
        $scope.elementSelected.isSelected = false;
        $scope.elementSelected.elm = {
            id: null
        }
        $('#pdfPreviewer').attr('data', '');
    };

    $scope.selectElement = function () {
        $scope.elementSelected.isSelected = true;
        $scope.elementSelected.elm = elm;
        httpService.get('Resources/GetRestrictedResource', {
            fileName: elm.name
        }).then(function (result) {
            $('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.data);
        });
    }

    //$('#pdfPreviewer').attr('src', 'Content/img/logo9.png');

    $scope.previewPdf = function (elm) {
        $scope.usersForFile = [$scope.allUsersForFile[elm.id]];
        if ($scope.elementSelected.elm.id === elm.id) {
            $scope.deselectElement();
        } else {
            $scope.selectElement();
        }


    }

    $scope.goBack = function () {
        $state.go('options');
    }

    $scope.isSubMenuVisible = function (panel) {
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

}]);