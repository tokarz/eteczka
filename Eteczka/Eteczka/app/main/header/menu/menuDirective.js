'use strict';
angular.module('et.utils').directive('userMenu', function () {
    return {
        restrict: 'AE',
        scope: {
            loginstatus: '=',
            useroptions: '=',
            firmparams: '='
        },
        templateUrl: 'app/main/header/menu/userMenu.html',
        controller: 'menuController'
    }
});