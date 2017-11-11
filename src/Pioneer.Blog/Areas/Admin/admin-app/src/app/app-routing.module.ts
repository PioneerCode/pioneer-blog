import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './components/pages/categories/categories-page.component';
import { TagsPageComponent } from './components/pages/tags/tags-page.component';
import { PostsPageComponent } from './components/pages/posts/posts-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/posts', pathMatch: 'full' },
  { path: 'categories', component: CategoriesPageComponent },
  { path: 'tags', component: TagsPageComponent },
  { path: 'posts', component: PostsPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
