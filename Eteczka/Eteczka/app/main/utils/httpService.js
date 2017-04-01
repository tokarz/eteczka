﻿'use strict';
angular.module('et.services').factory('sessionService', ['$http', '$q', function ($http, $q) {
    return {
        get: function (url, params) {
            var deferred = $q.defer();
            $http.get(url, { params: params }).then(function (result) {
                deferred.resolve(result.data);
            }, function () {
                deferred.reject(result);
            });

            return deferred.promise;
        }
    };
}]);