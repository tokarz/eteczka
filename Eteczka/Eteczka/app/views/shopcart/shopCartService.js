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
        },
        addFilesToCart: function (fileIds) {
            return httpService.get('Koszyk/DodajDoKoszyka', {
                sessionId: sessionService.getSessionId(),
                plikiId: fileIds
            });
        },
        deleteSelectedCartElements: function (fileIds) {
            return httpService.get('Koszyk/UsunZKoszyka', {
                sessionId: sessionService.getSessionId(),
                plikiId: fileIds
            });
        },
        deleteAllCartElements: function () {
            return httpService.get('Koszyk/WyczyscKoszyk', {
                sessionId: sessionService.getSessionId()
            });
        },
    }
}]);