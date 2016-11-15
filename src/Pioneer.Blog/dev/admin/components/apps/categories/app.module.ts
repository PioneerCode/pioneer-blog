import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { AppComponent }                     from './app.component';
import { CategoryPageComponent }            from '../../pages/category/category-page.component';
import { PaginatedCollectionListComponent } from '../../paginated-collection-list/paginated-collection-list.component';
import { CategoryRepository }               from '../../../repositories/category.repository';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    CategoryPageComponent,
    PaginatedCollectionListComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }