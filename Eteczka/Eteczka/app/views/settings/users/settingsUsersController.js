'use strict';
angular.module('et.controllers').controller('settingsUsersController', ['$scope', 'settingsService', function ($scope, settingsService) {
    $scope.tabs = [
        { Id: 0, Name: 'Wszyscy' }
    ];

    $scope.parameters = {
        user: {},
        tabs: $scope.tabs,
        activeTab: $scope.tabs[0],
        searchTerm: '',
        users: [],
        loading: false
    };

	$scope.getAllHrUsers = function () {
		settingsService.getAllHrUsers().then(function (res) {
			$scope.allUsersData = res.usersWithCredentials;

			$scope.parameters.users = $scope.allUsersData.map(function (fullData) {
				return fullData.Detale;
			});

		}).catch(function () {
			console.error('Wyjatek pobierania uzytkownikow!');
		});
	};

    $scope.getAllHrUsers();
}]);