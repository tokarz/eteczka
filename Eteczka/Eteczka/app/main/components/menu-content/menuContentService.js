'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (pracownik) {
            return httpService.get('MiejscePracy/MiejscePracyDlaPracownika', {
                sessionId: sessionService.getSessionId(),
                numeread: pracownik.Numeread
            });
        },
        getRegionsForFirm: function (firm) {
            return httpService.get('Rejony/PobierzRejonyDlaWybranejFirmy', { firma: firm })
        },
        getDepartmentsForFirm: function (firm) {
            return httpService.get('Wydzialy/PobierzWydzialy', { firma: firm })
        }
    }
}]);