import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';

import { TagRepository } from './tag.repository';
import { TagService } from './tag.service';
import { TagsPageComponent } from './tags-page.component';

import { TruncatePipe } from '../../pipes/truncate.pipe';
import { LoaderComponent } from '../loader/loader.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  declarations: [
    AppComponent,
    TagsPageComponent,
    TruncatePipe,
    LoaderComponent
  ],
  providers: [
    TagRepository,
    TagService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }