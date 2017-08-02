import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { PostsPageComponent } from './posts-page.component';

import { PostRepository } from '../../repositories/post.repository';
import { CategoryRepository } from '../../repositories/category.repository';
import { TagRepository } from '../../repositories/tag.repository';
import { PostTagRepository } from '../../repositories/post-tag.repository';

import { PostService } from './post.service';
import { TagService } from '../tags/tag.service';
import { CategoryService } from '../categories/category.service';

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
    PostTagRepository,
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
