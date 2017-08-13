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
        },
        post: function (url, params) {
            var deferred = $q.defer();
            $http.post(url, params).then(function (result) {
                deferred.resolve(result.data);
            }, function (err) {
                console.error(err);
                deferred.reject(err);
            });

            return deferred.promise;
        },
        makeFilePostQuery: function (url, fd, params) {
            var deferred = $q.defer();
            $http.post(url, fd, {
                params: params,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function () {
                deferred.reject(false);
            });

            return deferred.promise;
        }
    };
}]);