'use strict';
angular.module('et.controllers').controller('menuTableController', ['$scope', function ($scope) {
	$scope.selectedrow = null;

	$scope.isTabActive = function (tab) {
		var result = 'tab tab-default';
		if (tab === $scope.activetab) {
			result = 'tab tab-active';
		}

		return result;
	};
	$scope.setTabActive = function (tab) {
		$scope.activetab = tab;
	};

	$scope.deselectRow = function () {
		$scope.selectedrow = null;
	};

	$scope.selectRow = function (user) {
		if ($scope.selectedrow === user) {
			$scope.selectedrow = null;
		} else {
			$scope.selectedrow = user;
		}
	};

	$scope.goRowUp = function () {
		if ($scope.selectedrow) {
			var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
			if (indexOfRow !== -1 && indexOfRow !== $scope.selectedrow.length - 1) {
				$scope.selectedrow = $scope.rows[indexOfRow + 1];
				$scope.$apply();
			}
		}
	};

	$scope.goRowDown = function () {
		if ($scope.selectedrow) {
			var indexOfRow = $scope.rows.indexOf($scope.selectedrow);
			if (indexOfRow !== -1 && indexOfRow !== 0) {
				$scope.selectedrow = $scope.rows[indexOfRow - 1];
				$scope.$apply();
			}
		}
	};

	$scope.getRowStyle = function (row) {
		let result = 'table-row ';

		if (row && $scope.selectedrow && row === $scope.selectedrow) {
			result += 'active-row ';
			//if (row.Numeread && $scope.selectedrow.Numeread && row.Numeread === $scope.selectedrow.Numeread) {
			//	result += 'active-user-row ';
			//}
		}

		return result;
	};

}]);