'use strict';
angular.module('et.services').factory('employeesService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Pracownicy/PobierzWszystkich', {
                sessionID: sessionService.getSessionId()
            });
        },
        getHired: function () {
            return httpService.get('Pracownicy/PobierzWszystkichZatrudnionych', {
                sessionID: sessionService.getSessionId()
            });
        },
        getRemaining: function () {
            return httpService.get('Pracownicy/PobierzPozostalych', {
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
        },
        searchHiredByText: function (text) {
            return httpService.get('Pracownicy/WyszukajZatrPracownikowPoTekscie', {
                search: text
            });
        },
        searchRemainingByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPozostPracownikowPoTekscie', {
                search: text
            });
        }
    };
}]);