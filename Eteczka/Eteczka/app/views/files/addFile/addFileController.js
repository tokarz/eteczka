'use strict';
angular.module('et.controllers').controller('addFileController', ['$scope', function ($scope) {
    $scope.isElementChosen = false;

    $scope.wczytajPlik = function () {
        $scope.isElementChosen = true;
    }

    $scope.showFileDialog = function () {
        $('#uploadFile').click();
    }
}]);