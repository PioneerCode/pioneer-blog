var gulp = require('gulp');
var del = require('del');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var cleanCss = require('gulp-clean-css');
var uncss = require('gulp-uncss');
var ts = require('gulp-typescript');
var autoprefixer = require('gulp-autoprefixer');
var tsProject = ts.createProject('tsconfig.json');

var sassPaths = [
  'bower_components/normalize.scss/sass',
  'bower_components/foundation-sites/scss'
];

function clean() {
  return del([
    'temp/**'
  ]);
}

function styles() {
  //.pipe(uncss({
  //  html: [
  //    'http://localhost:8000',
  //    'http://localhost:8000/styles.html',
  //    'http://localhost:8000/about',
  //    'http://localhost:8000/contact',
  //    'http://localhost:8000/blog',
  //    'http://localhost:8000/account/register',
  //    'http://localhost:8000/post/developing-a-net-core-site-in-windows-and-deploying-it-to-a-budget-linux-host',
  //    'http://localhost:8000/post/asp-net-core-mvc-pagination-using-a-tag-helper'
  //  ]
  //}))
  return gulp.src(['./dev/public/app.scss'])
    .pipe(sass({
      includePaths: sassPaths
    }).on('error', sass.logError))
    .pipe(cleanCss({ keepSpecialComments: 0 }))

    .pipe(autoprefixer({
      browsers: ['last 2 versions', 'ie >= 9']
    }))
    .pipe(gulp.dest('wwwroot/'));
}

function typescript() {
  return tsProject.src()
    .pipe(tsProject())
    .pipe(gulp.dest('temp'));
}

function move() {
  return gulp.src([
      'bower_components/font-awesome/fonts/**/*'
    ])
    .pipe(gulp.dest('wwwroot/fonts'));
}

function libs() {
  return gulp.src([
    'bower_components/jquery/dist/jquery.min.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.core.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.responsiveToggle.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.dropdownMenu.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.tabs.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.equalizer.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.abide.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.mediaQuery.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.keyboard.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.box.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.motion.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.nest.js',
    'bower_components/foundation-sites/dist/js/plugins/foundation.util.timerAndImageLoader.js',
    'bower_components/smooth-scroll.js/dist/js/smooth-scroll.min.js',
    'scripts/syntaxhighlighter.js'
  ])
      .pipe(concat('libs.js'))
      .pipe(uglify())
      .pipe(gulp.dest('temp/lib'));
}

function scripts() {
  return gulp.src([
  'temp/lib/libs.js',
  'temp/public/**/*.js'
  ], { base: './temp/' })
      .pipe(concat('app.js'))
      .pipe(uglify())
      .pipe(gulp.dest('wwwroot'));
}

function watch() {
  gulp.watch('./typescript/app.ts', gulp.series(typescript, scripts));
  gulp.watch('./dev/public/**/*.scss', styles);
}

gulp.task('public-dev', gulp.series(clean, typescript, libs, scripts, styles, move, gulp.parallel(watch)));

gulp.task('public', gulp.series(clean, typescript, libs, scripts, styles, move));
