'use strict';
module.exports = function (config) {
    config.set({
        basePath: '',
        frameworks: ['jasmine'],
        files: [
           { 'pattern': 'Scripts/jquery-1.10.2.min.js', 'instrument': false },
           { 'pattern': 'test/scripts/lib/jasmine-jquery.js', 'instrument': false },
           { 'pattern': 'Scripts/angular/angular.js', 'instrument': false },
           { 'pattern': 'Scripts/angular-ui-router/angular-ui-router.min.js', 'instrument': false },
           { 'pattern': 'Scripts/angular-animate/angular-animate.js', 'instrument': false },
           { 'pattern': 'Scripts/angular/angular.aria.js', 'instrument': false },
           { 'pattern': 'node_modules/angular-material/angular-material.js', 'instrument': false },
           { 'pattern': 'node_modules/angular-mocks/angular-mocks.js', 'instrument': false },
           { 'pattern': 'node_modules/phantomjs-polyfill-find/find-polyfill.js', 'instrument': false },
           { 'pattern': 'Scripts/lodash.js', 'instrument': false },
                'app/app.js',
                'app/**/*.js',
                'test/**/*_test.js'
        ],
        reporters: ['progress', 'coverage'],
        preprocessors: {
            'app/**/*.js': ['coverage']
        },
        // optionally, configure the reporter
        coverageReporter: {
            type: 'html',
            dir: '',
            subdir: '.',
            file: 'js-coverage.xml'
        }
    });
};