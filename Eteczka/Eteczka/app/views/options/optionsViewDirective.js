'use strict';
angular.module('et.directives').directive('optionsView', function () {
    return {
        restrict: 'E',
        scope: {},
        controller: 'optionsViewController',
        templateUrl: 'app/views/options/optionsView.html'
    }

});