'use strict';
angular.module('et.services').factory('shopCartService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getShoppingCartForUser: function () {
            return httpService.get('Koszyk/PobierzKoszykDlaUzytkownika', {
                sessionId: sessionService.getSessionId()
            });
        },
        getShoppingCartFilesCount: function () {
            return httpService.get('Koszyk/PobierzIloscPlikowUzytkownika', {
                sessionId: sessionService.getSessionId()
            });
        }

    }

}]);