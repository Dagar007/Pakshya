import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardsComponent } from './post-cards/post-cards.component';
import { CreateComponent } from './create/create.component';
import { PostDetailsComponent } from './post-details/post-details.component';
import { PostCardComponent } from './post-cards/post-card/post-card.component';
import { PostDelailsHeaderComponent } from './post-details/post-delails-header/post-delails-header.component';
import { PostDelailsCommentsComponent } from './post-details/post-delails-comments/post-delails-comments.component';
import { SharedModule } from '../shared/shared.module';
import { PostComponent } from './post.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RightSidebarComponent } from './right-sidebar/right-sidebar.component';
import { PostRoutingModule } from './post-routing.module';



@NgModule({
  declarations: [
    PostComponent,
    PostCardsComponent,
    CreateComponent,
    PostDetailsComponent,
    PostCardComponent,
    PostDelailsHeaderComponent,
    PostDelailsCommentsComponent,
    SidebarComponent,
    RightSidebarComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PostRoutingModule
  ]
})
export class PostModule { }
