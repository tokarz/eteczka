'use strict';
angular.module('et.services').factory('menuContentService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getUserWorkplaces: function (pracownik) {
            return httpService.get('MiejscePracy/MiejscePracyDlaPracownika', {
                sessionId: sessionService.getSessionId(),
                numeread: pracownik.Numeread
            });
        },
        getRegionsForFirm: function (firm) {
            console.log('in promise')
            // do wywolania jak bedzie dostepna: httpService.get('<pathToMethod>', {firm})
            return Promise.resolve(['rejon A', 'rejon B', 'rejon C'])
        }
    }
}]);