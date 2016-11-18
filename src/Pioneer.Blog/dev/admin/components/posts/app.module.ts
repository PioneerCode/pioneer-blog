import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';
import { FormsModule }    from '@angular/forms';

import { PostRepository }       from './post.repository';
import { CategoryRepository }   from '../categories/category.repository';
import { TagRepository }        from '../tags/tag.repository';

import { PostService }      from './post.service';
import { CategoryService }  from '../categories/category.service';
import { TagService }       from '../tags/tag.service';

import { AppComponent }         from './app.component';
import { PostsPageComponent }   from './posts-page.component';

import { TruncatePipe }   from '../../pipes/truncate.pipe';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    PostsPageComponent,
    TruncatePipe
  ],
  providers: [
    PostRepository,
    CategoryRepository,
    TagRepository,
    PostService,
    CategoryService,
    TagService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }