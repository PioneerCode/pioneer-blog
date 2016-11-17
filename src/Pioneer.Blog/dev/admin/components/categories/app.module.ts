import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { AppComponent }                     from './app.component';
import { CategoriesPageComponent }          from './categories-page.component';
import { CategoryRepository }               from './category.repository';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    CategoriesPageComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }