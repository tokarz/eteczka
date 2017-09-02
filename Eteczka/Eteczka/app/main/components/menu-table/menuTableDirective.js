'use strict';
angular.module('et.directives').directive('menuTable', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '=',
            tabs: '=',
            activetab: '=',
            user: '=',
            search: '=',
            loading: '='
        },
        controller: 'menuTableController',
        templateUrl: 'app/main/components/menu-table/menuTable.html'
    };
});


