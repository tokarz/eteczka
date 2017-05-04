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

                var contentHeight = window.innerHeight - (parseInt(headerHeight) - parseInt(footerHeight)) - 74;

                //$('#content').css('height', contentHeight);
                $('#content').outerHeight(contentHeight);
            });

            $(window).resize(function (event) {
                $timeout(function () {
                    var headerHeight = $('#header').outerHeight();
                    var footerHeight = $('#footer').outerHeight();
                    var contentPadding = {
                        top: $('#content').css('padding-top'),
                        bottom: $('#content').css('padding-top')
                    };

                    var contentHeight = event.target.innerHeight - (parseInt(headerHeight) - parseInt(footerHeight)) - 74;

                    $('#content').outerHeight(contentHeight);
                });
            });

            $scope.$on('$destroy', function () {
                window.off('resize');
            });
        }

    }
}]);