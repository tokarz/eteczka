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

                        if ($bodyCells.length % 2 === 0) {
                            scope.singleColWidth = Math.ceil(fullWidth / $bodyCells.length);

                            $table.find('thead tr').children().each(function (i, v) {
                                $(v).width(scope.singleColWidth);
                            });
                            $table.find('tbody tr').children().each(function (i, v) {
                                $(v).width(scope.singleColWidth);
                            });
                        } else {
                            scope.singleColWidth = Math.ceil(fullWidth / $bodyCells.length);
                            

                            $table.find('thead tr').children().each(function (i, v) {
                                $(v).width(scope.singleColWidth);
                            });
                            $table.find('tbody tr').children().each(function (i, v) {
                                $(v).width(scope.singleColWidth);
                            });
                        }



                        // Set the width of thead columns
                        

                       
                    });
                }
            });
        }
    }

});