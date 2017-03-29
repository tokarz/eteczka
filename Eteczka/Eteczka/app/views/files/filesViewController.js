'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', function ($scope, $state) {
    $scope.files = [
        { date: '2011-11-11', name: 'FolienMisko.pdf', type: 'Szkolenie' },
         { date: '2011-11-12', name: 'Tiguan.pdf', type: 'Oferta' }
    ];

    $scope.selectedPdf = '';

    $scope.previewPdf = function (elm) {
        $('#pdfPreviewer').attr('src', 'FILE_FETCH?src=' + elm.name + '.fetchfile');
    }

    $scope.goBack = function () {
        $state.go('options');
    }

}]);