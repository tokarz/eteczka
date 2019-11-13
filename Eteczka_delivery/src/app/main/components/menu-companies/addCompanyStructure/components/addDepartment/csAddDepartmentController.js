'use strict';
angular.module('et.controllers').controller('csAddDepartmentController', ['$scope', 'csAddDepartmentService', function ($scope, csAddDepartmentService) {
	$scope.allDepartments = [];

	$scope.$watch('company', function (selectedCompany) {
		if (selectedCompany) {
			csAddDepartmentService.getDepartmentsForCompany(selectedCompany.Firma).then(result => {
				$scope.allDepartments = result.Wydzialy;
			}).catch(err => {
				console.error('Err!' + err);
			});
		}
	});
}]);