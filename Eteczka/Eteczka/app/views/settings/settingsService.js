'use strict';
angular.module('et.services').factory('settingsService', ['httpService', function (httpService) {
    return {
        importFiles: function () {
            return httpService.get('FilesImport/ImportujStrukturePlikow', { nadpisz: true });
        },
        importUsers: function (sessionId) {
            return httpService.get('Pracownicy/ImportujJson', { sessionId: sessionId });
        },
        importArchives: function (sessionId) {
            return httpService.get('FilesImport/ImportujLokalizacjeArchiwow', {
                sessionId: sessionId,
                nadpisz: true
            });
        },
        importFirms: function (sessionId) {
            return httpService.get('FilesImport/ImportujFirmy', {
                sessionId: sessionId,
                nadpisz: true
            });
        },
        importAreas: function (sessionId) {
            return httpService.get('FilesImport/ImportujRejony', {
                sessionId: sessionId,
                nadpisz: true
            });
        }

    };
}]);