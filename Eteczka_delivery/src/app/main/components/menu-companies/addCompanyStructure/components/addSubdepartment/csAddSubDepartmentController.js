'use strict';
angular.module('et.controllers').controller('csAddSubDepartmentController', ['$scope', 'csAddDepartmentService', 'csAddSubDepartmentService', function ($scope, csAddDepartmentService, csAddSubDepartmentService) {
	$scope.allSubDepartments = [];
	$scope.allDepartments = [];
	$scope.selectedDepartment = null;

	$scope.$watch('company', function (selectedCompany) {
		if (selectedCompany) {
			csAddDepartmentService.getDepartmentsForCompany(selectedCompany.Firma).then(result => {
				$scope.allDepartments = result.Wydzialy;
			}).catch(err => {
				console.error('Err!' + err);
			});
		}
	});

	$scope.getForSelectedDepartment = function (department) {
		if ($scope.selectedDepartment !== department) {
			$scope.selectedDepartment = department;
			if ($scope.company) {
				csAddSubDepartmentService.getSubDepartmentsForCompany($scope.company, department).then(result => {
					$scope.allSubDepartments = result.PodWydzialy;
				}).catch(err => {
					console.error('Err!' + err);
				});
			}
		} else {
			$scope.selectedDepartment = null;
			$scope.allSubDepartments = [];
		}
	};
}]);
