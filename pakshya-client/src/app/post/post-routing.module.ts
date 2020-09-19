import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostDetailsComponent } from './post-details/post-details.component';
import { CreateComponent } from './create/create.component';
import { PostComponent } from './post.component';
import { AuthGuard } from '../core/guards/auth.guard';


const routes: Routes = [
      { path: '', component: PostComponent},
      { path: 'create', component: CreateComponent , canActivate: [AuthGuard]},
      { path: ':id', component: PostDetailsComponent },
      { path: 'edit/:id', component: CreateComponent },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class PostRoutingModule { }
