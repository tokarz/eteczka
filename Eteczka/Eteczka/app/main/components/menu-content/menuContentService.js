'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (pracownik) {
            return httpService.get('MiejscePracy/MiejscePracyDlaPracownika', {
                sessionId: sessionService.getSessionId(),
                pracownik: pracownik
            });
        }
    }
}]);