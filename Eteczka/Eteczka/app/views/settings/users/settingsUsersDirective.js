'use strict';
angular.module('et.directives').directive('settingsUsers', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/users/settingsUsers.html',
        controller: 'settingsUsersController'
    }
});