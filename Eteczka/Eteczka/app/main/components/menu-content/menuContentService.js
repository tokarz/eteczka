'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (user) {
            return httpService.post('MiejscePracy/MiejscePracyDlaPracownika?sessionId=' + sessionService.getSessionId(), {
                user: user
            });
        }
    }
}]);