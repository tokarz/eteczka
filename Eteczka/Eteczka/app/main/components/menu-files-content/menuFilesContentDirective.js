'use strict';
angular.module('et.directives').directive('menuFilesContent', function () {
    return {
        restrict: 'E',
        scope: {
            user: '=',
            rows: '=',
            toolbar: '=',
            emptymessage: '@',
            shownames: '=',
            hasSummary: '='
        },
        controller: 'menuFilesContentController',
        templateUrl: 'app/main/components/menu-files-content/menuFilesContentView.html'
    };

});