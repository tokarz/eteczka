'use strict';
angular.module('et.directives').directive('menu-table', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '='
        },
        templateUrl: 'app/main/components/menu-table/menuTable.html'
    };
});