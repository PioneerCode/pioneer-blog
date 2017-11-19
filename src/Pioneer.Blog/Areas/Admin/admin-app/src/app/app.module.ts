import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { PostsPageComponent } from './components/pages/posts/posts-page.component';
import { CategoriesPageComponent } from './components/pages/categories/categories-page.component';
import { TagsPageComponent } from './components/pages/tags/tags-page.component';
import { LoaderComponent } from './components/loader/loader.component';
import { PagerComponent } from './components/pager/pager.component';
import { FormsModule } from '@angular/forms';
import { TruncatePipe } from './pipes/truncate.pipe';
import { TagService } from './components/pages/tags/tag.service';
import { CategoryService } from './components/pages/categories/category.service';
import { PostService } from './components/pages/posts/post.service';
import { TagRepository } from './repositories/tag.repository';
import { CategoryRepository } from './repositories/category.repository';
import { PostTagRepository } from './repositories/post-tag.repository';
import { PostRepository } from './repositories/post.repository';
import { HttpModule } from '@angular/http';

@NgModule({
  imports: [
    HttpModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule
  ],
  declarations: [
    TruncatePipe,
    AppComponent,
    TagsPageComponent,
    CategoriesPageComponent,
    PostsPageComponent,
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
  bootstrap: [ AppComponent ]
})
export class AppModule { }
