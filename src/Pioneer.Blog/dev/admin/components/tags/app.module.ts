import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { TagsPageComponent } from './tags-page.component';

import { TagRepository } from '../../repositories/tag.repository';

import { TagService } from './tag.service';

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
