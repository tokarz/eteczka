'use strict';
angular.module('et.directives').directive('choosefirmView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'chooseFirmController',
        templateUrl: 'app/views/login/choosefirm/chooseFirm.html'
    }

});