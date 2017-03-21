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
    ['et.controllers',
     'et.services',
     'et.directives',
     'et.utils'
    ]
     );

angular.module('et.services').config(['$httpProvider', function ($httpProvider) {
    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
        //$httpProvider.defaults.cache = true;
    }

    $httpProvider.useApplyAsync(true);
    //$httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
    //$httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    //$httpProvider.defaults.headers.get.Pragma = 'no-cache';
}]);




















