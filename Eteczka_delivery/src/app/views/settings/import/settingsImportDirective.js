'use strict';
angular.module('et.directives').directive('settingsImport', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/import/settingsImport.html',
        controller: 'settingsImportController'
    }
});