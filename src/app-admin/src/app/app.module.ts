import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { TruncatePipe } from './pipes/truncate.pipe';
import { TagsPageComponent } from './components/pages/tags/tags-page.component';
import { CategoriesPageComponent } from './components/pages/categories/categories-page.component';
import { PostsPageComponent } from './components/pages/posts/posts-page.component';
import { LoaderComponent } from './components/loader/loader.component';
import { PagerComponent } from './components/pager/pager.component';
import { TagService } from './components/pages/tags/tag.service';
import { CategoryService } from './components/pages/categories/category.service';
import { PostService } from './components/pages/posts/post.service';
import { TagRepository } from './repositories/tag.repository';
import { CategoryRepository } from './repositories/category.repository';
import { PostTagRepository } from './repositories/post-tag.repository';
import { PostRepository } from './repositories/post.repository';
import { LoginComponent } from './components/pages/login/login.component';
import { ModalComponent } from './components/modal/modal.component';
import { AuthenticationGuard } from './guards/authentication.guard';
import { AuthenticationService } from './services/authentication.service';
import { UserRepository } from './repositories/user.repository';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    TruncatePipe,
    TagsPageComponent,
    CategoriesPageComponent,
    PostsPageComponent,
    LoaderComponent,
    PagerComponent,
    LoginComponent,
    ModalComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    AuthenticationGuard,
    AuthenticationService,
    PostRepository,
    PostTagRepository,
    CategoryRepository,
    TagRepository,
    UserRepository,
    PostService,
    CategoryService,
    TagService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
