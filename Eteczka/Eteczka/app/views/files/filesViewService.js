'use strict';
angular.module('et.services').factory('filesViewService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAllFiles: function () {
            return httpService.get('Pliki/PobierzWszystkie', {
                sessionId: sessionService.getSessionId(),
            });
        },
        getFilesForUser: function (user) {
            return httpService.get('Pliki/PobierzDlaUzytkownika', {
                sessionId: sessionService.getSessionId(),
                numeread: user.Numeread
            });
        },
        getGitStateForCompany: function (firma) {
            return httpService.get('Pliki/PobierzGitStatusDlaFirmy', {
                sessionId: sessionService.getSessionId(),
                firma: firma
            });
        },
        getFileTypes: function () {
            return httpService.get('KatDokumentyRodzaj/PobierzWszystkieRodzajeDokumentow', {
                sessionId: sessionService.getSessionId()
            });
        },
        getAllEmployees: function () {
            return httpService.get('Pracownicy/PobierzWszystkich', {
                sessionId: sessionService.getSessionId()
            });
        },
        commitFile: function (plik) {
            return httpService.post('Pliki/KomitujPlik', {
                sessionId: sessionService.getSessionId(),
                plik: plik
            });
        },
        deleteFiles: function (files) {
            return httpService.post('Pliki/UsunPliki', {
                sessionId: sessionService.getSessionId(),
                files: files
            });
        }
    }
}]);