'use strict';
angular.module('et.directives').directive('adminView', function () {
    return {
        restrict: 'E',
        scope: {
           
        },
        controller: 'adminViewController',
        templateUrl: 'app/views/admin/adminView.html'
    }

});