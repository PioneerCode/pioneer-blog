(function (global) {
  System.config({
    paths: {
      'libs:': 'libs/'
    },
    map: {
      app: 'app',
      '@angular': 'libs:@angular',
      '@angular/core': 'libs:@angular/core/bundles/core.umd.js',
      '@angular/common': 'libs:@angular/common/bundles/common.umd.js',
      '@angular/compiler': 'libs:@angular/compiler/bundles/compiler.umd.js',
      '@angular/platform-browser': 'libs:@angular/platform-browser/bundles/platform-browser.umd.js',
      '@angular/platform-browser-dynamic': 'libs:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
      '@angular/http': 'libs:@angular/http/bundles/http.umd.js',
      '@angular/router': 'libs:@angular/router/bundles/router.umd.js',
      '@angular/forms': 'libs:@angular/forms/bundles/forms.umd.js',
      '@angular/upgrade': 'libs:@angular/upgrade/bundles/upgrade.umd.js',
      'rxjs': 'libs:rxjs'
    },
    packages: {
      app: {
        main: './main.js',
        defaultExtension: 'js'
      },
      rxjs: {
        defaultExtension: 'js'
      }
    }
  });
})(this);