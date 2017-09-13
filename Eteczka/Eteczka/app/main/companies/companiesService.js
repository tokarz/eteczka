'use strict';
angular.module('et.services').factory('companiesService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Firmy/PobierzWszystkieFirmy', {
                sessionId: sessionService.getSessionId()
            });
        }
    }
}]);