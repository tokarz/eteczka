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
        'ui.bootstrap',
        'et.controllers',
        'et.services',
        'et.directives',
        'et.utils',
        'angular-files-drop',
        'ngMaterial'
    ]
);

app.config(function ($stateProvider) {
    var loginState = {
        url: '/login',
        name: 'login',
        template: '<login-view></login-view>'
    };

    var homeState = {
        url: '/home',
        name: 'home',
        template: '<home-view></home-view>'
    };

    var adminState = {
        url: '/admin',
        name: 'admin',
        template: '<admin-view></options-view>'
    };

    var optionsState = {
        url: '/options',
        name: 'options',
        template: '<options-view></options-view>'
    };

    var filesState = {
        url: '/files',
        name: 'fi-files',
        template: '<files-view></files-view>'
    };

    var filecatalogState = {
        url: '/filecatalog',
        name: 'fi-catalog',
        template: '<file-cat></file-cat>'
    }

    var employeesState = {
        url: '/employees',
        name: 'emp-employees',
        template: '<employees-view></employees-view>'
    };

    var employeesFilesState = {
        url: '/employeesfiles',
        name: 'emp-files',
        template: '<employees-files-view></employees-files-view>'
    };

    var addUsersState = {
        url: '/addUsers',
        name: 'addUsers',
        template: '<add-users-view></add-users-view>'
    };
    var processingState = {
        url: '/processing',
        name: 'processing',
        template: '<processing></processing>'
    };

    var settingsManageUsers = {
        url: '/settingsusers',
        name: 'settingsusers',
        template: '<settings-users></settings-users>'
    }

    var settingsImportData = {
        url: '/settingsimport',
        name: 'settingsimport',
        template: '<settings-import></settings-import>'
    }

    var settingsSessions = {
        url: '/settingssessions',
        name: 'settingssessions',
        template: '<settings-sessions></settings-sessions>'
    }

    var settingsFiles = {
        url: '/settingsfiles',
        name: 'settingsfiles',
        template: '<settings-files></settings-files>'
    }

    var raportsState = {
        url: '/raports',
        name: 'raports',
        template: '<raports></raports>'
    }

    var shopcartState = {
        url: '/shopcart',
        name: 'shopcart',
        template: '<shop-cart></shop-cart>'
    }

    $stateProvider.state(loginState);
    $stateProvider.state(homeState);
    $stateProvider.state(optionsState);
    $stateProvider.state(filesState);
    $stateProvider.state(filecatalogState);
    $stateProvider.state(employeesState);
    $stateProvider.state(employeesFilesState);
    $stateProvider.state(addUsersState);
    $stateProvider.state(processingState);
    $stateProvider.state(adminState);
    $stateProvider.state(raportsState);
    $stateProvider.state(shopcartState);

    $stateProvider.state(settingsManageUsers);
    $stateProvider.state(settingsImportData);
    $stateProvider.state(settingsSessions);
    $stateProvider.state(settingsFiles);

});

app.config(function ($mdDateLocaleProvider) {

    // Example of a French localization.
    $mdDateLocaleProvider.months = ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'];
    $mdDateLocaleProvider.shortMonths = ['Sty', 'Lut', 'Mar', 'Kwi', 'Maj', 'Cze', 'Lip', 'Sie', 'Wrz', 'Paź', 'Lis', 'Gru'];
    $mdDateLocaleProvider.days = ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota', 'Niedziela'];
    $mdDateLocaleProvider.shortDays = ['Pon', 'Wt', 'Śr', 'Czw', 'Pt', 'So', 'Nd'];

    // Can change week display to start on Monday.
    $mdDateLocaleProvider.firstDayOfWeek = 0;

    // Optional.
    // $mdDateLocaleProvider.dates = [1, 2, 3, 4, 5, 6, ...];

    // Example uses moment.js to parse and format dates.
    $mdDateLocaleProvider.parseDate = function (dateString) {
        var m = moment(dateString, 'YYYY-MM-DD', true);
        return m.isValid() ? m.toDate() : new Date(NaN);
    };

    $mdDateLocaleProvider.formatDate = function (date) {
        var m = moment(date);
        return m.isValid() ? m.format('YYYY-MM-DD') : '';
    };

    /*$mdDateLocaleProvider.monthHeaderFormatter = function (date) {
        return myShortMonths[date.getMonth()] + ' ' + date.getFullYear();
    };*/

    // In addition to date display, date components also need localized messages
    // for aria-labels for screen-reader users.

    /*$mdDateLocaleProvider.weekNumberFormatter = function (weekNumber) {
        return 'Semaine ' + weekNumber;
    };*/

    // $mdDateLocaleProvider.msgCalendar = 'Calendrier';
    // $mdDateLocaleProvider.msgOpenCalendar = 'Ouvrir le calendrier';

    // You can also set when your calendar begins and ends.
    //$mdDateLocaleProvider.firstRenderableDate = new Date(1776, 6, 4);
    //$mdDateLocaleProvider.lastRenderableDate = new Date(2012, 11, 21);
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




















