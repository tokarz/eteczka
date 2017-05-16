'use strict';
angular.module('et.directives').directive('employeesView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'employeesViewController',
        templateUrl: 'app/views/employees/employeesView.html',
        link: function (scope) {
            scope.$watch('users', function (res) {
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