'use strict';
angular.module('et.services').factory('utilsService', ['httpService', function (httpService) {
    return {
        isPeselValid: function (pesel, plec) {
            return httpService.get('Utils/IsPeselValid', {
                pesel: pesel,
                plec: plec
            });
        }
    }
}]);