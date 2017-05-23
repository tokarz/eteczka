'use strict';
angular.module('et.utils').factory('fileService', 'httpService', function (httpService) {
    return {
        uploadFile: function (files, filename) {
            var fd = new FormData();
            fd.append("file", files[0]);

            return httpService.makeFilePostQuery('Server/ReadFile', fd, {
                'sessionID': 'someSessionId',
                'fileName': filename
            });
        }
    }

});