'use strict';
angular.module('et.services').factory('settingsService', ['httpService', function (httpService) {
    return {
        importFiles: function () {
            alert('import kurwa!');
            return httpService.get('FilesImport/ImportujWszystkiePliki', { nadpisz: true });
        }
    };
}]);