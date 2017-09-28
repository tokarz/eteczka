'use strict';
angular.module('et.directives').directive('filePreview', function () {
    return {
        restrict: 'E',
        scope: {
            file: '=',
            secure: '@',
            fileproperty: '@'
        },
        templateUrl: 'app/main/components/filepreview/filePreviewView.html',
        controller: function ($scope, httpService, sessionService) {

            $scope.$watch('file', function (value) {
                if (value) {
                    if ($scope.secure === 'true') {
                        $scope.previewPdf(value, 'GetRestrictedResource');
                    } else {
                        $scope.previewPdf(value, 'GetResource');
                    }
                } else {
                    $('#pdfPreviewer').attr('data', '');
                }
            });

            $scope.previewPdf = function (elm, ctrl) {
                $('#pdfPreviewer').attr('src', 'FILE_FETCH?src=' + elm[$scope.fileproperty] + '.fetchfile');
                $('#pdfPreviewer').addClass('processing');
                httpService.get('Resources/' + ctrl, {
                    sessionId: sessionService.getSessionId(),
                    fileName: elm[$scope.fileproperty]
                }).then(function (result) {
                    $('#pdfPreviewer').removeClass('processing');
                    $('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.Data.data);
                });
            }
        }
    }
});