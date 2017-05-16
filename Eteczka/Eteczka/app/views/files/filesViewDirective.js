'use strict';
angular.module('et.directives').directive('filesView', function ($timeout) {
    return {
        restrict: 'E',
        scope: {},
        controller: 'filesViewController',
        templateUrl: 'app/views/files/filesView.html',
        link: function (scope, element) {

            // Change the selector if needed
            scope.$watch('files', function (res) {
                if (res && res.length > 0) {

                    $timeout(function () {
                        var $table = $('table.scrollable'),
                        $bodyCells = $table.find('tbody tr:first').children(),
                        colWidth,
                        fullWidth = $table.find('tbody').width();

                        var singleColWidth = Math.floor(fullWidth / $bodyCells.length);
                        

                        // Set the width of thead columns
                        $table.find('thead tr').children().each(function (i, v) {
                            $(v).width(singleColWidth);
                        });
                        $table.find('tbody tr').children().each(function (i, v) {
                            $(v).width(singleColWidth);
                        });
                    });
                }
            });

            scope.randomData = function () {
                var diskUsageData = [
                { label: 'Umowy', value: 25, color: '#3366CC' },
                { label: 'Szkolenia', value: 12, color: '#DC3912' },
                { label: 'BHP', value: 50, color: '#FF9900' },
                { label: 'Certyfikaty', value: 13, color: '#109618' }
                ];
                return diskUsageData;
            };

            scope.pie = new d3pie("diskUsage", {
                "size": {
                    "canvasHeight": 200,
                    "canvasWidth": 320,
                    "pieInnerRadius": "33%",
                    "pieOuterRadius": "100%"
                },
                "data": {
                    "sortOrder": "value-desc",
                    "content": scope.randomData()
                },
                "labels": {
                    "outer": {
                        "pieDistance": 32
                    },
                    "inner": {
                        "hideWhenLessThanPercentage": 3
                    },
                    "mainLabel": {
                        "fontSize": 11
                    },
                    "percentage": {
                        "color": "#ffffff",
                        "decimalPlaces": 0
                    },
                    "value": {
                        "color": "#adadad",
                        "fontSize": 11
                    },
                    "lines": {
                        "enabled": true
                    },
                    "truncation": {
                        "enabled": true
                    }
                },
                //doesnt work on IE! patch will be released soon
                "tooltips": {
                    "enabled": false
                },
                'effects': {
                    'pullOutSegmentOnClick': {
                        'effect': 'linear',
                        'speed': 400,
                        'size': 8
                    }
                },
                'misc': {
                    'gradient': {
                        'enabled': true,
                        'percentage': 100
                    }
                }
            });
        }
    }

});