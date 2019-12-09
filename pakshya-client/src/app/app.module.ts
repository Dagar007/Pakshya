import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from './material.module';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostComponent } from './post/post.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RightSidebarComponent } from './right-sidebar/right-sidebar.component';
import { PostCardsComponent } from './post/post-cards/post-cards.component';
import { CreateComponent } from './post/create/create.component';
import { PostDetailsComponent } from './post/post-details/post-details.component';
import { PostCardComponent } from './post/post-cards/post-card/post-card.component';
import { PostDelailsHeaderComponent } from './post/post-details/post-delails-header/post-delails-header.component';
import { PostDelailsCommentsComponent } from './post/post-details/post-delails-comments/post-delails-comments.component';



@NgModule({
  declarations: [
    AppComponent,
    ValueComponent,
    PostComponent,
    SidebarComponent,
    RightSidebarComponent,
    PostCardsComponent,
    CreateComponent,
    PostDetailsComponent,
    PostCardComponent,
    PostDelailsHeaderComponent,
    PostDelailsCommentsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
