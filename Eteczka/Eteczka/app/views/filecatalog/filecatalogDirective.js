'use strict';
angular.module('et.directives').directive('fileCat', function () {
    return {
        scope: {},
        controller: 'fileCatController',
        templateUrl: 'app/views/filecatalog/fileCatView.html'
    }

});