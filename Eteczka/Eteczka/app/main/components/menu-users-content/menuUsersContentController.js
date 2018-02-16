'use strict';
angular.module('et.controllers').controller('menuUsersContentController', ['$scope', '$state', function ($scope, $state) {
    $scope.company = null;

    $scope.$watch('user', function (user) {
        if (user) {

            $scope.fullDetailsForUser = $scope.details.find(function (detail) {
                return detail.DaneUzytkownika.Identyfikator === user.Identyfikator
            });

            $scope.userDetails = $scope.fullDetailsForUser.Detale;
        }
    })

}]);