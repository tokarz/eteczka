'use strict';
angular.module('et.utils').directive('userMenu', function () {
    return {
        restrict: 'AE',
        scope: {
            loginstatus: '=',
            useroptions: '=',
            firms: '=',
            selectedfirm: '='
        },
        templateUrl: 'app/main/header/menu/userMenu.html',
        controller: 'headerController'
    }
});