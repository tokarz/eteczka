'use strict';
angular.module('et.directives').directive('filesView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'filesViewController',
        templateUrl: 'app/views/files/filesView.html'
    }

});