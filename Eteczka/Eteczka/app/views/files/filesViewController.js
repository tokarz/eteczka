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
        '0': { date: '1410-01-01', name: 'test', type: 'X' },
        '1': { date: '1410-01-02', name: 'test', type: 'X' },
        '2': { date: '1410-01-03', name: 'test', type: 'X' },
        '3': { date: '1410-01-04', name: 'test', type: 'X' },
        '4': { date: '1410-01-05', name: 'test', type: 'X' },
        '5': { date: '1410-01-06', name: 'test', type: 'X' },
        '6': { date: '1410-01-07', name: 'test', type: 'X' },
        '7': { date: '1410-01-08', name: 'test', type: 'X' },
        '8': { date: '1410-01-09', name: 'test', type: 'X' },
        '9': { date: '1410-01-10', name: 'test', type: 'X' }
    }

    $scope.fillInUserData = function (user) {

    }

    $scope.usersForFile = [];

    //$('#pdfPreviewer').attr('src', 'Content/img/logo9.png');

    $scope.previewPdf = function (elm) {
        $scope.usersForFile = [$scope.allUsersForFile[elm.id]];

        $('#pdfPreviewer').addClass('processing');
        httpService.get('Resources/GetRestrictedResource', {
            fileName: elm.name
        }).then(function (result) {
            $('#pdfPreviewer').removeClass('processing');
            $('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.data);
        });
    }

    $scope.goBack = function () {
        $state.go('options');
    }

}]);