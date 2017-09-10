'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$scope', function ($scope) {
    $scope.selectedFile = null;

    $scope.userFiles = [
        {
            Type: 'Badania Lekarskie',
            DataDok: '2011-11-11',
            DataPocz: '2012-12-12',
            DataKon: '2013-12-12',
            checked: false
        },
        {
            Type: 'Badania Lekarskie2',
            DataDok: '2011-11-11',
            DataPocz: '2012-12-12',
            DataKon: '2013-12-12',
            checked: false
        },
        {
            Type: 'Badania Lekarskie3',
            DataDok: '2011-11-11',
            DataPocz: '2012-12-12',
            DataKon: '2013-12-12',
            checked: false
        }
    ];


    $scope.selectFile = function (file) {
        if ($scope.selectedFile === file) {
            $scope.selectedFile = null;
        } else {
            $scope.selectedFile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedFile) {
            result += ' active-row';
        }

        return result;
    }

}]);