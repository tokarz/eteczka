'use strict';
angular.module('et.services').factory('httpService', ['$http', '$q', function ($http, $q) {
    return {
        get: function (url, params) {
            var deferred = $q.defer();
            $http.get(url, { params: params }).then(function (result) {
                deferred.resolve(result.data);
            }, function (err) {
                console.error(err);
                deferred.reject(err);
            });

            return deferred.promise;
        }
    };
}]);