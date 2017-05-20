﻿'use strict';
angular.module('et.services').factory('sessionService', ['httpService', function (httpService) {
    return {
        getSessionId: function () {
            return httpService.get('Sesja/StworzSesje', { token: 'token-alpha-b' });
        },
        setCompany: function (name) {
            return httpService.get('Sesja/UstawFirme', { name: name });
        }
    }
}])