'use strict';

angular.module('et.directives').directive('etDataTable', ['$timeout', function ($timeout) {
    function getOrSetDefault(value, defaultValue) {
        var result = defaultValue;
        if (typeof value !== 'undefined') {
            result = value;
        }

        return result;
    }

    return {
        restrict: 'A',
        scope: {
            haskeys: '@',
            scrolly: '@',
            scrollx: '@'
        },
        link: function (scope, element) {
            //{keys:true, fixedHeader:true, paging: false, scrollY:'50vh', scrollCollapse: true, scrollX:false, info: false}

            $timeout(function () {
                var parentContainer = $(element).parent();
                var initParentSize = parentContainer.height();
                var headerHeight = $('thead', element).height();

                var initTbodyHeight = initParentSize - headerHeight - 20;

                var table = $(element).DataTable({
                    keys: getOrSetDefault(scope.haskeys, false),
                    fixedHeader: true,
                    paging: false,
                    scrollY: getOrSetDefault(scope.scrolly, initTbodyHeight),
                    info: false,
                    scrollCollapse: true,
                    scrollX: getOrSetDefault(scope.scrollx, true),
                    searching: false,
                    language: {
                        emptyTable: 'Brak danych do wyświetlenia w tabeli'
                    },
                    columnDefs: [
                        {
                            orderable: false,
                            targets: -1
                        }
                    ]
                });

                table
                    .on('key', function (e, datatable, key, cell, originalEvent) {
                        alert(cell.Data());
                        //events.prepend('<div>Key press: ' + key + ' for cell <i>' + cell.data() + '</i></div>');
                    })
                    .on('key-focus', function (e, datatable, cell) {
                        //events.prepend('<div>Cell focus: <i>' + cell.data() + '</i></div>');
                    })
                    .on('key-blur', function (e, datatable, cell) {
                        //events.prepend('<div>Cell blur: <i>' + cell.data() + '</i></div>');
                    });

                $(window).resize(function () {
                    var initParentSize = parentContainer.height();

                    var initTbodyHeight = initParentSize - headerHeight - 20;
                    (_.debounce(function () {
                        $('div.dataTables_scrollBody').height(initTbodyHeight);
                    }, 100))();

                });

            });

        }
    }
}]);
