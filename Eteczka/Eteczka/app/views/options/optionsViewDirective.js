'use strict';
angular.module('et.directives').directive('optionsView', function () {
    return {
        restrict: 'E',
        scope: {
            options: '='
        },
        controller: 'optionsViewController',
        templateUrl: 'app/views/options/optionsView.html'
    }

});