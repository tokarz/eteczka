'use strict';
angular.module('et.services').factory('employeesService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAll: function () {
            return httpService.get('Pracownicy/PobierzWszystkich', {
                sessionId: sessionService.getSessionId()
            });
        },
        getHired: function () {
            return httpService.get('Pracownicy/PobierzWszystkichZatrudnionych', {
                sessionId: sessionService.getSessionId()
            });
        },
        getRemaining: function () {
            return httpService.get('Pracownicy/PobierzPozostalych', {
                sessionId: sessionService.getSessionId()
            });
        },
        getFilesForEmployee: function (pesel) {
            return httpService.get('Pliki/PobierzDlaPeselu', {
                pesel: pesel
            });
        },
        getEmployeesForFile: function (file) {
            return httpService.get('Pracownicy/PobierzDlaPliku', {
                file: file
            });
        },
        searchByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPracownikowPoTekscie', {
                search: text,
                sessionId: sessionService.getSessionId()
            });
        },
        searchHiredByText: function (text) {
            return httpService.get('Pracownicy/WyszukajZatrPracownikowPoTekscie', {
                search: text,
                sessionId: sessionService.getSessionId()
            });
        },
        searchRemainingByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPozostPracownikowPoTekscie', {
                search: text,
                sessionId: sessionService.getSessionId()
            });
        }
    };
}]);