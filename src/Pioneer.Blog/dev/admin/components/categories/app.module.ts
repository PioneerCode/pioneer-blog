import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';

import { CategoriesPageComponent } from './categories-page.component';

import { CategoryService } from './category.service';

import { CategoryRepository } from '../../repositories/category.repository';

import { TruncatePipe } from '../../pipes/truncate.pipe';
import { LoaderComponent } from '../shared/loader/loader.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    CategoriesPageComponent,
    TruncatePipe,
    LoaderComponent
  ],
  providers: [
    CategoryRepository,
    CategoryService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
