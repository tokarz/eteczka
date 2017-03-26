'use strict';
angular.module('et.directives').directive('employeesView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'employeesViewController',
        templateUrl: 'app/views/employees/employeesView.html'
    }

});