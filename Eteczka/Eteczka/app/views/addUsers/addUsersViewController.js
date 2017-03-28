'use strict'
angular.module('et.controllers').controller('addUsersViewController', ['$scope', function ($scope) {

    $scope.userToAdd = {
        username: '',
        password: '',
        retypedPassword: ''
    };

    $scope.generatePassword = function () {
        //implement me
    }

}]);
