'use strict';
angular.module('et.controllers').controller('homeViewController', ['$scope', '$state', function ($scope, $state) {

    $scope.options = [
        {
            label: 'Pracownicy',
            ngclass: 'menu-icon fa fa-users',
            menuclass: 'menu-button menu-button-one',
            clickaction: function () {
                $state.go('emp-files');
            }
        },
        {
            label: 'Dokumenty',
            ngclass: 'menu-icon fa fa-folder-open-o',
            menuclass: 'menu-button menu-button-two',
            clickaction: function () {
                $state.go('fi-files');
            }
        },
        {
            label: 'Raporty',
            ngclass: 'menu-icon fa fa-bar-chart not-yet-available',
            menuclass: 'menu-button menu-button-three',
            clickaction: function () {
                //$state.go('raports');
            }
        },
        {
            label: 'Struktura Firmy',
            ngclass: 'menu-icon fa fa-sitemap not-yet-available',
            menuclass: 'menu-button menu-button-four',
            clickaction: function () {

            }
        }
    ];


}]);