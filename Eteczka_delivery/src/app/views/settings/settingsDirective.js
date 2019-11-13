'use strict';
angular.module('et.directives').directive('settings', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/settingsView.html',
        controller: 'settingsViewController'
    }
});