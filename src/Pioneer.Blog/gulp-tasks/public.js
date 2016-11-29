var gulp = require('gulp');
var del = require('del');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var cleanCss = require('gulp-clean-css');
var uncss = require('gulp-uncss');
var ts = require('gulp-typescript');
var tsProject = ts.createProject('tsconfig.json');

function clean() {
  return del([
    'temp/**'
  ]);
}

function styles() {
  return gulp.src(['./dev/public/app.scss'])
    .pipe(sass({ outputStyle: 'compressed' })
      .on('error', sass.logError))
    .pipe(cleanCss({ keepSpecialComments: 0 }))
    .pipe(uncss({
      html: [
        'http://localhost:8000',
        'http://localhost:8000/about',
        'http://localhost:8000/contact',
        'http://localhost:8000/blog',
        'http://localhost:8000/account/register',
        'http://localhost:8000/post/developing-a-net-core-site-in-windows-and-deploying-it-to-a-budget-linux-host',
        'http://localhost:8000/post/asp-net-core-mvc-pagination-using-a-tag-helper'
      ]
    }))
    .pipe(gulp.dest('wwwroot/'));
}

function typescript() {
  return tsProject.src()
    .pipe(tsProject())
    .pipe(gulp.dest('temp'));
}

function libs() {
  return gulp.src([
    'bower_components/jquery/dist/jquery.min.js',
    'bower_components/foundation-sites/dist/plugins/foundation.core.js',
    'bower_components/foundation-sites/dist/plugins/foundation.responsiveToggle.js',
    'bower_components/foundation-sites/dist/plugins/foundation.util.mediaQuery.js',
    'bower_components/foundation-sites/dist/plugins/foundation.equalizer.js',
    'bower_components/foundation-sites/dist/plugins/foundation.abide.js',
    'bower_components/parallax.js/parallax.js',
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

gulp.task('public', gulp.series(clean, typescript, libs, scripts, styles, gulp.parallel(watch)));
