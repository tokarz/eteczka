'use strict';
angular.module('et.directives').directive('settingsSessions', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/sessions/settingsSessions.html',
        controller: 'settingsSessionsController'
    }
});