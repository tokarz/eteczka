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
        getGitStateForCompany: function () {
            return httpService.get('Pliki/PobierzGitStatusDlaFirmy', {
                sessionId: sessionService.getSessionId()
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
        editCommittedFile: function (plik) {
            return httpService.post('Pliki/EdytujDokumentZBazy', {
                sessionId: sessionService.getSessionId(),
                idPliku: plik.Id,
                plik: plik
            });
        },
        deleteFiles: function (files) {
            return httpService.post('Pliki/UsunPliki', {
                sessionId: sessionService.getSessionId(),
                files: files
            });
        },
        generatePdf: function (user) {
            return httpService.get('Raporty/GenerujRaportPdfSkorowidzTeczki', {
                sessionId: sessionService.getSessionId(),
                numeread: user.Numeread
            });
        },
        getFoldersList: function () {
            return httpService.get('Pliki/PobierzFolderyZWaitingroom', {
                sessionId: sessionService.getSessionId()
            });
        },
        setUsersFolder: function (folderName) {
            return httpService.get('Pliki/UstawWaitingroomDlaUsera', {
                sessionId: sessionService.getSessionId(),
                folder: folderName
            });
        },
        hasUserChoseFolder: function () {
            return httpService.get('Pliki/CzyUserWybralFolder', {
                sessionId: sessionService.getSessionId()
            });
        }
    }
}]);