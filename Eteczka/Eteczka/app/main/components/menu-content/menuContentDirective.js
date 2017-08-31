'use strict';
angular.module('et.directives').directive('menuContent', function () {
    return {
        restrict: 'E',
        scope: {
            user: '='
        },
        controller: 'menuContentController',
        templateUrl: 'app/main/components/menu-content/menuContentView.html'
    };

});