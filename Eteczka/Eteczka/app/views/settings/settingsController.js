'use strict';
angular.module('et.controllers').controller('settingsController', ['$scope', 'settingsService', function ($scope, settingsService) {

    $scope.importFiles = function () {
        alert('import start!');
        settingsService.importFiles(true).then(function () {
            alert('successfull');
        },
        function () {
            alert('failed!');
        });
    }


}]);