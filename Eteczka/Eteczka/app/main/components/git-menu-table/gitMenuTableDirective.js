'use strict';
angular.module('et.directives').directive('gitMenuTable', function () {
    return {
        restrict: 'E',
        scope: {
            company: '=',
        },
        controller: 'gitMenuTableController',
        templateUrl: 'app/main/components/git-menu-table/gitMenuTable.html'
    };
});


