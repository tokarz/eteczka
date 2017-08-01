'use strict';
angular.module('et.controllers').controller('menuController', ['$rootScope', '$scope', '$state', 'sessionService', function ($rootScope, $scope, $state, sessionService) {
    $scope.userLoggedIn = false;

    $scope.isUserLoggedIn = function () {
        return $scope.userLoggedIn;
    }

    $scope.$on('USER_LOGGED_IN_EV', function () {
        $scope.userLoggedIn = true;
    });

    $scope.goHome = function () {
        if ($scope.userLoggedIn) {
            $state.go('options');
        }
    }

    $scope.userMenuVisible = false;

    $scope.userMenuOptions = [
        {
            name: "Wyloguj",
            iconClass: "glyphicon glyphicon-log-out",
            onclick: function () {
                sessionService.killSession($rootScope.SESSIONID).then(function () {
                    $rootScope.SELECTED_FIRM = '';
                    $scope.userLoggedIn = false;

                    $state.go('login');
                }, function (err) {
                    $rootScope.SELECTED_FIRM = '';
                    $scope.userLoggedIn = false;
                    $state.go('login');
                    console.error(err);
                });
            }
        },
        //{
        //    name: "Edytuj Profil",
        //    iconClass: "glyphicon glyphicon-pencil",
        //    onclick: function () {
        //        //do nothing
        //    }
        //},
        //{
        //    name: "Wyslij wiadomosc",
        //    iconClass: "glyphicon glyphicon-envelope",
        //    onclick: function () {
        //        //do nothing
        //    }
        //}
    ];

    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

}]);
