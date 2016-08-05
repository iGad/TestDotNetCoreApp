﻿/// <binding BeforeBuild='inject:index' />
"use strict";

var gulp = require("gulp"),
    series = require('stream-series'),
    inject = require('gulp-inject'),
    wiredep = require('wiredep').stream;

var webroot = "./wwwroot/";

var paths = {
    ngModule: webroot + "app/**/*.module.js",
    ngRoute: webroot + "app/**/*.route.js",
    ngController: webroot + "app/**/*.controller.js",
    script: webroot + "scripts/**/*.js",
    style: webroot + "styles/**/*.css"
};

gulp.task('inject:index', function () {
    var moduleSrc = gulp.src(paths.ngModule, { read: false });
    var routeSrc = gulp.src(paths.ngRoute, { read: false });
    var controllerSrc = gulp.src(paths.ngController, { read: false });
    var scriptSrc = gulp.src(paths.script, { read: false });
    var styleSrc = gulp.src(paths.style, { read: false });

    gulp.src(webroot + '/index.html')
        .pipe(wiredep({
            optional: 'configuration',
            goes: 'here',
            ignorePath: '..'
        }))
        .pipe(inject(series(scriptSrc, moduleSrc, routeSrc, controllerSrc), { ignorePath: '/wwwroot' }))
        .pipe(inject(series(styleSrc), { ignorePath: '/wwwroot' }))
        .pipe(gulp.dest(webroot));
});