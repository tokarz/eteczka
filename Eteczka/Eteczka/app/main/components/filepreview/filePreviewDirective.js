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

            $scope.$watch('file', function (value) {
                if (value) {
                    if ($scope.secure === 'true') {
                        $scope.previewPdf(value, 'GetRestrictedResource', 'pdfPreview');
                    } else {
                        $scope.previewPdf(value, 'GetResource', 'pdfPreview');
                    }
                } else {
                    $scope.generatePdf('');
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

                var pdfDoc = null,
                pageNum = 1,
                pageRendering = false,
                pageNumPending = null,
                scale = 1.5,
                canvas = document.getElementById(id),
                ctx = canvas.getContext('2d');

                function renderPages(pdfDoc) {
                    for (var num = 1; num <= pdfDoc.numPages; num++)
                        pdfDoc.getPage(num).then(renderPage);
                }

                function renderPage(num) {
                    pageRendering = true;
                    // Using promise to fetch the page
                    if (pdfDoc) {
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


                function onFitToWindow() {
                    scale = 2.25;
                    renderPage(pageNum);
                }
                document.getElementById('fittowindow').addEventListener('click', onFitToWindow);

                function printCanvas(data) {
                    httpService.get('app/main/components/filepreview/print/print.html').then(function (html) {
                        var contents = html;
                        contents = contents.replace('{{resource}}', 'atob("' + data + '")');

                        var windowContent = contents;

                        var printWin = window.open('', '', 'width=800,height=700');

                        printWin.document.open();
                        printWin.document.write(windowContent);

                        printWin.addEventListener("PDF_READY", function (e) {
                            printWin.focus();
                            printWin.print();
                            printWin.close();
                        }, true);
                        printWin.document.close();
                    });
                }

                function onPrint() {
                    printCanvas(base64);
                }
                document.getElementById('print').addEventListener('click', onPrint);

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

        }
    }
});