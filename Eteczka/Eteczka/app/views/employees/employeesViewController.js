'use strict';
angular.module('et.controllers').controller('employeesViewController', ['$scope', '$state', 'utilsService', function ($scope, $state, utilsService) {

    $scope.userPesel = '';
    $scope.userSex = 'M';
    $scope.isPeselCorrect = false;

    $scope.checkPesel = function () {
        utilsService.isPeselValid($scope.userPesel, $scope.userSex).then(function (result) {
            $scope.isPeselCorrect = result.data.valid;
        }, function () {
            $scope.isPeselCorrect = false;
        });
    }

    $scope.validatePesel = function () {
        $scope.checkPesel();
    }

    $scope.goBack = function () {
        $state.go('options');
    }

}]);