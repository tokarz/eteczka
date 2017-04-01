'use strict';
angular.module('et.directives').directive('resizable', ['$timeout', function ($timeout) {
    return {
        restrict: 'C',
        scope: {},
        link: function ($scope) {
            $timeout(function () {
                var headerHeight = $('#header').outerHeight();
                var footerHeight = $('#footer').outerHeight();

                var contentHeight = window.screen.availHeight - headerHeight - footerHeight;

                $('#content').css('height', contentHeight);
            });

            $(window).resize(function () {
                $timeout(function () {
                    var headerHeight = $('#header').outerHeight();
                    var footerHeight = $('#footer').outerHeight();

                    var contentHeight = window.screen.availHeight - headerHeight - footerHeight;

                    $('#content').css('height', contentHeight);
                });
            });

            $scope.$on('$destroy', function () {
                window.off('resize');
            });
        }

    }
}]);