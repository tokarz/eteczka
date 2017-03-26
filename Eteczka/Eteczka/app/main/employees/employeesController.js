'use strict';
angular.module('et.controllers').controller('employeesController', ['$scope', '$state', function ($scope, $state) {
    $scope.goToUsers = function () {
        $state.go('employees');
    }

}]);