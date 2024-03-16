import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PostDetailsComponent } from './post-details/post-details.component';
import { CreateComponent } from './create/create.component';
import { PostComponent } from './post.component';
import { AuthGuard } from '../core/guards/auth.guard';
import { TimeagoModule } from 'ngx-timeago';


const routes: Routes = [
      { path: '', component: PostComponent},
      { path: 'create', component: CreateComponent , canActivate: [AuthGuard]},
      { path: ':id', component: PostDetailsComponent, canActivate: [AuthGuard] },
      { path: 'edit/:id', component: CreateComponent },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    TimeagoModule.forChild()
  ],
  exports: [
    RouterModule
  ]
})
export class PostRoutingModule { }
