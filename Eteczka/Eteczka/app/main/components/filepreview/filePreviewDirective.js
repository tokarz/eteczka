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
                    $scope.generatePdf(result.Data.data);
                    //$('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.Data.data);
                });
            }


            $scope.generatePdf = function (base64) {
                var pdfData = atob(base64);
                // Disable workers to avoid yet another cross-origin issue (workers need
                // the URL of the script to be loaded, and dynamically loading a cross-origin
                // script does not work).
                // PDFJS.disableWorker = true;

                // The workerSrc property shall be specified.
                PDFJS.workerSrc = 'Scripts/pdfjs/workers/worker.js';

                // Using DocumentInitParameters object to load binary data.
                var loadingTask = PDFJS.getDocument({ data: pdfData });
                loadingTask.promise.then(function (pdf) {
                    console.log('PDF loaded');

                    // Fetch the first page
                    var pageNumber = 1;
                    pdf.getPage(pageNumber).then(function (page) {

                        var scale = 1.5;
                        var viewport = page.getViewport(scale);

                        // Prepare canvas using PDF page dimensions
                        var canvas = document.getElementById('pdffpreview');
                        var context = canvas.getContext('2d');
                        canvas.height = viewport.height;
                        canvas.width = viewport.width;

                        // Render PDF page into canvas context
                        var renderContext = {
                            canvasContext: context,
                            viewport: viewport
                        };
                        var renderTask = page.render(renderContext);
                        renderTask.then(function () {
                            console.log('Page rendered');
                        });
                    });
                }, function (reason) {
                    // PDF loading error
                    console.error(reason);
                });
            }

        }
    }
});