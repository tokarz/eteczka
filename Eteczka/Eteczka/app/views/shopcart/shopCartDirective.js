'use strict';
angular.module('et.directives').directive('shopCart', function () {
    return {
        restrict: 'E',
        scope: {},
        templateUrl: 'app/views/shopcart/shopCartView.html',
        controller: 'shopCartController'
    };
});