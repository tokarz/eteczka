const gulp = require('gulp');
const webpack = require('webpack');
const webpackStream = require('webpack-stream');
const webpackConfig = require('./webpack.config.js');

gulp.task('js', () => {
    gulp.src('./src/js/index.js')
        .pipe(webpackStream(webpackConfig), web-  pack)
        .pipe(gulp.dest('./dist/js'));
});