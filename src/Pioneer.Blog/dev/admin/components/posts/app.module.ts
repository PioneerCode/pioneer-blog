import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { PostRepository }   from './post.repository';

import { PostService }      from './post.service';

import { AppComponent }         from './app.component';
import { PostsPageComponent }   from './posts-page.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    PostsPageComponent
  ],
  providers: [
    PostRepository,
    PostService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }