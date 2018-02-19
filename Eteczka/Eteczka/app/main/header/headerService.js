'use strict';
angular.module('et.services').factory('headerService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        addFileType: function (fileType) {
            return httpService.post('KatDokumentyRodzaj/DopiszRodzajDokumentu', {
                sessionId: sessionService.getSessionId(),
                symbol: fileType.Symbol,
                nazwaDokumentu: fileType.Nazwa,
                typEdycji: fileType.Typedycji,
                teczkaDzial: fileType.Teczkadzial
            });
        }
    }
}]);