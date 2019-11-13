'use strict';
angular.module('et.utils').factory('adminViewService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getScannedFilesStatus: function () {
            return httpService.get('Pliki/PobierzStanZeskanowanychPlikow', {
                sessionId: sessionService.getSessionId()
            });
        }
    }
}]);