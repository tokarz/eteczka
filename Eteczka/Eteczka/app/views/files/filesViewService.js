'use strict';
angular.module('et.services').factory('filesViewService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAllFiles: function () {
            return httpService.get('Pliki/PobierzWszystkie', {
                sessionId: sessionService.getSessionId(),
            });
        },
        getFilesForUser: function (userid) {
            return httpService.get('Pliki/PobierzDlaUzytkownika', {
                sessionId: sessionService.getSessionId(),
                userid: userid
            });
        },
        getGitStateForCompany: function (firma) {
            return httpService.get('Pliki/PobierzGitStatusDlaFirmy', {
                sessionId: sessionService.getSessionId(),
                firma: firma
            });
        }
    }
}]);