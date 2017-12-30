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
        controller: function ($scope, httpService, sessionService, modalService) {
            $scope.isPreviewVisible = false;
            $scope.$watch('file', function (value, oldValue) {
                $scope.isPreviewVisible = false;
                $scope.closePdf('#pdfPreview');
                if (value) {
                    $scope.isPreviewVisible = true;
                    $scope.closePdf('#pdfPreview');
                    if ($scope.secure === 'true') {
                        $scope.previewPdf(value, 'GetRestrictedResource', 'pdfPreview');
                    } else {
                        $scope.previewPdf(value, 'GetResource', 'pdfPreview');
                    }
                } else {
                    $scope.isPreviewVisible = false;
                    $scope.closePdf('#pdfPreview');
                }
            });

            $scope.closePdf = function (id) {
                $(id).empty();
            }

            $scope.previewPdf = function (elm, ctrl, id) {
                var hashId = '#' + id;
                $(hashId).attr('src', 'FILE_FETCH?src=' + elm[$scope.fileproperty] + '.fetchfile');
                $(hashId).addClass('processing');
                httpService.get('Resources/' + ctrl, {
                    sessionId: sessionService.getSessionId(),
                    fileName: elm[$scope.fileproperty]
                }).then(function (result) {
                    $(hashId).removeClass('processing');
                    $scope.closePdf('#pdfPreview');

                    if (result.Data.data === 'ERROR') {
                        $scope.generatePdf(id, '');
                        modalService.alert('', 'Blad! Nieobslugiwany format pdf (plik uszkodzony lub wersja starsza niz Adobe 6.0');
                    } else {
                        $scope.generatePdf(id, result.Data.data);
                    }

                    //$('#pdfPreviewer').attr('data', 'data:application/pdf;base64,' + result.Data.data);
                });
            }

            $scope.generatePdf = function (id, base64) {
                var pdfData = atob(base64);
                // Disable workers to avoid yet another cross-origin issue (workers need
                // the URL of the script to be loaded, and dynamically loading a cross-origin
                // script does not work).
                // PDFJS.disableWorker = true;

                // The workerSrc property shall be specified.
                PDFJS.workerSrc = 'Scripts/pdfjs/workers/worker.js';
                if (pdfDoc) {
                    pdfDoc.destroy();
                }
                var pdfDoc = null,
                    pageNum = 1,
                    pageRendering = false,
                    pageNumPending = null,
                    scale = 1,
                    rotate = 0,
                    canvas = document.getElementById(id),
                    ctx = canvas.getContext('2d');

                function renderPages(pdfDoc) {
                    for (var num = 1; num <= pdfDoc.numPages; num++)
                        pdfDoc.getPage(num).then(renderPage);
                }

                function renderPage(num, rotateChange) {
                    pageRendering = true;
                    // Using promise to fetch the page
                    if (pdfDoc) {

                        pdfDoc.getPage(num).then(function (page) {

                            if (typeof rotateChange !== 'undefined') {
                                rotate = (rotate + rotateChange) % 360;
                            }


                            var viewport = page.getViewport(scale, rotate);
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
                    }

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

                //function onFitToWindow() {
                //    scale = 2.25;
                //    renderPage(pageNum);
                //}
                //document.getElementById('fittowindow').addEventListener('click', onFitToWindow);

                $("#print").unbind('click');
                $("#print").bind('click', function () {
                    $scope.onPrint(base64);
                });

                function onZoomIn() {
                    scale += 0.25;
                    renderPage(pageNum);

                }
                document.getElementById('zoomin').addEventListener('click', onZoomIn);

                function rotateLeft() {
                    var rotateLeft = -90;
                    renderPage(pageNum, rotateLeft);

                }
                document.getElementById('rotateleft').addEventListener('click', rotateLeft);

                function rotateRight() {
                    var rotateRight = 90;
                    renderPage(pageNum, rotateRight);

                }
                document.getElementById('rotateright').addEventListener('click', rotateRight);

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
                if (pdfData) {
                    PDFJS.getDocument({ data: pdfData }).then(function (pdfDoc_) {
                        pdfDoc = pdfDoc_;
                        document.getElementById('page_count').textContent = pdfDoc.numPages;

                        // Initial/first page rendering
                        renderPage(pageNum);
                    });
                } else {
                    $scope.closePdf('#pdfPreview');
                }
            }

            $scope.printCanvas = function (data) {
                httpService.get('app/main/components/filepreview/print/print.html').then(function (html) {
                    var contents = html;
                    contents = contents.replace('{{resource}}', 'atob("' + data + '")');

                    var windowContent = contents;

                    var printWin = window.open('', '', 'width=800,height=700');

                    printWin.document.open();
                    printWin.document.write(windowContent);

                    printWin.document.close();
                });
            }

            $scope.onPrint = function (base64) {
                $scope.printCanvas(base64);
            }

        }
    }
});