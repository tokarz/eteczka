'use strict';
angular.module('et.controllers').controller('mainController', ['$window', '$rootScope', '$scope', '$state', 'sessionService', function ($window, $rootScope, $scope, $state, sessionService) {
    $state.go('login');
    $scope.title = 'ETeczka';
    $scope.isLoaded = false;
    $scope.selectedUser = '';
    $scope.selectedFirm = '';

    $scope.isLoggedIn = false;

    $scope.startupContext = {
        title: 'EAd',
        version: '1.0'
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
                  if (toState.name !== 'admin') {
                      event.preventDefault();
                  }
              } else if ($scope.isAdmin === false) {
                  if (toState.name === 'admin') {
                      event.preventDefault();
                  }
              }
          } else if (toState.name === 'processing') {
              event.preventDefault();
          }
      })
}]);