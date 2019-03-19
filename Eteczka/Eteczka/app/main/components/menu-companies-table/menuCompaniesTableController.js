'use strict';
angular.module('et.controllers').controller('menuCompaniesTableController', ['$scope', function ($scope) {
    $scope.selectedrow = {};

    $scope.selectRow = function (company) {
        if ($scope.selectedrow === company) {
            $scope.selectedrow = {};
        } else {
            $scope.selectedrow = company;
        }
    }

    $scope.goRowUp = function () {
        if ($scope.selectedrow) {
            var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
            if (indexOfRow !== -1 && indexOfRow !== $scope.selectedrow.length - 1) {
                $scope.selectedrow = $scope.rows[indexOfRow + 1];
                $scope.$apply();
            }
        }
    }

    $scope.goRowDown = function () {
        if ($scope.selectedrow) {
            var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
            if (indexOfRow !== -1 && indexOfRow !== 0) {
                $scope.selectedrow = $scope.rows[indexOfRow - 1];
                $scope.$apply();
            }
        }
    }

    $scope.getRowStyle = function (company) {
        var result = 'table-row';

        if (company && $scope.selectedrow && company.Nazwa && $scope.selectedrow.Nazwa && company.Nazwa === $scope.selectedrow.Nazwa) {
            result += ' active-row';
        }

        return result;
    }


}]);