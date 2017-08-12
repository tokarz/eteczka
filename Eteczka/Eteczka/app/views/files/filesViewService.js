'use strict';
angular.module('et.services').factory('filesViewService', ['httpService', 'sessionService', function (httpService, sessionService) {
    return {
        getAllFiles: function () {
            return httpService.get('Pliki/PobierzWszystkie', {
            });
        },
        getFilesForUser: function (userid) {
            return httpService.get('Pliki/PobierzDlaUzytkownika', {
                userid: userid
            });
        }
    }
}]);