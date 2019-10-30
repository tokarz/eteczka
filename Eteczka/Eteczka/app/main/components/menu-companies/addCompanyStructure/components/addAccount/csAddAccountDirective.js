'use strict';
angular.module('et.directives').directive('csAddAccount', function () {
	return {
		restrict: 'E',
		scope: {
			company: '='
		},
		controller: 'csAddAccountController',
		templateUrl: 'app/main/components/menu-companies/addCompanyStructure/components/addAccount/csAddAccount.html'
	};
});