'use strict';
angular.module('et.services').factory('usersService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        removeCompanyFromUser: function (user, company) {
            return httpService.post('KatLoginy/UsunFirmeUzytkownika', {
                sessionId: sessionService.getSessionId(),
                user: user,
                firma: company
            });
        },
        addCompanyToUser: function (user, company) {
            return httpService.post('KatLoginy/DodajFirmeDlaUzytkownika', {
                sessionId: sessionService.getSessionId(),
                user: user,
                firma: company
            });
        }
    }
}]);