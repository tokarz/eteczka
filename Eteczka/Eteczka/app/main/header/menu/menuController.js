'use strict';
angular.module('et.controllers').controller('menuController', ['$scope', '$state', function ($scope, $state) {
    $scope.isUserLoggedIn = function () {
        return $state.current.name !== 'login';        
    }

    $scope.userMenuVisible = false;

    $scope.userMenuOptions = [
        {
            name: "Wyloguj",
            iconClass: "glyphicon glyphicon-log-out",
            onclick: function () {
                $state.go('login')
            }
        },
        {
            name: "Edytuj Profil",
            iconClass: "glyphicon glyphicon-pencil",
            onclick: function () {
                //do nothing
            }
        },
        {
            name: "Wyslij wiadomosc",
            iconClass: "glyphicon glyphicon-envelope",
            onclick: function () {
                //do nothing
            }
        }
        ];

    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

}]);
