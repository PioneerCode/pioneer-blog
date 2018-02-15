var gulp = require('gulp');
var del = require('del');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var cleanCss = require('gulp-clean-css');
var ts = require('gulp-typescript');
var autoprefixer = require('gulp-autoprefixer');
var tsProject = ts.createProject('tsconfig.json');

var sassPaths = [
    'node_modules/normalize.scss/sass',
    'node_modules/foundation-sites/scss'
];

function clean() {
    return del([
        'temp/**'
    ]);
}

function styles() {
    return gulp.src(['./styles/app.scss'])
        .pipe(sass({
            includePaths: sassPaths
        }).on('error', sass.logError))
        .pipe(cleanCss({ keepSpecialComments: 0 }))

        .pipe(autoprefixer({
            browsers: ['last 2 versions', 'ie >= 9']
        }))
        .pipe(gulp.dest('../wwwroot/'));
}

function typescript() {
    return tsProject.src()
        .pipe(tsProject())
        .pipe(gulp.dest('temp'));
}

function move() {
    return gulp.src([
        'node_modules/font-awesome/fonts/**/*'
    ])
        .pipe(gulp.dest('../wwwroot/fonts'));
}

function libs() {
    return gulp.src([
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
        .pipe(gulp.dest('../wwwroot'));
}

function watch() {
    gulp.watch('./typescript/app.ts', gulp.series(typescript, scripts));
    gulp.watch('./styles/**/*.scss', styles);
}

gulp.task('dev', gulp.series(clean, typescript, libs, scripts, styles, move, gulp.parallel(watch)));

gulp.task('build', gulp.series(clean, typescript, libs, scripts, styles, move));