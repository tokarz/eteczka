'use strict';
angular.module('et.directives').directive('etQuickNote', function () {
    return {
        restrict: 'E',
        scope: {
            title: '=',
            icon: '=',
            value: '=', 
            trend : '='
        },
        templateUrl: 'app/views/admin/quickNotes/quickNotes.html',
        controller: function ($scope) {
            $scope.highlightTrend = function () {
                var result = 'grayTrend';
                if ($scope.trend && $scope.trend[0] === '+') {
                    result = 'positiveTrend';
                } else if ($scope.trend && $scope.trend[0] === '-') {
                    result = 'negativeTrend';
                }

                return result;
            }
        }
    }

});