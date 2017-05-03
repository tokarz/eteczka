'use strict';
angular.module('et.services').factory('loginService', ['httpService', function (httpService) {
    return {
        authenticate: function (user, password) {
            return httpService.get('Users/PobierzPracownika', {
                nazwa: user,
                haslo: password
            });
        }
    };

}]);