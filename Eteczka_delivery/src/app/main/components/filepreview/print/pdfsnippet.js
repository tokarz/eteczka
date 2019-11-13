function renderPDF(canvasContainer, pdfData, options) {
    var options = options || { scale: 4 };

    function renderPage(page) {

        var viewport = page.getViewport(options.scale);
        var canvas = document.createElement('canvas');
        var DPI = 72; // increase to improve quality
        var PRINT_OUTPUT_SCALE = DPI / 72;
        canvas.width = Math.floor(viewport.width * PRINT_OUTPUT_SCALE);
        canvas.height = Math.floor(viewport.height * PRINT_OUTPUT_SCALE);

        // The rendered size of the canvas, relative to the size of canvasWrapper.
        canvas.style.width = '100%';
        var ctx = canvas.getContext('2d');
        var renderContext = {
            canvasContext: ctx,
            viewport: viewport
        };

        canvas.height = viewport.height;
        canvas.width = viewport.width;
        canvasContainer.appendChild(canvas);
        page.render(renderContext);
    }

    function renderPages(pdfDoc) {
        for (var num = 1; num <= pdfDoc.numPages; num++)
            pdfDoc.getPage(num).then(renderPage);
    }

    PDFJS.workerSrc = 'Scripts/pdfjs/workers/worker.js';
    PDFJS.getDocument({ data: pdfData }).then(function (pdf) {
        renderPages(pdf);

        window.postMessage("PDF_READY", "*");
    }, function (err) {
        alert(err);
    });
}


