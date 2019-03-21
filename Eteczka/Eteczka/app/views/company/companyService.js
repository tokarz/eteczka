'use strict';

angular.module('et.services').factory('companyService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Firmy/PobierzWszystkieFirmy', {
                sessionId: sessionService.getSessionId()
            });
        }
    };
}]);