'use strict';
angular.module('et.directives').directive('draggable', function () {
    return function (scope, element) {
        $(element).draggable();
    };

});