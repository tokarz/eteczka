'use strict';
angular.module('et.controllers').controller('settingsCompaniesController', ['$scope', 'settingsService', function ($scope, settingsService) {
	$scope.tabs = [
		{ Id: 0, Name: 'Firmy' }
	];

	$scope.parameters = {
		company: {},
		tabs: $scope.tabs,
		activeTab: $scope.tabs[0],
		searchTerm: '',
		companies: [],
		loading: false
	};

	$scope.getAllCompanies = function () {
		settingsService.getAllCompanies().then(function (res) {
			$scope.allCompanies = res.Firmy;

			$scope.parameters.companies = $scope.allCompanies;

		}).catch(function () {
			console.error('Wyjatek pobierania firm!');
		});
	};

	$scope.getAllCompanies();
}]);