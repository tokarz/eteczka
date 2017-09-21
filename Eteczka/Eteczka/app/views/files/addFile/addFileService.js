'use strict';
angular.module('et.services').factory('addFileService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        pobierzMetadane: function (path) {
            return httpService.get('Pliki/PobierzMetadane', {
                sessionId: sessionService.getSessionId(),
                name: path
            });
        },
        pobierzBezpieczniePlik: function (filename) {
            return httpService.get('Resources/GetRestrictedResource', {
                sessionId: sessionService.getSessionId(),
                fileName: filename
            });
        }
    }
}]);