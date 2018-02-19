'use strict';
angular.module('et.directives').directive('adminSessions', function () {
    return {
        scope: {},
        templateUrl: 'app/views/settings/sessions/admin/adminSessions.html',
        controller: 'adminSessionsController'
    }
});