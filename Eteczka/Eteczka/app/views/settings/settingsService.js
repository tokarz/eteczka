'use strict';
angular.module('et.services').factory('settingsService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        importUsers: function () {
            return httpService.get('Pracownicy/ImportujJson',
                {
                    sessionId: sessionService.getSessionId(),
                });
        },
        importArchives: function () {
            return httpService.get('FilesImport/ImportujLokalizacjeArchiwow', {
                sessionId: sessionService.getSessionId(),
                nadpisz: true
            });
        },
        importFirms: function () {
            return httpService.get('FilesImport/ImportujFirmy', {
                sessionId: sessionService.getSessionId(),
                nadpisz: true
            });
        },
        importAreas: function () {
            return httpService.get('FilesImport/ImportujRejony', {
                sessionId: sessionService.getSessionId(),
                nadpisz: true
            });
        },
        importWorkplaces: function () {
            return httpService.get('FilesImport/ImportujMiejscaPracy', {
                sessionId: sessionService.getSessionId()
            });
        },

        importSubdepartments: function () {
            return httpService.get('FilesImport/ImportujPodwydzialy', {
                sessionId: sessionService.getSessionId()
            });
        },
        importDepartments: function () {
            return httpService.get('FilesImport/ImportujWydzialy', {
                sessionId: sessionService.getSessionId()
            });
        },
        importAccount5: function () {
            return httpService.get('FilesImport/ImportujKonta5', {
                sessionId: sessionService.getSessionId()
            });
        },
        importDocumentTypes: function () {
            return httpService.get('FilesImport/WczytajDokDoPostgres', {
                sessionId: sessionService.getSessionId()
            });
        },
        createSourceFolder: function (name) {
            return httpService.get('FilesImport/CreateSourceFolder', {
                sessionId: sessionService.getSessionId(),
                firma: name
            });
        },
        doesFolderExist: function (folder) {
            return httpService.get('FilesImport/CzyFolderIstnieje', {
                sesja: sessionService.getSessionId(),
                folder: folder
            });
        },
        checkUpdateStatus: function (type) {
            return httpService.get('FilesImport/SprawdzUpdate', {
                sessionId: sessionService.getSessionId(),
                type: type
            });
        },
        fetchAllOpenSessions: function () {
            return httpService.get('Sesja/PobierzOtwarteSesje', {
                sessionId: sessionService.getSessionId()
            });
        }
    };
}]);