'use strict';
angular.module('et.controllers').controller('filesViewController', ['$scope', '$state', function ($scope, $state) {
    $scope.files = [
        {date: '2011-11-11', name: 'CV.pdf'}
    ];


    $scope.goBack = function () {
        $state.go('options');
    }

}]);