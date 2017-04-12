'use strict';
angular.module('et.services').factory('filesViewService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getFiles: function () {
            return httpService.get('Pliki/??', {
                od_indeksu: 0,
                do_indeksu: 100,
                sessionId: sessionService.getSessionId()
            });
        },
        getFilesForUser: function (userid) {
            return httpService.get('Pliki/??', {
                sessionId: sessionService.getSessionId(),
                userid: userid
            });
        }
    }
}]);