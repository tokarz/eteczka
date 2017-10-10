'use strict';
angular.module('et.controllers').controller('menuFilesContentController', ['$rootScope', '$scope', 'filesViewService', 'shopCartService', 'modalService', function ($rootScope, $scope, filesViewService, shopCartService, modalService) {
    $scope.selectedFile = null;

    $scope.userFiles = [];

    $scope.selectFile = function (file) {
        if ($scope.selectedFile === file) {
            $scope.selectedFile = null;
        } else {
            $scope.selectedFile = file;
        }
    }

    $scope.getRowStyle = function (file) {
        var result = 'table-row';

        if (file === $scope.selectedFile) {
            result += ' active-row';
        }

        if (file.checked) {
            result += ' checked-row';
        }

        return result;
    }

    $scope.$watch('user', function (user) {
        if (user) {
            filesViewService.getFilesForUser(user).then(function (result) {
                $scope.userFiles = result.pliki;
            });
        }
    });

    $scope.triggeraddtocart = function () {
        var elementsToAdd = [];

        angular.forEach($scope.userFiles, function (file) {
            if (file === $scope.selectedFile || file.checked) {
                elementsToAdd.push(file.Id);
            }
        });

        shopCartService.addFilesToCart(elementsToAdd).then(function (res) {
            if (res.success) {
                modalService.alert('', 'Dodano Pliki!');
            } else {
                modalService.alert('', 'Blad, pliki znajduja sie juz w koszyku!');
            }

            $rootScope.$broadcast('RECALCULATE_CART');
        });
    };

    $scope.$watch('rows', function (value) {
        if (value) {
            $scope.userFiles = value;
        }
    });
}]);