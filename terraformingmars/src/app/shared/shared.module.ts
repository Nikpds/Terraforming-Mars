import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';

import { IconPickerModule } from 'ngx-icon-picker';
import { LoaderService } from './loader.service';
import { PaginationService } from './pagination.service';
import { UtilityService } from './utility.service';
import { WINDOW_PROVIDERS } from './windowService';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    IconPickerModule,
    NoopAnimationsModule,
    HttpClientModule
  ],
  providers: [
    LoaderService,
    UtilityService,
    PaginationService,
    WINDOW_PROVIDERS
  ],
  exports: [
    CommonModule,
    IconPickerModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ]
})
export class SharedModule { }
