'use strict';
angular.module('et.directives').directive('menuDepartmentContent', function () {
    return {
        restrict: 'E',
        scope: {
            user: '=',
            rows: '=',
            toolbar: '=',
            selectedRow: '=',
            emptymessage: '@',
            shownames: '=',
            hasSummary: '='
        },
        controller: 'menuDepartmentContentController',
        templateUrl: 'app/main/components/menu-department-content/menuDepartmentContentView.html'
    };

});