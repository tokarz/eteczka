'use strict';
angular.module('et.directives').directive('resizable', ['$timeout', function ($timeout) {
    return {
        restrict: 'C',
        scope: {},
        link: function ($scope, element) {
            $timeout(function () {
                var headerHeight = $('#header').outerHeight();
                var footerHeight = $('#footer').outerHeight();
                var contentPadding = {
                    top: $('#content').css('padding-top'),
                    bottom: $('#content').css('padding-top')
                };

                var contentHeight = window.innerHeight - (headerHeight - footerHeight) - 74;

                $('#content').css('height', contentHeight);
            });

            $(window).resize(function () {
                $timeout(function () {
                    var headerHeight = $('#header').outerHeight();
                    var footerHeight = $('#footer').outerHeight();
                    var contentPadding = {
                        top: $('#content').css('padding-top'),
                        bottom: $('#content').css('padding-top')
                    };

                    var contentHeight = window.innerHeight - (headerHeight - footerHeight) - 74;

                    $('#content').css('height', contentHeight);
                });
            });

            $scope.$on('$destroy', function () {
                window.off('resize');
            });
        }

    }
}]);