'use strict';
angular.module('et.services').factory('loginService', ['httpService', function (httpService) {
    return {
        authenticate: function (user, password) {
            return httpService.get('KatLoginy/PobierzPracownika', {
                nazwa: user,
                haslo: password
            });
        },
        sendMessageToAdmin: function (question) {
            return httpService.post('Admin/SendMessageToAdmin', {
                question: question
            });
        }
    };

}]);