'use strict';
angular.module('et.directives').directive('menuTable', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '=',
            tabs: '=',
            user: '=',
            search: '='
        },
        controller: 'menuTableController',
        templateUrl: 'app/main/components/menu-table/menuTable.html'
    };
});


