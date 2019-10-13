import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostComponent } from './post/post.component';
import { SidebarComponent } from './post/sidebar/sidebar.component';
import { RightSidebarComponent } from './post/right-sidebar/right-sidebar.component';
import { PostCardsComponent } from './post/post-cards/post-cards.component';


@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    PostComponent,
    SidebarComponent,
    RightSidebarComponent,
    PostCardsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
