'use strict';
angular.module('et.directives').directive('filePreview', function () {
    return {
        restrict: 'E',
        scope: {
            file: '='
        },
        templateUrl: 'app/main/components/filepreview/filePreviewView.html',
        controller: function ($scope, httpService) {
            $scope.$watch('file', function (value) {
                if (value) {
                    $scope.previewPdf(value);
                } else {
                    $('#pdfPreviewer').attr('data', '');
                }
            });

            $scope.previewPdf = function (elm) {
                $('#pdfPreviewer').attr('src', 'FILE_FETCH?src=' + elm.name + '.fetchfile');
                $('#pdfPreviewer').addClass('processing');
                httpService.get('Resources/GetRestrictedResource', {
                    fileName: 'Snopowiazalka.pdf'//elm.name
                }).then(function (result) {
                    $('#pdfPreviewer').removeClass('processing');
                    $('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.data);
                });
            }
        }
    }
});