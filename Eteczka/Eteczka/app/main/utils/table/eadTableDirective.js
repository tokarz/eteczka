'use strict';
angular.module('et.directives').directive('ead-table', function ($timeout) {
        return {
                restrict: 'C',
                scope: {},
                link: function (scope, element) {
                        $timeout(function () {
                                var parentSize = {
                                        width: $(element).parent().width(),
                                        height: $(element).parent().height(),
                                }

                                var wholeTable = {
                                        width: parentSize.width,
                                        height: parentSize.height
                                },
                                        thead = {
                                                width: wholeTable.width,
                                        },
                                        tbody = {
                                                width: thead.width
                                        };

                                $('tbody', element).css('display', 'block');

                                $(window).resize(function () {
                                        parentSize = {
                                                width: $(element).parent().width(),
                                                height: $(element).parent().height(),
                                        }

                                        wholeTable = {
                                                width: parentSize.width,
                                                height: parentSize.height
                                        },
                                                thead = {
                                                        width: wholeTable.width,
                                                },
                                                tbody = {
                                                        width: thead.width
                                                };
                                });
                        });


                }
        }
});