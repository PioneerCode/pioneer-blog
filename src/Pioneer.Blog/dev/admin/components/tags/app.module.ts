import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { AppComponent }                     from './app.component';
import { TagsPageComponent }                from './tags-page.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    TagsPageComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }