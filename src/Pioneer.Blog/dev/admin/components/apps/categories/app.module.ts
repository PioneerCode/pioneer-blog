import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { AppComponent }                     from './app.component';
import { CategoriesPageComponent }            from '../../pages/categories/categories-page.component';
import { PaginatedCollectionListComponent } from '../../paginated-collection-list/paginated-collection-list.component';
import { CategoryRepository }               from '../../../repositories/category.repository';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
      CategoriesPageComponent,
    PaginatedCollectionListComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }