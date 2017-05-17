'use strict';
angular.module('et.controllers').controller('menuController', ['$rootScope', '$scope', '$state', function ($rootScope, $scope, $state) {
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
                $rootScope.SELECTED_FIRM ='';
                $scope.userLoggedIn = false;
                $state.go('login')
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
