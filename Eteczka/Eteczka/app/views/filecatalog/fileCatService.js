'use strict';
angular.module('et.services').factory('fileCatService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getRegions: function () {
            return httpService.get('Rejony/PobierzRejonyDlaWybranejFirmy', {
                sessionId: sessionService.getSessionId()
            });
        },
        getDepartments: function () {
            return httpService.get('Wydzialy/PobierzWydzialy', {
                sessionId: sessionService.getSessionId()
            })
        },
        getSubDepartments: function (department) {
            return httpService.get('PodWydzial/PobierzWszystkiePodwydzialy', {
                sessionId: sessionService.getSessionId(),
                wydzial: department
            });
        },
        getAccounts5: function () {
            return httpService.get('Konto5/PobierzKonta5', {
                sessionId: sessionService.getSessionId()
            });
        },
        getDocumentTypes: function () {
            return httpService.get('KatDokumentyRodzaj/PobierzWszystkieRodzajeDokumentow', {
                sessionId: sessionService.getSessionId()
            });
        },
        getValuesForFilters: function (rejon, wydzial, podwydzial, konto5, typ, user, dateRange) {
            return httpService.post('Pliki/PobierzPlikiWgFiltrow', {
                sessionId: sessionService.getSessionId(),
                filtry: {
                    Rejon: rejon,
                    Wydzial: wydzial,
                    Podwydzial: podwydzial,
                    Konto5: konto5,
                    Typ: typ,
                    Pracownik: user,
                    DateRange: dateRange
                }
            });
        }
    }

}]);