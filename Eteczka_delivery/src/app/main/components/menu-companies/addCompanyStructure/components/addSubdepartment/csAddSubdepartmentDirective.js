'use strict';
angular.module('et.directives').directive('csAddSubDepartment', function () {
	return {
		restrict: 'E',
		scope: {
			company: '='
		},
		controller: 'csAddSubDepartmentController',
		templateUrl: 'app/main/components/menu-companies/addCompanyStructure/components/addSubdepartment/csAddSubdepartment.html'
	};
});