import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PostComponent } from './post/post.component';
import { PostDetailsComponent } from './post/post-details/post-details.component';
import { CreateComponent } from './post/create/create.component';

const routes: Routes = [
  { path: 'posts', component: PostComponent},
  { path: 'post/:id', component: PostDetailsComponent},
  { path: 'create-post', component: CreateComponent},
  { path: 'create-post/:id', component: CreateComponent},
  { path: 'edit-post', component: CreateComponent},
  { path: '**', redirectTo: 'posts', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
