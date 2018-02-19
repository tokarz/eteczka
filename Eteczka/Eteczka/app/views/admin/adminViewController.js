'use strict';
angular.module('et.controllers').controller('adminViewController', ['$scope', '$state', function ($scope, $state) {

    $scope.options = [
        {
            label: 'Uzytkownicy',
            ngclass: 'menu-icon fa fa-users',
            menuclass: 'menu-button menu-button-one',
            clickaction: function () {
                $state.go('settingsusers');
            }
        },
        {
            label: 'Importy',
            ngclass: 'menu-icon fa fa-upload',
            menuclass: 'menu-button menu-button-two',
            clickaction: function () {
                $state.go('settingsimport');
            }
        },
        {
            label: 'Sesje',
            ngclass: 'menu-icon fa fa-clock-o',
            menuclass: 'menu-button menu-button-three',
            clickaction: function () {
                $state.go('settingssessions');
            }
        },
        {
            label: 'Foldery i pliki',
            ngclass: 'menu-icon fa fa-files-o',
            menuclass: 'menu-button menu-button-four',
            clickaction: function () {
                $state.go('settingsfiles');
            }
        },
        {
            label: 'Hasła',
            ngclass: 'menu-icon fa fa-key',
            menuclass: 'menu-button menu-button-five',
            clickaction: function () {
                $state.go('adminsessions');
            }
        }
    ];

}]);