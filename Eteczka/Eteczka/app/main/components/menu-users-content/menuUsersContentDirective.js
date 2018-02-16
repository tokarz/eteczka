'use strict';
angular.module('et.directives').directive('menuUsersContent', function () {
    return {
        restrict: 'E',
        scope: {
            user: '=',
            details: '='
        },
        controller: 'menuUsersContentController',
        templateUrl: 'app/main/components/menu-users-content/menuUsersContentView.html'
    };

});