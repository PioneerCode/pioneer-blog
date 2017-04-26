import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';

import { PostsPageComponent } from './posts-page.component';
import { PostRepository } from './post.repository';
import { PostService } from './post.service';

import { CategoryRepository } from '../categories/category.repository';
import { CategoryService } from '../categories/category.service';

import { TagRepository } from '../tags/tag.repository';
import { TagService } from '../tags/tag.service';

import { TruncatePipe } from '../../pipes/truncate.pipe';
import { LoaderComponent } from '../shared/loader/loader.component';
import { PagerComponent } from '../shared/pager/pager.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    PostsPageComponent,
    TruncatePipe,
    LoaderComponent,
    PagerComponent
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