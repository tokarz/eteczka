'use strict';
angular.module('et.directives').directive('loginView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'loginViewController',
        templateUrl: 'app/views/login/loginView.html'
    }

});