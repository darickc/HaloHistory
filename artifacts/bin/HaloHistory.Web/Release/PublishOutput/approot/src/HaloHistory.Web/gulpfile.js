/// <binding ProjectOpened='watch' />
//"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    minifyCss = require('gulp-minify-css'),
    uglify = require("gulp-uglify"),
    sass = require('gulp-sass'),
    autoprefixer = require('gulp-autoprefixer'),
    watch = require('gulp-watch'),
    lib = require('bower-files')(),
    sourcemaps = require('gulp-sourcemaps'),
    Promise = require('es6-promise').Promise;

var paths = {
    webroot: "./wwwroot/",
    bower: "./bower_components/"
};

gulp.task('copy', function () {

    gulp.src(lib.ext(['eot', 'woff', 'ttf', 'svg']).files)
      .pipe(gulp.dest(paths.webroot + 'fonts'));

    gulp.src(lib.ext('js').files)
        .pipe(concat('lib.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest(paths.webroot + 'js'));

    gulp.src(lib.ext('css').files)
      .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task('sass', function () {
    gulp.src(['styles/**/*.scss', paths.webroot + 'app/app.scss'])
        .pipe(sourcemaps.init())
        .pipe(sass({
            errLogToConsole: true
        }))
        .pipe(autoprefixer({
            browsers: ['last 2 versions'],
            cascade: false
        }))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(paths.webroot + 'css/'));
});

gulp.task('watch', function () {
    gulp.watch('styles/**/*.scss', ['sass']);
    gulp.watch(paths.webroot + 'app/**/*.scss', ['sass']);
});

gulp.task('css', function () {
    gulp.src([paths.webroot + 'css/reset.css', paths.webroot + 'css/font-awesome.css', paths.webroot + 'css/loading-bar.css', paths.webroot + 'css/app.css'])
      .pipe(minifyCss())
      .pipe(concat('app.min.css'))
      .pipe(gulp.dest(paths.webroot + 'css'));

    //gulp.src([paths.webroot + 'css/print.css'])
    //  .pipe(minifyCss())
    //  .pipe(concat('print.min.css'))
    //  .pipe(gulp.dest(paths.webroot + 'css'));
});

gulp.task('app-js', function () {
    gulp.src(paths.webroot + 'app/**/*.js')
      .pipe(uglify())
      .pipe(concat('app.min.js'))
      .pipe(gulp.dest(paths.webroot + 'js'));
});


//paths.js = paths.webroot + "js/**/*.js";
//paths.minJs = paths.webroot + "js/**/*.min.js";
//paths.css = paths.webroot + "css/**/*.css";
//paths.minCss = paths.webroot + "css/**/*.min.css";
//paths.concatJsDest = paths.webroot + "js/site.min.js";
//paths.concatCssDest = paths.webroot + "css/site.min.css";

//gulp.task("clean:js", function (cb) {
//    rimraf(paths.concatJsDest, cb);
//});

//gulp.task("clean:css", function (cb) {
//    rimraf(paths.concatCssDest, cb);
//});

//gulp.task("clean", ["clean:js", "clean:css"]);

//gulp.task("min:js", function () {
//    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
//        .pipe(concat(paths.concatJsDest))
//        .pipe(uglify())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min:css", function () {
//    return gulp.src([paths.css, "!" + paths.minCss])
//        .pipe(concat(paths.concatCssDest))
//        .pipe(cssmin())
//        .pipe(gulp.dest("."));
//});

//gulp.task("min", ["min:js", "min:css"]);
