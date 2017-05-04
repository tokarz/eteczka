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

                var contentHeight = window.innerHeight - (2 * parseInt(headerHeight) - 2 * parseInt(footerHeight)) - 35;

                //$('#content').css('height', contentHeight);
                $('#content').outerHeight(contentHeight);
            });

            $(window).resize(function () {
                $timeout(function () {
                    var headerHeight = $('#header').outerHeight();
                    var footerHeight = $('#footer').outerHeight();
                    var contentPadding = {
                        top: $('#content').css('padding-top'),
                        bottom: $('#content').css('padding-top')
                    };

                    var contentHeight = window.innerHeight - (2 * parseInt(headerHeight) - 2 * parseInt(footerHeight)) - 35;

                    $('#content').css('height', contentHeight);
                });
            });

            $scope.$on('$destroy', function () {
                window.off('resize');
            });
        }

    }
}]);