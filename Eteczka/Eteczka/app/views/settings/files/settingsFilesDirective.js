'use strict';
angular.module('et.directives').directive('settingsFiles', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/files/settingsFiles.html',
        controller: 'settingsFilesController'
    }
});