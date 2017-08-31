'use strict';
angular.module('et.services').factory('employeesService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Pracownicy/PobierzWszystkich', {
                sessionID: sessionService.getSessionId()
            });
        },
        getFilesForEmployee: function (pesel) {
            return httpService.get('Pliki/PobierzDlaPeselu', {
                pesel: pesel
            });
        },
        searchByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPracownikowPoTekscie', {
                search: text
            });
        }
    };
}]);