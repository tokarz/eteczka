'use strict';
angular.module('et.controllers').controller('shopCartController', ['$scope', 'shopCartService', function ($scope, shopCartService) {
    $scope.rows = [];

    $scope.printSelectedOptions = function () {
        alert('print');
    }

    $scope.openSendEmailDialog = function () {
        alert('email');
    }

    $scope.downloadFiles = function () {
        alert('print');
    }

    $scope.toggleSelectAll = function () {
        angular.forEach($scope.rows, function (elm) {
            elm.checked = !elm.checked;
        });
    }

    $scope.deleteSelectedFromCart = function () {
        alert('print');
    }

    $scope.deleteAllFromCart = function () {
        alert('email');
    }

    $scope.toolbar = [
        {
            action: $scope.printSelectedOptions,
            itemClass: 'toolbar-option option-one fa fa-print',
        },
        {
            action: $scope.openSendEmailDialog,
            itemClass: 'toolbar-option option-two fa fa-envelope-open-o',
        },
        {
            action: $scope.downloadFiles,
            itemClass: 'toolbar-option option-two fa fa-download',
        },
        {
            action: $scope.toggleSelectAll,
            itemClass: 'toolbar-option option-select-all fa fa-check-square',
        },
        {
            action: $scope.deleteAllFromCart,
            itemClass: 'toolbar-option option-three fa fa-trash-o',
        },
        {
            action: $scope.deleteSelectedFromCart,
            itemClass: 'toolbar-option option-three fa fa-minus-square-o',
        }
    ];

    shopCartService.getShoppingCartForUser().then(function (result) {
        $scope.rows = result.pliki;
    });

}]);