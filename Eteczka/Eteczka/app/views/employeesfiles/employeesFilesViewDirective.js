'use strict';
angular.module('et.directives').directive('employeesFilesView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'employeesViewController',
        templateUrl: 'app/views/employeesfiles/employeesFilesView.html'
    }

});