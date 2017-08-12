'use strict';
angular.module('et.directives').directive('statisticsChart', function ($timeout) {
    return {
        restrict: 'E',
        scope: {
            data: '='
        },
        templateUrl: 'app/views/files/statistics/statisticsChart.html',
        link: function (scope, element) {

            // Change the selector if needed


            //scope.randomData = function () {
            //    var diskUsageData = [
            //    { label: 'Umowy', value: 25, color: '#3366CC' },
            //    { label: 'Szkolenia', value: 12, color: '#DC3912' },
            //    { label: 'BHP', value: 50, color: '#FF9900' },
            //    { label: 'Certyfikaty', value: 10, color: '#109618' }
            //    ];
            //    return diskUsageData;
            //};

            scope.$watch('data', function (val) {
                if (val && val.length > 0) {
                    scope.pie = new d3pie("diskUsage", {
                        "size": {
                            "canvasHeight": 200,
                            "canvasWidth": 320,
                            "pieInnerRadius": "33%",
                            "pieOuterRadius": "100%"
                        },
                        "data": {
                            "sortOrder": "value-desc",
                            "content": val
                        },
                        "labels": {
                            "outer": {
                                "pieDistance": 32
                            },
                            "inner": {
                                "hideWhenLessThanPercentage": 3
                            },
                            "mainLabel": {
                                "fontSize": 13
                            },
                            "percentage": {
                                "color": "#ffffff",
                                "decimalPlaces": 0
                            },
                            "value": {
                                "color": "#adadad",
                                "fontSize": 13
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
            });


        }
    }

});