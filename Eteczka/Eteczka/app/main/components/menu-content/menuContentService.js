'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (pracownik) {
            return httpService.get('MiejscePracy/MiejscePracyDlaPracownika', {
                sessionId: sessionService.getSessionId(),
                numeread: pracownik.Numeread
            });
        },
        getActiveCompany: function (sessionId) {
            return httpService.get('Firmy/PobierzAktywnaFirme', { sessionId: sessionId })
        },
        getRegionsForFirm: function (sessionId) {
            return httpService.get('Rejony/PobierzRejonyDlaWybranejFirmy', { sessionId: sessionId })
        },
        getDepartmentsForFirm: function (sessionId) {
            return httpService.get('Wydzialy/PobierzWydzialy', { sessionId: sessionId })
        },
        getSubDepartmets: function (sessionId, department) {
            return httpService.get('PodWydzial/PobierzWszystkiePodwydzialy', {
                sessionId: sessionId,
                wydzial: department
            });
        },
        getAccounts5: function (sessionId) {
            return httpService.get('Konto5/PobierzKonta5', {
                sessionId: sessionId
            });
        }
        
    }
}]);