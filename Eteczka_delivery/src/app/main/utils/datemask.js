'use strict';
angular.module('et.directives').directive('datemask', function () {
    return {
        restrict: 'A',
        scope: {
            separator: '@',
            datevalue: '='
        },
        link: function (scope, element) {
            $('input', element).bind('keyup', function (event) {
                var key = event.keyCode || event.charCode;
                if (key == 8 || key == 45 || key == 32) return false;
                var strokes = $(this).val().length;
                if (strokes === 4) {
                    var thisVal = $(this).val();
                    thisVal += scope.separator;
                    $(this).val(thisVal);
                }
                if (strokes === 7) {
                    var thisVal = $(this).val();
                    thisVal += scope.separator;
                    $(this).val(thisVal);
                }

                if (strokes > 10) {
                    var thisVal = $(this).val();
                    thisVal = thisVal.substring(0, thisVal.length - 1);
                    $(this).val(thisVal);
                }

            });

        }
    };

});