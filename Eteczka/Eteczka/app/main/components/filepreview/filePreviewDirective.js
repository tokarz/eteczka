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
                    $scope.generatePdf('');
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
                    $('#pdfPreviewer').empty();

                    $scope.generatePdf(result.Data.data);
                    //$('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.Data.data);
                });
            }

            $scope.closePdf = function () {

            }

            $scope.generatePdf = function (base64) {
                var pdfData = atob(base64);
                // Disable workers to avoid yet another cross-origin issue (workers need
                // the URL of the script to be loaded, and dynamically loading a cross-origin
                // script does not work).
                // PDFJS.disableWorker = true;

                // The workerSrc property shall be specified.
                PDFJS.workerSrc = 'Scripts/pdfjs/workers/worker.js';

                var pdfDoc = null,
                pageNum = 1,
                pageRendering = false,
                pageNumPending = null,
                scale = 2.25,
                canvas = document.getElementById('pdfPreview'),
                ctx = canvas.getContext('2d');
                function renderPage(num) {
                    pageRendering = true;
                    // Using promise to fetch the page
                    pdfDoc.getPage(num).then(function (page) {
                        var viewport = page.getViewport(scale);
                        canvas.height = viewport.height;
                        canvas.width = viewport.width;

                        // Render PDF page into canvas context
                        var renderContext = {
                            canvasContext: ctx,
                            viewport: viewport
                        };
                        var renderTask = page.render(renderContext);

                        // Wait for rendering to finish
                        renderTask.promise.then(function () {
                            pageRendering = false;
                            if (pageNumPending !== null) {
                                // New page rendering is pending
                                renderPage(pageNumPending);
                                pageNumPending = null;
                            }
                        });
                    });

                    // Update page counters
                    document.getElementById('page_num').textContent = pageNum;
                }

                /**
                 * If another page rendering in progress, waits until the rendering is
                 * finised. Otherwise, executes rendering immediately.
                 */
                function queueRenderPage(num) {
                    if (pageRendering) {
                        pageNumPending = num;
                    } else {
                        renderPage(num);
                    }
                }

                function onPrevPage() {
                    if (pageNum <= 1) {
                        return;
                    }
                    pageNum--;
                    queueRenderPage(pageNum);
                }
                document.getElementById('prev').addEventListener('click', onPrevPage);


                function onFitToWindow() {
                    scale = 2.25;
                    renderPage(pageNum);
                }
                document.getElementById('fittowindow').addEventListener('click', onFitToWindow);


                function onZoomIn() {
                    scale += 0.25;
                    renderPage(pageNum);

                }
                document.getElementById('zoomin').addEventListener('click', onZoomIn);

                function onZoomOut() {
                    if (scale <= 1) {
                        return;
                    }
                    scale -= 0.25;
                    renderPage(pageNum);
                }
                document.getElementById('zoomout').addEventListener('click', onZoomOut);


                function onNextPage() {
                    if (pageNum >= pdfDoc.numPages) {
                        return;
                    }
                    pageNum++;
                    queueRenderPage(pageNum);
                }
                document.getElementById('next').addEventListener('click', onNextPage);

                /**
                 * Asynchronously downloads PDF.
                 */
                PDFJS.getDocument({ data: pdfData }).then(function (pdfDoc_) {
                    pdfDoc = pdfDoc_;
                    document.getElementById('page_count').textContent = pdfDoc.numPages;

                    // Initial/first page rendering
                    renderPage(pageNum);
                });
            }

        }
    }
});