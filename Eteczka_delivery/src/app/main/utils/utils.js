'use strict';
angular.module('et.services').factory('utilsService', ['httpService', function (httpService) {
    return {
        isPeselValid: function (pesel, plec) {
            return httpService.get('Utils/IsPeselValid', {
                pesel: pesel,
                plec: plec
            });
        },
        callStartupMethod: function (callback) {
            if ($.isFunction(callback)) {
                callback();
            }
        },
        findUniqueValueInListForKey: function (list, id, key) {
            if (!list) {
                return {};
            }

            if (!id) {
                return {};
            }

            if (!key) {
                return {};
            }

            var result = list.find(function (item) {
                return item[key].trim() === id.trim()
            })

            if (!result) {
                return {};
            }

            return result;
        }
    }
}]);