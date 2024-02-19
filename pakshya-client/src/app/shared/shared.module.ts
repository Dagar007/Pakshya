import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../material.module';

import { LoginPopupComponent } from './components/login-popup/login-popup.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { NgPipesModule } from 'ngx-pipes';



@NgModule({
  declarations: [LoginPopupComponent, TextInputComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    InfiniteScrollModule,
    MaterialModule,
    NgPipesModule

  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MaterialModule,
    NgPipesModule,
    InfiniteScrollModule,
    LoginPopupComponent,
    TextInputComponent
  ]
})
export class SharedModule { }
