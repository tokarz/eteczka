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
                sessionId: sessionService.getSessionId(),
                pesel: pesel
            });
        },
        getEmployeesForFile: function (file) {
            return httpService.get('Pracownicy/PobierzDlaPliku', {
                sessionId: sessionService.getSessionId(),
                file: file
            });
        },
        searchByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPracownikowPoTekscie', {
                sessionId: sessionService.getSessionId(),
                search: text
            });
        },
        searchHiredByText: function (text) {
            return httpService.get('Pracownicy/WyszukajZatrPracownikowPoTekscie', {
                sessionId: sessionService.getSessionId(),
                search: text
            });
        },
        searchRemainingByText: function (text) {
            return httpService.get('Pracownicy/WyszukajPozostPracownikowPoTekscie', {
                sessionId: sessionService.getSessionId(),
                search: text
            });
        },
        addEmployee: function (pracownik) {
            return httpService.put('Pracownicy/Dodaj', {
                sessionId: sessionService.getSessionId(),
                pracownik: pracownik
            });
        },
        editEmployee: function (pracownik) {
            return httpService.post('Pracownicy/Edytuj', {
                sessionId: sessionService.getSessionId(),
                pracownik: pracownik
            });
        },
        addEmployeeWithWorkplace: function (employeeWithWorkplace) {
            return httpService.post('Pracownicy/DodajPracownikaIMiejscePracy', {
                sessionId: sessionService.getSessionId(),
                pracownikDoDodania: employeeWithWorkplace
            });
        },
        addWorkplace: function (workplace) {
            return httpService.post('MiejscePracy/DodajMiejscePracy', {
                sessionId: sessionService.getSessionId(),
                miejsceDoDodania: workplace
            });
        },
        editWorkplace: function (workplace) {
            return httpService.put('MiejscePracy/EdytujMiejscePracy', {
                sessionId: sessionService.getSessionId(),
                miejsceDoEdycji: workplace
            });
        },
        removeWorkplace: function (workplace) {
            return httpService.put('MiejscePracy/UsunMiejscePracy', {
                sessionId: sessionService.getSessionId(),
                miejsceDoUsuniecia: workplace
            });
        }
    };
}]);