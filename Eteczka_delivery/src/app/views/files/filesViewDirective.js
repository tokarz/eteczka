'use strict';
angular.module('et.directives').directive('filesView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'filesViewController',
        templateUrl: 'app/views/files/filesView.html',
        link: function (scope, element) {

            // Change the selector if needed
            scope.$watch('files', function (res) {
                if (res && res.length > 0) {

                    $timeout(function () {
                        var $table = $('table.scrollable'),
                        $bodyCells = $table.find('tbody tr:first').children(),
                        colWidth,
                        fullWidth = $table.find('tbody').width();

                        var singleColWidth = Math.floor(fullWidth / $bodyCells.length);


                        // Set the width of thead columns
                        $table.find('thead tr').children().each(function (i, v) {
                            $(v).width(singleColWidth);
                        });
                        $table.find('tbody tr').children().each(function (i, v) {
                            $(v).width(singleColWidth);
                        });
                    });
                }
            });
        }
    }

});