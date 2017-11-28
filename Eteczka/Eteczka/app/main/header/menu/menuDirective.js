'use strict';
angular.module('et.utils').directive('userMenu', function () {
    return {
        restrict: 'AE',
        scope: {
            loginstatus: '=',
            useroptions: '=',
            firmparams: '=',
            isadmin: '='
        },
        templateUrl: 'app/main/header/menu/userMenu.html',
        controller: 'menuController',
        link: function (scope, element) {

            $('.user-icon', element).bind('click', function (ev) {
                $('.callout', element).toggleClass('element-invisible');
            });

            $(document).bind('click', function (e) {
                if (!$(e.target).is('.user-icon') && !$(e.target).parents(".user-icon").is(".user-icon")) {
                    var elm = $('.callout', element);
                    if (!elm.hasClass('element-invisible')) {
                        elm.addClass('element-invisible');

                    }
                }
            });
        }
    }
});