'use strict';
angular.module('et.directives').directive('csAddDepartment', function () {
	return {
		restrict: 'E',
		scope: {
			company: '='
		},
		controller: 'csAddDepartmentController',
		templateUrl: 'app/main/components/menu-companies/addCompanyStructure/components/addDepartment/csAddDepartment.html'
	};
});