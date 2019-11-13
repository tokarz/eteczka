'use strict';
angular.module('et.directives').directive('menuCompaniesTable', function () {
    return {
        restrict: 'E',
        scope: {
            rows: '=',
            selectedrow: '=',
            tab: '=',
            search: '=',
            loading: '=',
            placeholder: '@'
        },
        controller: 'menuCompaniesTableController',
        templateUrl: 'app/main/components/menu-companies-table/menuCompaniesTable.html',
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


