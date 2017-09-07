'use strict';
angular.module('et.directives').directive('gitMenuTable', function () {
    return {
        restrict: 'E',
        scope: {
            newrows: '=',
            stagedrows: '=',
            loading: '='
        },
        controller: 'gitMenuTableController',
        templateUrl: 'app/main/components/git-menu-table/gitMenuTable.html'
    };
});


