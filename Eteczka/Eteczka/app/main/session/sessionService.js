﻿'use strict';
angular.module('et.services').factory('sessionService', ['httpService', function (httpService) {
    var sesja = '';
    return {
        getSessionId: function () {
            if (sesja) {
                return httpService.get('Sesja/OdnowSesje', { sesionid: sesja });
            } else {
                return httpService.get('Sesja/StworzSesje');
            }
        },
        killSession: function (sessionid) {
            return httpService.get('Sesja/ZamknijSesje', { token: sessionid });
        },
        setCompany: function (name) {
            return httpService.get('Sesja/UstawFirme', { name: name });
        }
    }
}])