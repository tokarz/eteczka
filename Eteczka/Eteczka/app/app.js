/*property
    Pragma, cache, config, defaults, forEach, get, headers, module, plugins,
    push, requires, useApplyAsync
*/
'use strict';
angular.module('et.controllers', []);
angular.module('et.services', []);
angular.module('et.directives', []);
angular.module('et.utils', []);

var app = angular.module('EtApp',
    ['ui.router',
     'et.controllers',
     'et.services',
     'et.directives',
     'et.utils'
    ]
     );

app.config(function ($stateProvider) {
    var loginState = {
        url: '/login',
        name: 'login',
        template: '<login-view></login-view>'
    };

    var optionsState = {
        url: '/options',
        name: 'options',
        template: '<options-view></options-view>'
    };

    $stateProvider.state(loginState);
    $stateProvider.state(optionsState);
});

// IE chached $http.get Aufrufe (z.B. in statusbarController)
// deaktiviere chaching
angular.module('et.services').config(['$httpProvider', function ($httpProvider) {
    //initialize get if not there
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    //test if it helps. It delays all $http request to the newxt $digest process
    $httpProvider.useApplyAsync(true);

    // Answer edited to include suggestions from comments
    // because previous version of code introduced browser-related errors

    //disable IE ajax request caching
    $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    // extra
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

    //$httpProvider.interceptors.push('myInterceptor');

}]);




















