'use strict';
angular.module('et.services').factory('utilsService', ['$http', function ($http) {
    return {
        isPeselValid: function (pesel, plec) {
            return $http.get('Utils/IsPeselValid', {
                params: {
                    pesel: pesel,
                    plec: plec
                }
            });
        }
    }
}]);