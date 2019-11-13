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
        },
        addUser: function (user) {
            return httpService.post('KatLoginy/DodajPrac', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        changePassword: function (user) {
            return httpService.post('KatLoginy/ZmienHaslo', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        markAsDeleted: function (user) {
            return httpService.post('KatLoginy/UsunPrac', {
                sessionId: sessionService.getSessionId(),
                user: user
            });
        },
        checkPassword: function (password) {
            return httpService.post('KatLoginy/SprawdzHasloKrotkie', {
                sessionId: sessionService.getSessionId(),
                password: password
            });
        }
    }
}]);