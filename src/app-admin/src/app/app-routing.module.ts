import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './components/pages/categories/categories-page.component';
import { TagsPageComponent } from './components/pages/tags/tags-page.component';
import { PostsPageComponent } from './components/pages/posts/posts-page.component';
import { AuthenticationGuard } from './guards/authentication.guard';
import { LoginComponent } from './components/pages/login/login.component';

const routes: Routes = [
  { path: 'categories', component: CategoriesPageComponent, canActivate: [AuthenticationGuard] },
  { path: 'tags', component: TagsPageComponent, canActivate: [AuthenticationGuard] },
  { path: 'posts', component: PostsPageComponent, canActivate: [AuthenticationGuard] },
  { path: '**', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
