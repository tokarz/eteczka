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
            link: "#"
        },
        {
            name: "Edytuj Profil",
            iconClass: "glyphicon glyphicon-pencil",
            link: "#"
        },
        {
            name: "Wyslij wiadomosc",
            iconClass: "glyphicon glyphicon-envelope",
            link: "#"
        }
        ];

    $scope.showUserOptions = function () {
        $scope.userMenuVisible = !$scope.userMenuVisible;
    }

    $scope.executeOption = function (option) {
        //implement me
    }

}]);
