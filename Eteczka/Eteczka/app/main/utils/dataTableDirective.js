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
            scrollx: '@',
            selectedrow: '='
        },
        link: function (scope, element) {
            //{keys:true, fixedHeader:true, paging: false, scrollY:'50vh', scrollCollapse: true, scrollX:false, info: false}

            $timeout(function () {
                var parentContainer = $(element).parent();
                var initParentSize = parentContainer.height();
                var headerHeight = $('thead', element).height();

                var initTbodyHeight = initParentSize - headerHeight - 15;

                scope.table = $(element).DataTable({
                    keys: getOrSetDefault(scope.haskeys, false),
                    fixedHeader: true,
                    paging: false,
                    scrollY: getOrSetDefault(scope.scrolly, initTbodyHeight),
                    info: false,
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

                scope.table
                    .on('key-focus', function (e, datatable, cell) {
                        scope.selectedrow = cell.index().row;
                    });

                $(window).resize(function () {
                    var initParentSize = parentContainer.height();

                    var initTbodyHeight = initParentSize - headerHeight - 15;
                    (_.debounce(function () {
                        $('div.dataTables_scrollBody').height(initTbodyHeight);
                    }, 200))();

                });

            });

            scope.$on('$destroy', function () {
                if (scope.table) {
                    scope.table.destroy();
                }
            });

        }
    }
}]);
