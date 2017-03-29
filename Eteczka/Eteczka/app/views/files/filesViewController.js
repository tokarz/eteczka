'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', function ($scope, $state) {
    $scope.files = [
        { date: '2011-11-11', name: 'FolienMisko.pdf' }
    ];

    $scope.selectedPdf = 'FolienMisko.pdf';

    $scope.previewPdf = function (elm) {
        $scope.selectedPdf = elm.name;
    }

    $scope.goBack = function () {
        $state.go('options');
    }

}]);