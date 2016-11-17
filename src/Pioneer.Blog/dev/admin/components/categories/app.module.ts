import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { CategoryService}                   from './category.service';

import { CategoryRepository }               from './category.repository';

import { AppComponent }                     from './app.component';
import { CategoriesPageComponent }          from './categories-page.component';

import { TruncatePipe }   from '../../pipes/truncate.pipe';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    CategoriesPageComponent,
    TruncatePipe
  ],
  providers: [
    CategoryRepository,
    CategoryService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }