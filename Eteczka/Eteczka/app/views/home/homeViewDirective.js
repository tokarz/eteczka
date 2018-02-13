'use strict';
angular.module('et.directives').directive('homeView', function () {
    return {
        restrict: 'E',
        scope: {
           
        },
        controller: 'homeViewController',
        templateUrl: 'app/views/home/homeView.html'
    }

});