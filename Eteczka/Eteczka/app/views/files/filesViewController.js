'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', 'httpService', function ($scope, $state, httpService) {
    $scope.files = [
        { date: '2011-11-11', name: 'FolienMisko.pdf', type: 'Szkolenie' },
         { date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' }
    ];

    //$('#pdfPreviewer').attr('src', 'Content/img/logo9.png');

    $scope.previewPdf = function (elm) {
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