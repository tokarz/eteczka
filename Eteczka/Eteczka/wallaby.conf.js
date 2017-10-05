'use strict';
module.exports = function () {
    return {
        files: [
           { 'pattern': 'Scripts/jquery-1.10.2.min.js', 'instrument': false },
           { 'pattern': 'test/scripts/lib/jasmine-jquery.js', 'instrument': false },
           { 'pattern': 'Scripts/angular/angular.js', 'instrument': false },
           { 'pattern': 'Scripts/angular-ui-router/angular-ui-router.min.js', 'instrument': false },
           { 'pattern': 'Scripts/angular-animate/angular-animate.js', 'instrument': false },
           { 'pattern': 'Scripts/angular/angular.aria.js', 'instrument': false },
           { 'pattern': 'node_modules/angular-material/angular-material.js', 'instrument': false },
           { 'pattern': 'node_modules/angular-mocks/angular-mocks.js', 'instrument': false },
           { 'pattern': 'Scripts/prototypes.js', 'instrument': false },
           { 'pattern': 'Scripts/lodash.js', 'instrument': false },
                'app/app.js',
                'app/**/*.js'
        ],
        tests: [
            'test/main/**/*_test.js',
            'test/views/**/*_test.js'
        ]
    };
};