'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (pracownik) {
            return httpService.get('MiejscePracy/MiejscePracyDlaPracownika', {
                sessionId: sessionService.getSessionId(),
                numeread: pracownik.Numeread
            });
        },
        getActiveCompany: function () {
            return httpService.get('Firmy/PobierzAktywnaFirme', { sessionId: sessionService.getSessionId() })
        },
        getRegionsForFirm: function () {
            return httpService.get('Rejony/PobierzRejonyDlaWybranejFirmy', { sessionId: sessionService.getSessionId() })
        },
        getDepartmentsForFirm: function () {
            return httpService.get('Wydzialy/PobierzWydzialy', { sessionId: sessionService.getSessionId() })
        },
        getSubDepartmets: function (department) {
            return httpService.get('PodWydzial/PobierzWszystkiePodwydzialy', {
                sessionId: sessionService.getSessionId(),
                wydzial: department
            });
        },
        getAccounts5: function () {
            return httpService.get('Konto5/PobierzKonta5', {
                sessionId: sessionService.getSessionId()
            });
        },
        isProperAdminPassword: function (password) {
            return httpService.get('Osys/checkHasloAdmin', {
                checkHaslo: password
            })
        }  
    }
}]);