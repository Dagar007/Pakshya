import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../material.module';
import { TimeAgoPipe } from 'time-ago-pipe';
import { LoginPopupComponent } from './components/login-popup/login-popup.component';



@NgModule({
  declarations: [TimeAgoPipe, LoginPopupComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    InfiniteScrollModule,
    MaterialModule,

  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    MaterialModule,
    TimeAgoPipe,
    InfiniteScrollModule,
    LoginPopupComponent
  ]
})
export class SharedModule { }
