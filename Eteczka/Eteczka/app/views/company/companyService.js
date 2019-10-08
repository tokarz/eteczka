'use strict';

angular.module('et.services').factory('companyService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAllDepartments: function () {
            return httpService.get('Wydzialy/PobierzWydzialy', {
                sessionId: sessionService.getSessionId()
            });
        },
        getSubdepartments: function (department) {
            return httpService.get('PodWydzial/PobierzWszystkiePodwydzialy', {
                sessionId: sessionService.getSessionId(),
                wydzial: department
            });
        }
    };
}]);