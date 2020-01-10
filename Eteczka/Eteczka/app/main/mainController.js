'use strict';
angular.module('et.controllers').controller('mainController', ['$window', '$rootScope', '$scope', '$state', 'sessionService', 'cacheService', function ($window, $rootScope, $scope, $state, sessionService, cacheService) {
	$state.go('login');
	$scope.title = 'ETeczka';
	$scope.isLoaded = false;
	$scope.selectedUser = '';
	$scope.selectedFirm = '';

	$scope.isLoggedIn = false;

	$scope.startupContext = {
		title: 'EAd',
		version: '1.5-6'
	};

	$scope.currentState = {
		state: $state.current.name
	};

	$scope.$on('USER_LOGGED_IN_EV', function (ev, user) {
		$scope.isLoggedIn = true;
		$scope.selectedUser = user.userdetails;
		$scope.companies = user.companies;
		$scope.isAdmin = user.isadmin;
		$scope.selectedFirm = $scope.companies[0];
	});

	$window.onbeforeunload = function () {
		sessionService.killSession().then(function () {
			$rootScope.isLoggedIn = false;
		});
	};

	$rootScope.$on('$stateChangeStart',
		function (event, toState, toParams, fromState, fromParams) {
			if (toState.name !== 'login' && toState.name !== 'processing') {
				if ($scope.isAdmin === true) {
					if (!_.startsWith(toState.name, 'settings') && !_.startsWith(toState.name, 'admin')) {
						console.log('Prevented transition [' + fromState.name + '] => [' + toState.name + ']')
						event.preventDefault();
					}
				} else if ($scope.isAdmin === false) {
					if (_.startsWith(toState.name, 'settings') || _.isEqual(toState.name, 'admin')) {
						console.log('Prevented transition [' + fromState.name + '] => [' + toState.name + ']')
						event.preventDefault();
					}
				}
			} else if (toState.name === 'processing') {
				event.preventDefault();
			}

			if (!_.isEqual(toState.name, fromState.name)) {
				// cache sluzy tylko do akcji state.reload(). Przy zmianie stanu - czyscimy!
				cacheService.clearCache();
			}
		});
}]);