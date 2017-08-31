'use strict';
angular.module('et.services').factory('sessionService', ['$q', 'httpService', function ($q, httpService) {
    var sesja = '';
    return {
        createSession: function () {
            var deferred = $q.defer();
            httpService.get('Sesja/StworzSesje').then(function (result) {
                sesja = result.session;
                deferred.resolve(sesja);
            }, function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        },
        getSessionId: function () {
            if (sesja) {
                httpService.get('Sesja/OdnowSesje', { sessionid: sesja });
            }

            return sesja
        },
        killSession: function (sessionid) {
            sesja = '';
            return httpService.get('Sesja/ZamknijSesje', { token: sessionid });
        }
    }
}])