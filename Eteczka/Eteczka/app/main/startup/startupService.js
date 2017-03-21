'use strict';
angular.module('et.services').factory('startupService', ['$http', function ($http) {
    return {
        initializeApplicationConttext: function () {
            return $http.get('Account/GetAccounts');
        }
    }

}]);

