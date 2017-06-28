'use strict';
angular.module('et.controllers').controller('adminViewController', ['$scope', function ($scope) {
    $scope.sideMenuOptions = [
        {
            icon: 'fa fa-id-badge',
            label: 'Uzytkownicy',
            index: 0
        },
        {
            icon: 'fa fa-universal-access',
            label: 'Uprawnienia',
            index: 1
        },
        {
            icon: 'fa fa-gear',
            label: 'Ustawienia',
            index: 2
        }
    ];

    $scope.quickNote = {
        files: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '200'
        },
        users: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '+200'
        },
        imports: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '-200'
        },
        exports: {
            icon: 'fa fa-gear',
            title: 'Tit1',
            value: 200,
            trend: '-200'
        }
    };


}]);