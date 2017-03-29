'use strict';
angular.module('et.services').factory('sessionService', [function () {
    return {
        getSessionId: function () {
            return 'toBedzieWygenerowanaSesja'
        }
    }
}])