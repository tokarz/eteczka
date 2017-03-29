'use strict';
angular.module('et.services').factory('employeesService', ['$http', 'sessionService', function ($http, sessionService) {
    return {
        getAll: function () {
            return $http.get('Pracownicy/PobierzWszystkich', {
                params: {
                    sessionID: sessionService.getSessionId()
                }
            });
        }
    };
}]);