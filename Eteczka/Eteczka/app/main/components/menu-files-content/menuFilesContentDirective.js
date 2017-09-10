'use strict';
angular.module('et.directives').directive('menuFilesContent', function () {
    return {
        restrict: 'E',
        scope: {
            user: '='
        },
        controller: 'menuFilesContentController',
        templateUrl: 'app/main/components/menu-files-content/menuFilesContentView.html'
    };

});