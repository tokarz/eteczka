'use strict';
angular.module('et.directives').directive('menuTable', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '=',
            selectedrow: '=',
            tabs: '=',
            activetab: '=',
            search: '=',
            loading: '=',
            placeholder: '@'
        },
        controller: 'menuTableController',
        templateUrl: 'app/main/components/menu-table/menuTable.html',
        link: function (scope, element) {
            $(element).find('.ead-table').on('keyup', function (e) {
                checkKey(e)
            });

            function checkKey(e) {
                e = e || window.event;
                if (e.keyCode == '38') {
                    scope.goRowDown();
                } else if (e.keyCode == '40') {
                    scope.goRowUp();
                }
                e.stopPropagation();
                e.preventDefault();
            }

        }

    };
});


