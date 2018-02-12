'use strict';
angular.module('et.directives').directive('menuTable', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '=',
            selectedrow: '=',
            tabs: '=',
            activetab: '=',
            search: '=',
            loading: '=',
            placeholder: '@'
        },
        controller: 'menuTableController',
        templateUrl: 'app/main/components/menu-table/menuTable.html'
    };
});


