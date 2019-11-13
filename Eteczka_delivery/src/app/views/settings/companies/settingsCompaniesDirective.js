'use strict';
angular.module('et.directives').directive('settingsCompanies', function () {
	return {
		scope: {},
		templateUrl: 'app/views/settings/companies/settingsCompanies.html',
		controller: 'settingsCompaniesController'
	};
});