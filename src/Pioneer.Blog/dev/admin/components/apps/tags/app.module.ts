import { NgModule }       from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule }     from '@angular/http';

import { AppComponent }                     from './app.component';
import { TagPageComponent }                 from '../../pages/tag/tag-page.component';
import { PaginatedCollectionListComponent } from '../../paginated-collection-list/paginated-collection-list.component';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    TagPageComponent,
    PaginatedCollectionListComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }