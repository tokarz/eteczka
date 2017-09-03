'use strict';
angular.module('et.directives').directive('header', function () {
    return {
        restrict: 'E',
        controller: 'headerController',
        templateUrl: 'app/main/header/header.html'
    }
});