'use strict';
angular.module('et.directives').directive('employeesView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'employeesViewController',
        templateUrl: 'app/views/employees/employeesView.html'
    }

});