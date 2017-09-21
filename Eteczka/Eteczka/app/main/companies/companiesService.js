'use strict';
angular.module('et.services').factory('companiesService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Firmy/PobierzWszystkieFirmy', {
                sessionId: sessionService.getSessionId()
            });
        },
        setActiveCompany: function (company) {
            return httpService.get('Firmy/UstawAktywnaFirme', {
                sessionId: sessionService.getSessionId(),
                company: company
            });
        },
        getActiveCompany: function () {
            return httpService.get('Firmy/PobierzAktywnaFirme', {
                sessionId: sessionService.getSessionId(),
            });
        }
    }
}]);