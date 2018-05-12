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
        },
        getAllUserAccounts: function () {
            return httpService.get('KatLoginy/PobierzWszystkichPracownikow', {
                sessionId: sessionService.getSessionId()
            });
        },
        getAllHrUsers: function () {
            return httpService.get('KatLoginy/PobierzWszystkichUzytkownikow', {
                sessionId: sessionService.getSessionId()
            });
        },
        deleteCompanyForUser: function (company) {
            return httpService.post('KatLoginy/UsunFirmeUzytkownika', {
                sessionId: sessionService.getSessionId(),
                firma: company
            });
        },
        deleteUser: function(user) {
            return httpService.post('KatLoginy/UsunPrac', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        addNewUser: function (user) {
            return httpService.post('KatLoginy/DodajPrac', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        editUserPassword: function (user) {
            return httpService.post('KatLoginy/ZmienHaslo', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        addCompanyToUser: function(company) {
            return httpService.post('KatLoginy/DodajFirmeDlaUzytkownika', {
                sessionId: sessionService.getSessionId(),
                company: company
            });
        },
        updateUserCompany: function(company) {
            return httpService.post('KatLoginy/AktualizujFirmeDlaUzytkownika', {
                sessionId: sessionService.getSessionId(),
                company: company
            });
        },
        setNewAdminPassword: function (oldPassword, shortPassword, longPassword) {
            return httpService.post('KatLoginy/ZmienHasloAdministratora', {
                sessionId: sessionService.getSessionId(),
                oldPassword: oldPassword,
                shortPassword: shortPassword,
                longPassword: longPassword
            });
        },
        setNewFilesPassword: function(old, newPassword) {
             return httpService.post('Pliki/ZmienHaslaPlikow', {
                sessionId: sessionService.getSessionId(),
                stareHaslo: old,
                noweHaslo: newPassword
            });
        }

    };
}]);