'use strict';
angular.module('et.controllers').controller('filesController', ['$scope', '$state', function ($scope, $state) {
    $scope.goToFileAdd = function () {
        $state.go('files');
    };

}]);