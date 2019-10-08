'use strict';
angular.module('et.controllers').controller('menuDepartmentContentController', ['$scope', function ($scope) {
    $scope.triggerAddDepartment = function () {
        console.log('implement adding new department')
    };
     $scope.triggerEditDepartment = function () {
        console.log('implement editing department')
    };
     $scope.triggerDeleteDepartment = function () {
        console.log('implement deleting department')
    };
}]);