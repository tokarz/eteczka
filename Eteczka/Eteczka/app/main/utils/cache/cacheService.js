'use strict';
angular.module('et.utils').factory('cacheService', function () {
    var CACHE = {};

    return {
        addToCache: function (key, value) {
            CACHE[key] = value;
        },
        removeFromCache: function (key) {
            CACHE[key] = null;
        },
        clearCache: function () {
            CACHE = {};
        },
        getValue: function (key) {
            return CACHE[key];
        }
    }

});